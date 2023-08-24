using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;


namespace Coorth.Files; 

public class FileNodeGroup : FileNode {

    private readonly Dictionary<string, FileNode> nodes = new();
    public IReadOnlyDictionary<string, FileNode> Nodes => nodes;

    public void CreateNode(string path, FileNode node) {
        nodes.Add(path, node);
    }
    
    private bool ResolveNode(ref string path, [NotNullWhen(true)]out FileNode? node) {
        for (var i = path.Length -1; i >= 0; i--) {
            var ch = path[i];
            if (ch != Path.DirectorySeparatorChar && ch != Path.AltDirectorySeparatorChar) {
                continue;
            }
            var dir = path.Substring(0, i);
            if (nodes.TryGetValue(dir, out node)) {
                path = path.Substring(i);
                return true;
            }
        }
        if (nodes.TryGetValue("", out var value)) {
            node = value;
            return true;
        }
        node = null;
        return false;
    }

    public override DirectoryInfo DirectoryCreate(string path) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.DirectoryCreate(path);
    }

    public override void DirectoryDelete(string path, bool recursive = true) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        node.DirectoryDelete(path, recursive);
    }

    public override bool DirectoryExists(string path) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.DirectoryExists(path);
    }

    public override IEnumerable<string> DirectoryList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.DirectoryList(path, pattern, option);
    }

    public override bool FileExists(string path) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.FileExists(path);
    }

    public override void FileDelete(string path) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        node.FileDelete(path);
    }

    public override void FileCopy(string source_path, string target_path, bool overwrite = false) {
        if (!ResolveNode(ref source_path, out var source_node)) {
            throw new DirectoryNotFoundException(source_path);
        }
        if (!ResolveNode(ref target_path, out var target_node)) {
            throw new DirectoryNotFoundException(target_path);
        }
        if (source_node == target_node) {
            source_node.FileCopy(source_path, target_path);
            return;
        }
        if (source_node is FileNodeDisk source_disk && target_node is FileNodeDisk target_disk) {
            source_path = source_disk.ToFullPath(source_path);
            target_path = target_disk.ToFullPath(target_path);
#if NET7_0_OR_GREATER
            File.Move(source_path, target_path, overwrite);
#else
            File.Move(source_path, target_path);
#endif
            return;
        }
        if (!overwrite && target_node.FileExists(target_path)) {
            throw new InvalidOperationException(target_path);
        }
        using var source_stream = source_node.OpenStream(source_path, FileOptions.Read);
        using var target_stream = target_node.OpenStream(target_path, FileOptions.Write);
        source_stream.CopyTo(target_stream);
    }

    public override FileInfo FileInfo(string path) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.FileInfo(path);
    }

    public override IEnumerable<string> FileList(string path, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.FileList(path, pattern, option);
    }

    public override void FileMove(string source_path, string target_path, bool overwrite = false) {
        if (!ResolveNode(ref source_path, out var source_node)) {
            throw new DirectoryNotFoundException(source_path);
        }
        if (!ResolveNode(ref target_path, out var target_node)) {
            throw new DirectoryNotFoundException(target_path);
        }
        if (source_node == target_node) {
            source_node.FileMove(source_path, target_path, overwrite);
            return;
        }
        if (source_node is FileNodeDisk source_disk && target_node is FileNodeDisk target_disk) {
            source_path = source_disk.ToFullPath(source_path);
            target_path = target_disk.ToFullPath(target_path);
#if NET7_0_OR_GREATER
            File.Move(source_path, target_path, overwrite);
#else
            File.Move(source_path, target_path);
#endif
            return;
        }
        if (!overwrite && target_node.FileExists(target_path)) {
            throw new InvalidOperationException(target_path);
        }
        using var source_stream = source_node.OpenStream(source_path, FileOptions.Read);
        using var target_stream = target_node.OpenStream(target_path, FileOptions.Write);
        source_stream.CopyTo(target_stream);
        source_node.FileDelete(source_path);
    }

    public override Stream OpenStream(string path, FileOptions option) {
        if (!ResolveNode(ref path, out var node)) {
            throw new DirectoryNotFoundException(path);
        }
        return node.OpenStream(path, option);
    }
}