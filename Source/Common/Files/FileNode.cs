using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Coorth.Files;

public abstract class FileNode {

    public abstract bool DirectoryExists(string path);
    public abstract DirectoryInfo DirectoryCreate(string path);
    public abstract void DirectoryDelete(string path, bool recursive = true);
    public abstract IEnumerable<string> DirectoryList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly);

    public abstract bool FileExists(string path);
    public abstract void FileDelete(string path);
    public abstract void FileMove(string source_path, string target_path, bool overwrite = false);
    public abstract void FileCopy(string source_path, string target_path, bool overwrite = false);
    public abstract FileInfo FileInfo(string path);
    public abstract IEnumerable<string> FileList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly);
    public abstract Stream OpenStream(string path, FileOptions option);

    public Stream OpenRead(string path) => OpenStream(path, FileOptions.Read);
    public Stream OpenWrite(string path) => OpenStream(path, FileOptions.Write);
    public StreamReader OpenText(string path) {
        var stream = OpenRead(path);
        return new StreamReader(stream, Encoding.UTF8, true);
    }
}