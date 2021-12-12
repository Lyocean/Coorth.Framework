namespace Coorth {
    public class AppConfig: IActorConfig {

        public static readonly AppConfig Default = new AppConfig();

        public int ActorThroughput { get; } = 100;
    }
}