using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework;

public static class Apps {
    
    private static readonly object locker = new();

    private static readonly HashSet<AppFrame> apps = new();

    private static AppFrame Main {
        get {
            lock (locker) {
                return apps.FirstOrDefault() ?? throw new Exception();
            }
        }
    }

    internal static void AddApp(AppFrame app) {
        lock (locker) {
            apps.Add(app);
        }
    }

    public static AppFrame GetApp(int id) => FindApp(id) ?? throw new KeyNotFoundException(id.ToString());

    public static AppFrame? FindApp(int id) {
        lock (locker) {
            return apps.FirstOrDefault(app => app.Id == id);
        }
    }

    public static AppFrame? FindApp(Guid guid) {
        lock (locker) {
            return apps.FirstOrDefault(app => app.Guid == guid);
        }
    }

    public static AppFrame? FindApp(string name) {
        lock (locker) {
            return apps.FirstOrDefault(app => app.Name == name);
        }
    }

    public static bool TryGetApp(int id, out AppFrame? app) {
        app = FindApp(id);
        return app != null;
    }

    public static bool TryGetApp(Guid guid, out AppFrame? app) {
        app = FindApp(guid);
        return app != null;
    }

    public static AppFrame GetApp(Guid guid) => FindApp(guid) ?? throw new KeyNotFoundException(guid.ToString());

    public static bool TryGetApp(string name, out AppFrame? app) {
        app = FindApp(name);
        return app != null;
    }

    public static AppFrame GetApp(string name) => FindApp(name) ?? throw new KeyNotFoundException(name);

    public static bool RemoveApp(AppFrame app) {
        lock (locker) {
            return apps.Remove(app);
        }
    }
}