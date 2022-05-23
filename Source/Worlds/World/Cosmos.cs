using System;
using System.Collections.Generic;
using System.Linq;
using Coorth.Collections;
using Coorth.Framework;

namespace Coorth.Worlds; 

public static class Cosmos {

    private static readonly Dictionary<ActorId, World> worlds = new ();

    private static readonly object locking = new();

    public static IReadOnlyList<World> GetWorlds() {
        lock (locking) {
            return worlds.Values.ToList();
        }
    }

    internal static void Register(World world) {
        lock (locking) {
            worlds.Add(world.ActorId, world);
        }
    }

    internal static void Remove(World world) {
        lock (locking) {
            worlds.Remove(world.ActorId);
        }
    }
    
}