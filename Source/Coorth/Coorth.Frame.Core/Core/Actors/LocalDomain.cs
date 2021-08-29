namespace Coorth {
    public class LocalDomain : ActorDomain {
        public override ActorRef GetRef() {

            return default;
        } 
    }

    public class WorldDomain : LocalDomain {
        
    }
}