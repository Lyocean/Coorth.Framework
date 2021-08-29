using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class ActorProxy : IActor {
        public readonly ActorRef Ref;

        public void OnActive() {
        }

        public Task OnActiveAsync() {
            return Task.CompletedTask;
        }

        public void Execute(in ActorMail e) {
            Ref.Send(e.message, Ref);
        }

        public Task ExecuteAsync(in ActorMail e) {
            Ref.Send(e.message, Ref);
            return Task.CompletedTask;
        }

        public void DeActive() {
        }

        public Task OnDeActiveAsync() {
            return Task.CompletedTask;
        }
        
    }
}