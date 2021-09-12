using System;

namespace Coorth {
    public class LocalDomain : ActorDomain<LocalContext> {

        public ActorRef CreateActor<TActor>(TActor actor, string name) where TActor : class, IActor {
            var id = ActorId.New();
            var path = this.Path + "/" + (string.IsNullOrEmpty(name) ? id.ToShortString() : name);
            var mailbox = new ActorMailbox();
            var context = new LocalContext(id, this, actor, mailbox, new ActorPath(path));

            Context.AddChild(context);
            
            return context.Ref;
        }
        
        public ActorRef CreateActor<TActor>(string name) where TActor : class, IActor {
            if (typeof(TActor).IsInterface || typeof(TActor).IsAbstract) {
                var actor = TypeBinding.Create<TActor>();
                return CreateActor<TActor>(actor, name);
            }
            else {
                var actor = Activator.CreateInstance<TActor>();
                return CreateActor<TActor>(actor, name);
            }         
        }
        
        public void RemoveActor(ActorId id) {
            Context.RemoveChild(id);
        }
        
        public void RemoveActor(ActorRef actorRef) {
            Context.RemoveChild(actorRef.Id);
        }
    }
}