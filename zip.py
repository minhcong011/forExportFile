import os
import shutil
import zipfile

# Tên file zip đầu ra
zip_filename = "UnityZip.zip"

# Thư mục cần bỏ qua (giống .gitignore)
ignore_dirs = {
    "Library", "Temp", "Obj", "Build", "Logs", ".vs", ".idea"
}
ignore_extensions = {
    ".csproj", ".sln", ".user", ".unitypackage"
}

# Nén project trừ những thứ bị ignore
def zip_project(source_dir, zip_filename):
    with zipfile.ZipFile(zip_filename, "w", zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(source_dir):
            # Bỏ qua thư mục không cần
            dirs[:] = [d for d in dirs if d not in ignore_dirs]
            for file in files:
                # Bỏ qua file không cần
                if any(file.endswith(ext) for ext in ignore_extensions):
                    continue
                filepath = os.path.join(root, file)
                arcname = os.path.relpath(filepath, source_dir)
                zipf.write(filepath, arcname)
    print(f"✅ Đã tạo {zip_filename} thành công!")

if __name__ == "__main__":
    project_path = "."  # Thư mục hiện tại
    zip_project(project_path, zip_filename)
