using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public sealed class ReactChannel : IReactionContainer, IDisposable {
    
    public readonly Type Key;

    public readonly List<Reaction> Reactions = new();

    public ReactChannel(Type key) {
        Key = key;
    }

    public void Add(Reaction reaction) {
        Reactions.Add(reaction);
    }

    public void Remove(ReactionId id) {
        Reactions.RemoveAll(_ => _.Id == id);
    }

    public void Dispose() {
        foreach (var reaction in Reactions) {
            reaction.Dispose();
        }
    }
}