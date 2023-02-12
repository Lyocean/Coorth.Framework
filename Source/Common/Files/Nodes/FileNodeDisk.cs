using System.Collections.Generic;
using System.IO;
using Coorth.Logs;


namespace Coorth.Files; 

public class FileNodeDisk : FileNode {
       
    public string BasePath { get; private set; } = string.Empty;

    public FileNodeDisk() {
    }

    public FileNodeDisk(string path) {
        SetBasePath(path);
    }

    public void SetBasePath(string path) {
        BasePath = path;
        if (!string.IsNullOrEmpty(BasePath)) {
            BasePath = BasePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
        if (!string.IsNullOrEmpty(BasePath) && !BasePath.EndsWith(Path.AltDirectorySeparatorChar.ToString())) {
            BasePath += Path.AltDirectorySeparatorChar;
        }
    }

    public string ToFullPath(string path) => Path.GetFullPath(path.TrimStart('/'), BasePath);

    public override bool DirectoryExists(string path) {
        path = ToFullPath(path);
        return Directory.Exists(path);
    }

    public override DirectoryInfo DirectoryCreate(string path) {
        path = ToFullPath(path);
        return Directory.CreateDirectory(path);
    }

    public override void DirectoryDelete(string path, bool recursive = true) {
        path = ToFullPath(path);
        Directory.Delete(path, recursive);
    }

    public override IEnumerable<string> DirectoryList(string path, string pattern = "*",
        SearchOption option = SearchOption.TopDirectoryOnly) {
        path = ToFullPath(path);
        return Directory.EnumerateDirectories(path, pattern, option);
    }

    public override bool FileExists(string path) {
        path = ToFullPath(path);
        return File.Exists(path);
    }

    public override void FileDelete(string path) {
        path = ToFullPath(path);
        File.Delete(path);
    }

    public override void FileMove(string source_path, string target_path, bool overwrite = false) {
        source_path = ToFullPath(source_path);
        target_path = ToFullPath(target_path);
#if NET7_0_OR_GREATER
        File.Move(source_path, target_path, overwrite);
#else
        if (overwrite && File.Exists(target_path)) {
            File.Delete(target_path);
        }
        File.Move(source_path, target_path);
#endif
    }

    public override void FileCopy(string source_path, string target_path, bool overwrite = false) {
        source_path = ToFullPath(source_path);
        target_path = ToFullPath(target_path);
        File.Copy(source_path, target_path, overwrite);
    }

    public override FileInfo FileInfo(string path) {
        path = ToFullPath(path);
        return new FileInfo(path);
    }

    public override IEnumerable<string> FileList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly) {
        path = ToFullPath(path);
        return Directory.EnumerateFiles(path, pattern, option);
    }

    public override Stream OpenStream(string path, FileOptions option) {
        path = ToFullPath(path);
        var result = new FileStream(path, option.Mode, option.Access, option.Share);
        return result;
    }
}