using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Coorth.Framework;

public partial class World {

    private readonly Stack<int> spaceIndex = new();

    internal readonly Dictionary<Guid, Space> spaces = new();
    public IReadOnlyDictionary<Guid, Space> Spaces => spaces;

    private readonly Space rootSpace;
    public Space RootSpace => rootSpace;
    
    private Space mainSpace;
    public Space MainSpace => mainSpace;

    private void SetupSpaces(out Space root, out Space main) {
        root = CreateSpace("Space:Root");
        main = root;
    }

    private void ClearSpaces() {
        var index = 0;
        var values = new Space[spaces.Count];
        foreach (var (_, space) in spaces) {
            values[index] = space;
            index++;
        }
        foreach (var space in values) {
            space.Dispose();
        }
        spaces.Clear();
    }

    public void SetMain(Space space) {
        Debug.Assert(ReferenceEquals(space.World, this));
        mainSpace = space;
    }

    public Space CreateSpace(string? name = null) {
        var id = Guid.NewGuid();
        name ??= $"Space:{id.ToString()}";
        var index = spaceIndex.TryPop(out var value) ? value : spaces.Count;
        var space = new Space(this, id, index, name);
        spaces.Add(id, space);
        return space;
    }

    public Space GetSpace(Guid id) {
        return spaces[id];
    }

    public Space? FindSpace(Guid id) {
        return spaces.TryGetValue(id, out var space) ? space : null;
    }

    public bool HasSpace(Guid id) {
        return spaces.ContainsKey(id);
    }

    public bool HasSpace(string name) {
        foreach (var (id, space) in spaces) {
            if (space.Name == name) {
                return true;
            }
        }
        return false;
    }

    public bool HasSpace(Space space) {
        return spaces.ContainsKey(space.Id);
    }

    public bool RemoveSpace(Space space) {
        Debug.Assert(ReferenceEquals(space.World, this));
        return RemoveSpace(space.Id);
    }
    
    public bool RemoveSpace(Guid id) {
        var space = FindSpace(id);
        if (space == null) {
            return false;
        }
        space.Dispose();
        return true;
    }
}