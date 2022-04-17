using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth {
    public abstract partial class AppFrame {
        
        private static readonly object locker = new();

        private static readonly List<AppFrame> apps = new();

        private static AppFrame Main { get { lock (locker) { return apps.FirstOrDefault() ?? throw new Exception(); } } }

        private static void AddApp(AppFrame app) {
            lock (locker) {
                Infra.Instance.AddChild(app.services);
                app.Index = apps.Count;
                apps.Add(app);
            }
        }

        public static AppFrame? TryGetApp(int id) {
            lock (locker) {
                foreach (var app in apps) {
                    if (app.Id == id) {
                        return app;
                    }
                }
                return null;
            }
        }

        public static bool TryGetApp(int id, out AppFrame? app) {
            app = TryGetApp(id);
            return app != null;
        }
        
        public static AppFrame GetApp(int id) => TryGetApp(id) ?? throw new KeyNotFoundException(id.ToString());
       
        public static AppFrame? TryGetApp(Guid guid) {
            lock (locker) {
                foreach (var app in apps) {
                    if (app.Guid == guid) {
                        return app;
                    }
                }
                return null;
            }
        }
        
        public static bool TryGetApp(Guid guid, out AppFrame? app) {
            app = TryGetApp(guid);
            return app != null;
        }
        
        public static AppFrame GetApp(Guid guid) => TryGetApp(guid) ?? throw new KeyNotFoundException(guid.ToString());

        public static AppFrame? TryGetApp(string name) {
            lock (locker) {
                foreach (var app in apps) {
                    if (app.Name == name) {
                        return app;
                    }
                }
                return null;
            }
        }

        public static bool TryGetApp(string name, out AppFrame? app) {
            app = TryGetApp(name);
            return app != null;
        }
        
        public static AppFrame GetApp(string name) => TryGetApp(name) ?? throw new KeyNotFoundException(name);
        
        public static bool RemoveApp(AppFrame app) {
            lock (locker) {
                if (!apps.Remove(app)) {
                    return false;
                }
                Infra.Instance.RemoveChild(app.services);
                for (var i = 0; i < apps.Count; i++) {
                    apps[i].Index = i;
                }
                return true;
            }
        }

    }
}