using System;
using Coorth.Serialize;

namespace Coorth.Framework; 

public readonly record struct ReactionId {
        
    private readonly Guid id;
        
    public static ReactionId New() => new(Guid.NewGuid());

    private ReactionId(Guid id) => this.id = id;

}