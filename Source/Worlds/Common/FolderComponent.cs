using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Coorth.Framework;

namespace Coorth.Framework; 

[Serializable, StoreContract]
[Component, Guid("4276C354-293B-4554-9B9E-7C224CEB6B56")]
public class FolderComponent : Component {
        
    private readonly List<EntityFolder> folders = new();
        
    private readonly Dictionary<string, EntityFolder> name2Folder = new();
        
    private readonly Dictionary<EntityId, EntityFolder> id2Folder = new();

    private void ValidateEntity(Entity entity) {
        if (!ReferenceEquals(entity.World, this.World)) {
            throw new ArgumentException("Can't add entity cross world.");
        }
    }
        
    public void AddFolder(string name) {
        var folder = new EntityFolder(name);
        folders.Add(folder);
        name2Folder.Add(name, folder);
    }
        
    public string? GetFolder(Entity entity) {
        ValidateEntity(entity);
        return id2Folder.TryGetValue(entity.Id, out var folder) ? folder.Name : null;
    }

    public bool RemoveFolder(string name, bool destroyEntity = false) {
        var index = folders.FindIndex(folder => folder.Name == name);
        if (index < 0) {
            return false;
        }
        var folder = folders[index];
        folders.RemoveAt(index);
        name2Folder.Remove(name);
        foreach (var entityId in folder.Entities) {
            id2Folder.Remove(entityId);
            if (destroyEntity) {
                World.DestroyEntity(entityId);
            }
        }
        return true;
    }
        
    public bool AddEntity(Entity entity, string name) {
        ValidateEntity(entity);
        if (!name2Folder.TryGetValue(name, out var folder)) {
            throw new KeyNotFoundException($"Folder with name:{name} not exist.");
        }
        if (!folder.Entities.Add(entity.Id)) {
            return false;
        }
        id2Folder.Add(entity.Id, folder);
        return true;
    }

    public IEnumerable<Entity> GetEntities(string name) {
        var index = folders.FindIndex(folder => folder.Name == name);
        if (index < 0) {
            yield break;
        }
        var folder = folders[index];
        foreach (var id in folder.Entities) {
            yield return new Entity(World, id);
        }
    }

    public bool HasEntity(Entity entity) {
        ValidateEntity(entity);
        return id2Folder.ContainsKey(entity.Id);
    }

    public bool RemoveEntity(Entity entity) {
        ValidateEntity(entity);
        return id2Folder.TryGetValue(entity.Id, out var folder) && folder.Entities.Remove(entity.Id);
    }
        
    private class EntityFolder {
        public readonly string Name;
        public readonly HashSet<EntityId> Entities = new HashSet<EntityId>();

        public EntityFolder(string name) {
            Name = name;
        }
    }
}