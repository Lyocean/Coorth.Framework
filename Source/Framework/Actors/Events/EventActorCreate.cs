using System;

namespace Coorth.Framework; 

[Event]
public readonly record struct EventActorCreate(ActorRef Ref, Type Type, IActor Actor) : IEvent {
    
    public readonly ActorRef Ref = Ref;
    
    public readonly Type Type = Type;
    
    public readonly IActor Actor = Actor;
}