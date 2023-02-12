using System.Collections.Generic;
using System.IO;
using Coorth.Framework;

namespace Coorth.Files;

public interface IFileManager {

    void CreateNode(string path, FileNode node);
    
    bool DirectoryExists(string path);
    DirectoryInfo DirectoryCreate(string path);
    void DirectoryDelete(string path, bool recursive = true);
    IEnumerable<string> DirectoryList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly);

    bool FileExists(string path);
    void FileDelete(string path);
    void FileMove(string source, string target, bool overwrite = false);
    void FileCopy(string source_path, string target_path, bool overwrite = false);
    FileInfo FileInfo(string path);
    IEnumerable<string> FileList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly);
    Stream OpenStream(string path, FileOptions option);

    Stream OpenRead(string path);
    Stream OpenWrite(string path);
    StreamReader OpenText(string path);
}

public sealed class FileManager : Manager, IFileManager {
    
    private readonly FileNodeGroup root = new FileNodeGroup();

    public void CreateNode(string path, FileNode node) {
        root.CreateNode(path, node);
    }

    public bool DirectoryExists(string path) => root.DirectoryExists(path);

    public DirectoryInfo DirectoryCreate(string path) => root.DirectoryCreate(path);

    public void DirectoryDelete(string path, bool recursive = true) => root.DirectoryDelete(path, recursive);

    public IEnumerable<string> DirectoryList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly) {
        return root.DirectoryList(path, pattern, option);
    }


    public bool FileExists(string path) => root.FileExists(path);

    public void FileDelete(string path) => root.FileDelete(path);

    public void FileMove(string source_path, string target_path, bool overwrite = false) {
        root.FileMove(source_path, target_path, overwrite);
    }

    public void FileCopy(string source_path, string target_path, bool overwrite = false) {
        root.FileCopy(source_path, target_path, overwrite);
    }

    public FileInfo FileInfo(string path) => root.FileInfo(path);

    public IEnumerable<string> FileList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly) {
        return root.FileList(path, pattern, option);
    }

    public Stream OpenStream(string path, FileOptions option) => root.OpenStream(path, option);

    public Stream OpenRead(string path) => root.OpenRead(path);

    public Stream OpenWrite(string path) => root.OpenWrite(path);

    public StreamReader OpenText(string path) => root.OpenText(path);
}