namespace Coorth {
    public interface IActorSetting {
        int ActorThroughput { get; }
    }

    public class ActorSetting : IActorSetting {
        
        public static readonly ActorSetting Default = new ActorSetting();
        
        public int ActorThroughput => int.MaxValue;
    }
}