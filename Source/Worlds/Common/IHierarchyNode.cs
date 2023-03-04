using System.Collections.Generic;
using Coorth.Framework;

namespace Coorth.Framework; 

public interface IHierarchyNode {
    Entity ParentEntity { get; }
    int ChildCount { get; }
    IEnumerable<Entity> GetChildrenEntities();
}