using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    [System, DataContract, Guid("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
    public class TransformSystem : SystemBase {
        protected override void OnAdd() {
            Subscribe<EventComponentAdd<TransformComponent>>(Execute);
            Subscribe<EventComponentRemove<TransformComponent>>(Execute);
        }
        
        private static void Execute(EventComponentAdd<TransformComponent> e) {
            e.Component.OnAdd();
        }

        private static void Execute(EventComponentRemove<TransformComponent> e) {
            e.Component.OnRemove();
        }
    }
}