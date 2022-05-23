using System.Collections.Generic;
using Coorth.Framework;

namespace Coorth.Worlds; 

public interface IHierarchyNode {
    Entity ParentEntity { get; }
    int ChildCount { get; }
    IEnumerable<Entity> GetChildrenEntities();
}