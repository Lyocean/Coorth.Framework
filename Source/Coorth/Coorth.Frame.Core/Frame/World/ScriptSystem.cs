using System.Collections.Generic;

namespace Coorth {
    [System, StoreContract("AECBF415-F47B-4833-8C1D-504E033F34E0")]
    public class ScriptSystem : SystemBase {

        public EventDispatcher GetDispatcher() => this.Dispatcher;
        
        private readonly Queue<IScriptStart> starts = new Queue<IScriptStart>();
        private readonly List<IScriptStepUpdate> stepUpdates = new List<IScriptStepUpdate>();
        private readonly List<IScriptTickUpdate> tickUpdates = new List<IScriptTickUpdate>();
        private readonly List<IScriptLateUpdate> lateUpdates = new List<IScriptLateUpdate>();

        protected override void OnAdd() {
            Subscribe<EventComponentAdd<ScriptComponent>>(Execute);
            Subscribe<EventComponentRemove<ScriptComponent>>(Execute);
            
            Subscribe<EventStepUpdate>(Execute);
            Subscribe<EventTickUpdate>(Execute);
            Subscribe<EventLateUpdate>(Execute);
        }

        private void Execute(EventComponentAdd<ScriptComponent> e) {
            var script = e.Component;
            
            if(script is IScriptStart startScript) {
                starts.Enqueue(startScript);
            }
            if(script is IScriptStepUpdate stepScript) {
                stepUpdates.Add(stepScript);
            }
            if(script is IScriptTickUpdate tickScript) {
                tickUpdates.Add(tickScript);
            }
            if(script is IScriptLateUpdate lateScript) {
                lateUpdates.Add(lateScript);
            }
        }
        
        private void Execute(EventComponentRemove<ScriptComponent> e) {
            var script = e.Component;
            if(script is IScriptStepUpdate stepScript) {
                stepUpdates.Remove(stepScript);
            }
            if(script is IScriptTickUpdate tickScript) {
                tickUpdates.Remove(tickScript);
            }
            if(script is IScriptLateUpdate lateScript) {
                lateUpdates.Remove(lateScript);
            }
            script.Dispose();
        }
        
        private void Execute(EventStepUpdate e) {
            if (stepUpdates.Count == 0) {
                return;
            }

            TempList<IScriptStepUpdate> tempList = default;
            try {
                tempList = new TempList<IScriptStepUpdate>(stepUpdates);
                for (var i = 0; i < tempList.Count; i++) {
                    var script = tempList[i];
                    if (script.IsDisposed || !script.IsEnable) {
                        continue;
                    }
                    script.OnStepUpdate(e);
                }
            }
            finally {
                tempList.Dispose();
            }
        }

        private void InvokeScriptStart() {
            while (starts.Count > 0){
                var start = starts.Dequeue();
                if (start.IsDisposed) {
                    continue;
                }
                if (!start.IsEnable) {
                    starts.Enqueue(start);
                }
            }
        }
        
        private void Execute(EventTickUpdate e) {
            InvokeScriptStart();
            if (tickUpdates.Count == 0) {
                return;
            }
            
            TempList<IScriptTickUpdate> tempList = default;
            try {
                tempList = new TempList<IScriptTickUpdate>(tickUpdates);
                for (var i = 0; i < tempList.Count; i++) {
                    var script = tempList[i];
                    if (script.IsDisposed || !script.IsEnable) {
                        continue;
                    }
                    script.OnTickUpdate(e);
                }
            }
            finally {
                tempList.Dispose();
            }
        }
        
        private void Execute(EventLateUpdate e) {
            if (lateUpdates.Count == 0) {
                return;
            }
            TempList<IScriptLateUpdate> tempList = default;
            try {
                tempList = new TempList<IScriptLateUpdate>(lateUpdates);
                for (var i = 0; i < tempList.Count; i++) {
                    var script = tempList[i];
                    if (script.IsDisposed || !script.IsEnable) {
                        continue;
                    }
                    script.OnLateUpdate(e);
                }
            }
            finally {
                tempList.Dispose();
            }
        }
    }
}