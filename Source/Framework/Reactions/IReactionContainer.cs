namespace Coorth.Framework; 

public interface IReactionContainer {
    void Add(Reaction reaction);
    void Remove(ReactionId id);
}