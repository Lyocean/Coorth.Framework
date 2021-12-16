using System.Collections.Generic;

namespace Coorth {
    public class ResourceManager : Management, IResourceManager {
        
        private class ResourceFolder {
            public string Name;
            public string Path;
            public ResourceFolder Parent;
            public Dictionary<string, ResourceFolder> Children;
        }

        private readonly ResourceFolder root = new ResourceFolder();
        
        public void Register(IResourceNode node, string path) {
            var names = path.Split('/');
            var current = root;
            for (var i = 0; i < names.Length - 2; i++) {
                var name = names[i];
                if (current.Children == null) {
                    current.Children = new Dictionary<string, ResourceFolder>();
                }
                if (current.Children != null && current.Children.ContainsKey(name)) {
                    current = current.Children[name];
                    continue;
                }
                var child = new ResourceFolder();
                
                current.Children.Add(name, child);
                current = child;
            }
                    
        }

        //
        // public IResourceNode GetNode(string path) {
        //     
        // }
    }
}