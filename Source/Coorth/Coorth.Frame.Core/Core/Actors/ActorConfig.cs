namespace Coorth {
    public interface IActorConfig {
        int ActorThroughput { get; }
    }

    public class ActorConfig : IActorConfig {
        
        public static readonly ActorConfig Default = new ActorConfig();
        
        public int ActorThroughput => 100;
    }
}