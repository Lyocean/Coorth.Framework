using System.Runtime.Serialization;

namespace Coorth {
    public class AppSetting : IActorSetting {
        public int AppId { get; set; } = 0;

        public string AppName { get; set; } = "App-Default";

        public int ActorThroughput { get; set; } = int.MaxValue;
    }
}