import requests
import psycopg2
import uuid
import sys
import json
import time

def main():
    print("=====================================================================")
    print("PHARMACORE E2E INTEGRATION & PROVISIONING VERIFICATION SCRIPT")
    print("=====================================================================")

    # 1. Configuration
    api_url = "http://localhost:5117/api"
    db_config = {
        "host": "localhost",
        "port": 5432,
        "database": "PharmaCoreDB",
        "user": "postgres",
        "password": "13579"
    }

    # Generate unique credentials for this run
    test_id = str(uuid.uuid4())[:8]
    org_name = f"E2E Provisioning Corp {test_id}"
    admin_name = f"Admin {test_id}"
    admin_email = f"admin_{test_id}@pharmacore-test.com"
    password = "SecureP@ssword123!"

    print(f"[1/5] Registering new organization: '{org_name}'")
    print(f"      Admin Email: {admin_email}")

    # 2. Register
    register_payload = {
        "organizationName": org_name,
        "adminName": admin_name,
        "adminEmail": admin_email,
        "password": password
    }

    try:
        res = requests.post(f"{api_url}/auth/register", json=register_payload)
    except Exception as e:
        print(f"Error connecting to the API at {api_url}: {e}")
        print("Please make sure the dotnet API backend is running on port 5117.")
        sys.exit(1)

    if res.status_code != 200:
        print(f"Registration failed with status code {res.status_code}: {res.text}")
        sys.exit(1)

    reg_data = res.json()
    token = reg_data.get("token")
    user_info = reg_data.get("user")
    
    if not token or not user_info:
        print("Registration response missing token or user information.")
        sys.exit(1)

    print("✔ Registration successful!")
    print(f"  Received Token: {token[:20]}...{token[-10:]}")
    print(f"  User ID: {user_info.get('id')}")

    # 3. Setup scales and addons
    print("\n[2/5] Configuring subscription scales & addons (setup endpoint)")
    setup_payload = {
        "tenants": 3,
        "regions": 5,
        "branches": 8,
        "devices": 10,
        "addons": {
            "aiForecast": True,
            "multiBranchSync": True,
            "customApi": False,
            "dedicatedDb": True,
            "prioritySupport": False
        }
    }

    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    res_setup = requests.post(f"{api_url}/auth/setup", json=setup_payload, headers=headers)
    if res_setup.status_code != 200:
        print(f"Setup failed with status code {res_setup.status_code}: {res_setup.text}")
        sys.exit(1)

    print("✔ Setup endpoint returned success: Setup completed successfully.")

    # 4. Connect to database to verify results
    print("\n[3/5] Connecting to PostgreSQL database for E2E validation...")
    try:
        conn = psycopg2.connect(**db_config)
        cur = conn.cursor()
    except Exception as e:
        print(f"Database connection failed: {e}")
        sys.exit(1)

    print("✔ Connected to database successfully.")

    # Get Organization ID from the admin user we registered
    user_email_normalized = admin_email.strip().lower()
    cur.execute('SELECT "OrgId" FROM "Users" WHERE "UserCode" = %s', (user_email_normalized,))
    row = cur.fetchone()
    if not row:
        print(f"Error: Admin user {admin_email} not found in database.")
        sys.exit(1)
    
    org_id = row[0]
    print(f"✔ Found Organization in database. ID: {org_id}")

    # Verify Organization limits
    cur.execute(
        'SELECT "Name", "MaxTenants", "MaxRegions", "MaxBranches", "MaxDevices", "ActiveAddons" FROM "Organizations" WHERE "Id" = %s::uuid', 
        (org_id,)
    )
    org_row = cur.fetchone()
    if not org_row:
        print("Error: Organization not found in database.")
        sys.exit(1)

    db_org_name, max_tenants, max_regions, max_branches, max_devices, active_addons = org_row
    
    print("\n[4/5] Verifying Organization Scaling limits:")
    print(f"  * Name: {db_org_name}")
    print(f"  * MaxTenants: {max_tenants} (Expected: 3) -> {'PASS' if max_tenants == 3 else 'FAIL'}")
    print(f"  * MaxRegions: {max_regions} (Expected: 5) -> {'PASS' if max_regions == 5 else 'FAIL'}")
    print(f"  * MaxBranches: {max_branches} (Expected: 8) -> {'PASS' if max_branches == 8 else 'FAIL'}")
    print(f"  * MaxDevices: {max_devices} (Expected: 10) -> {'PASS' if max_devices == 10 else 'FAIL'}")
    
    addons_dict = json.loads(active_addons)
    print(f"  * ActiveAddons: {addons_dict}")
    print(f"    - aiForecast: {addons_dict.get('aiForecast')} (Expected: True) -> {'PASS' if addons_dict.get('aiForecast') else 'FAIL'}")
    print(f"    - multiBranchSync: {addons_dict.get('multiBranchSync')} (Expected: True) -> {'PASS' if addons_dict.get('multiBranchSync') else 'FAIL'}")
    print(f"    - dedicatedDb: {addons_dict.get('dedicatedDb')} (Expected: True) -> {'PASS' if addons_dict.get('dedicatedDb') else 'FAIL'}")

    assert max_tenants == 3, "MaxTenants mismatch"
    assert max_regions == 5, "MaxRegions mismatch"
    assert max_branches == 8, "MaxBranches mismatch"
    assert max_devices == 10, "MaxDevices mismatch"
    assert addons_dict.get("aiForecast") is True, "aiForecast addon mismatch"
    assert addons_dict.get("multiBranchSync") is True, "multiBranchSync addon mismatch"
    assert addons_dict.get("dedicatedDb") is True, "dedicatedDb addon mismatch"

    # Verify Dynamic Provisioning Counts
    # A. Tenants count
    cur.execute('SELECT "Id", "Code", "Name" FROM "Tenants" WHERE "OrgId" = %s::uuid AND "IsActive" = true', (org_id,))
    tenants = cur.fetchall()
    tenant_count = len(tenants)
    print(f"\n[5/5] Verifying Provisioned Entities & Distribution:")
    print(f"  A. Tenants Provisioned: {tenant_count} (Expected: 3) -> {'PASS' if tenant_count == 3 else 'FAIL'}")
    for t_id, t_code, t_name in tenants:
        print(f"     - Tenant: {t_name} (Code: {t_code}, ID: {t_id})")
    
    assert tenant_count == 3, f"Expected 3 active tenants, found {tenant_count}"

    # B. Regions count and distribution
    tenant_ids = [t[0] for t in tenants]
    cur.execute(
        'SELECT "Id", "TenantId", "Code", "Name" FROM "Regions" WHERE "TenantId" = ANY(%s::uuid[]) AND "IsActive" = true', 
        (tenant_ids,)
    )
    regions = cur.fetchall()
    region_count = len(regions)
    print(f"  B. Regions Provisioned: {region_count} (Expected: 5) -> {'PASS' if region_count == 5 else 'FAIL'}")
    
    # Calculate regions per tenant
    regions_per_tenant = {}
    for r_id, r_tenant_id, r_code, r_name in regions:
        regions_per_tenant[r_tenant_id] = regions_per_tenant.get(r_tenant_id, 0) + 1
        print(f"     - Region: {r_name} (Code: {r_code}) belongs to Tenant: {r_tenant_id}")
    
    print("     - Distribution of Regions across Tenants:")
    for t_id, t_code, t_name in tenants:
        count = regions_per_tenant.get(t_id, 0)
        print(f"       * Tenant {t_name} has {count} region(s).")
    
    assert region_count == 5, f"Expected 5 active regions, found {region_count}"
    
    # Verify balanced distribution of regions (minimum 1 region per tenant)
    for t_id in tenant_ids:
        cnt = regions_per_tenant.get(t_id, 0)
        assert cnt >= 1, f"Tenant {t_id} has 0 regions, expected balanced distribution!"
    print("     - Region distribution check -> PASS (All tenants have at least 1 region)")

    # C. Branches count and distribution
    region_ids = [r[0] for r in regions]
    cur.execute(
        'SELECT "Id", "RegionId", "Code", "Name" FROM "Branches" WHERE "OrgId" = %s::uuid AND "IsActive" = true', 
        (org_id,)
    )
    branches = cur.fetchall()
    branch_count = len(branches)
    print(f"  C. Branches Provisioned: {branch_count} (Expected: 8) -> {'PASS' if branch_count == 8 else 'FAIL'}")
    
    # Calculate branches per region
    branches_per_region = {}
    for b_id, b_region_id, b_code, b_name in branches:
        branches_per_region[b_region_id] = branches_per_region.get(b_region_id, 0) + 1
        print(f"     - Branch: {b_name} (Code: {b_code}) belongs to Region: {b_region_id}")

    print("     - Distribution of Branches across Regions:")
    for r_id, r_tenant_id, r_code, r_name in regions:
        count = branches_per_region.get(r_id, 0)
        print(f"       * Region {r_name} has {count} branch(es).")
    
    assert branch_count == 8, f"Expected 8 active branches, found {branch_count}"

    # Verify balanced distribution of branches (minimum 1 branch per region)
    for r_id in region_ids:
        cnt = branches_per_region.get(r_id, 0)
        assert cnt >= 1, f"Region {r_id} has 0 branches, expected balanced distribution!"
    print("     - Branch distribution check -> PASS (All regions have at least 1 branch)")

    cur.close()
    conn.close()

    print("\n=====================================================================")
    print("SUCCESS: ALL E2E PROVISIONING & VERIFICATION CHECKS PASSED!")
    print("=====================================================================")

if __name__ == "__main__":
    main()
