import re
import os

file_path = r"D:\M7.T.S. Project\PharmaCore\PharmaCore.Infrastructure\Migrations\20260318144001_AddStandardItems.cs"

with open(file_path, "r", encoding="utf-8") as f:
    lines = f.readlines()

tables_to_remove = {
    "ActiveIngredients",
    "ProductCategories",
    "ProductUnits",
    "Warehouses",
    "Products",
    "ProductBatches",
    "BatchStocks",
    "BatchUnits",
    "EmployeeBonusLogs",
    "SellerBonuses"
}

new_lines = []
i = 0
while i < len(lines):
    line = lines[i]
    
    # Match CreateTable
    m = re.search(r'migrationBuilder\.CreateTable\(\s*name:\s*"([^"]+)"', "".join(lines[i:i+3]))
    if m and m.group(1) in tables_to_remove:
        # Skip until the matching ';' that closes the statement
        brace_count = 0
        started = False
        while i < len(lines):
            line_str = lines[i]
            if '{' in line_str: 
                brace_count += line_str.count('{')
                started = True
            if '}' in line_str: 
                brace_count -= line_str.count('}')
            if started and brace_count == 0 and ';' in line_str:
                i += 1
                break
            i += 1
        continue

    # Match CreateIndex
    m = re.search(r'migrationBuilder\.CreateIndex\(.*table:\s*"([^"]+)"', "".join(lines[i:i+4]), re.DOTALL)
    if m and m.group(1) in tables_to_remove:
        while i < len(lines):
            if ';' in lines[i]:
                i += 1
                break
            i += 1
        continue

    # Match DropTable
    m = re.search(r'migrationBuilder\.DropTable\(\s*name:\s*"([^"]+)"', "".join(lines[i:i+2]))
    if m and m.group(1) in tables_to_remove:
        while i < len(lines):
            if ';' in lines[i]:
                i += 1
                break
            i += 1
        continue
        
    new_lines.append(line)
    i += 1

with open(file_path, "w", encoding="utf-8") as f:
    f.writelines(new_lines)

print("Migration file cleaned successfully!")
