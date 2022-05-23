using System.Collections.Generic;

namespace Coorth.Framework; 

public partial class Sandbox {

    private static readonly List<Sandbox?> sandboxes = new();
        
    private static readonly object locking = new();

    private static void OnCreateSandbox(Sandbox sandbox) {
        lock (locking) {
            for (var i = 0; i < sandboxes.Count; i++) {
                if (sandboxes[i] != null) {
                    continue;
                }
                sandbox.Index = i;
                sandboxes[i] = sandbox;
                return;
            }
            sandbox.Index = sandboxes.Count;
            sandboxes.Add(sandbox);
        }
    }

    private static void OnRemoveSandbox(Sandbox sandbox) {
        lock (locking) {
            sandboxes[sandbox.Index] = default;
            sandbox.Index = -1;
        }
    }

    public static Sandbox[] GetSandboxes() {
        lock (locking) {
            var array = new Sandbox[sandboxes.Count];
            sandboxes.CopyTo(array);
            return array;
        }
    }
        
    public static Sandbox? GetSandbox(int index) {
        lock (locking) {
            if (0 <= index && index < sandboxes.Count) {
                return sandboxes[index];
            }
            return null;
        }
    }

    public static Sandbox GetDefault() {
        var sandbox = GetSandbox(0);
        if (sandbox != null) {
            return sandbox;
        }
        sandbox = new Sandbox(SandboxOptions.Default);
        return sandbox;
    }
}