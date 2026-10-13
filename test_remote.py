import psycopg2
import sys

import os

def test_conn():
    conn_str = os.getenv("PHARMACORE_DB", "dbname='PharmaCoreDB' user='postgres' host='localhost' port='5432' password='12345' sslmode='disable'")
    try:
        conn = psycopg2.connect(conn_str)
        print("Successfully connected to the remote PostgreSQL database!")
        
        cur = conn.cursor()
        
        # 1. Print tables in the database
        cur.execute("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' ORDER BY table_name;")
        tables = cur.fetchall()
        print("\n--- Tables in Database ---")
        for table in tables:
            print(f"- {table[0]}")
            
        # 2. Count rows in key tables
        key_tables = ["Organizations", "Tenants", "Regions", "Branches", "Users", "BranchUsers"]
        print("\n--- Row Counts in Key Tables ---")
        for t in key_tables:
            try:
                cur.execute(f'SELECT COUNT(*) FROM "{t}";')
                count = cur.fetchone()[0]
                print(f"- {t}: {count} rows")
            except Exception as e:
                conn.rollback()
                print(f"- {t}: Error querying table ({e})")
                
        # 3. View users and organizations details
        print("\n--- Registered Users ---")
        try:
            cur.execute('SELECT "Id", "OrgId", "UserCode", "DisplayName", "Role", "IsActive", "CreatedAt" FROM "Users";')
            users = cur.fetchall()
            for u in users:
                print(f"User Id: {u[0]}, OrgId: {u[1]}, Email/Code: {u[2]}, Name: {u[3]}, Role: {u[4]}, Active: {u[5]}, Created: {u[6]}")
        except Exception as e:
            conn.rollback()
            print(f"Error querying Users: {e}")

        # 4. View Organizations details
        print("\n--- Organizations ---")
        try:
            cur.execute('SELECT "Id", "Code", "Name", "MaxTenants", "MaxRegions", "MaxBranches", "MaxDevices", "IsActive" FROM "Organizations";')
            orgs = cur.fetchall()
            for o in orgs:
                print(f"Org Id: {o[0]}, Code: {o[1]}, Name: {o[2]}, Limits (T/R/B/D): {o[3]}/{o[4]}/{o[5]}/{o[6]}, Active: {o[7]}")
        except Exception as e:
            conn.rollback()
            print(f"Error querying Organizations: {e}")
            
        cur.close()
        conn.close()
    except Exception as ex:
        print("Database connection error:", ex)

if __name__ == "__main__":
    test_conn()
