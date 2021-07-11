using System;
using System.Collections.Generic;

namespace Coorth {
	
	public partial class Sandbox {

		#region Component_1
		//
		// public void ForEach<TComponent>(Action<TComponent> action) where TComponent : class, IComponent {
		// 	var typeId = Components.GetTypeId<TComponent>();
		// 	var group = components.GetGroup<TComponent>(typeId, this);
		//
		// 	for (var i = 0; i < group.Count; i++) {
		// 		if (group.mapping[i] != 0) {
		// 			action(group.components[i]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<TEvent, TComponent>(TEvent e, Action<TEvent, Entity, TComponent> action) where TComponent : class, IComponent {
		// 	var typeId = Components.GetTypeId<TComponent>();
		// 	var group = (ComponentGroup<TComponent>)components.GetGroup<TComponent>(typeId, this);
		// 	for (var i = 0; i < group.Count; i++) {
		// 		var index = group.mapping[i];
		// 		if (index != 0) {
		// 			ref var context = ref entities.GetContext(index);
		// 			action(e, context.GetEntity(this), group.components[i]);
		// 		}
		// 	}
		// }

		// public void ForEach<T>(EventActionR<T> action) where T : struct, IComponent {
		// 	var typeId = Components.GetTypeId<T>();
		// 	var group = (ValComponentGroup<T>)components.GetGroup<T>(typeId, this);
		// 	for (var i = 0; i < group.Count; i++) {
		// 		if (group.entts[i] != 0) {
		// 			action(ref group.items[i]);
		// 		}
		// 	}
		// }
		
		// public void ForEach<TEvent, TComponent>(TEvent e, ComponentEventAction<TEvent, TComponent> action) where TEvent: IEvent where TComponent : class, IComponent {
		// 	var typeId = Components.GetTypeId<TComponent>();
		// 	var group = (RefComponentGroup<TComponent>)components.GetGroup<TComponent>(typeId, this);
		// 	for (var i = 0; i < group.Count; i++) {
		// 		var index = group.entts[i];
		// 		if (index != 0) {
		// 			ref var data = ref entities.GetData(index);
		// 			action(e, data.Entity, ref group.items[i]);
		// 		}
		// 	}
		// }

		#endregion
		
		#region Component_2
		
		// public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : class, IComponent where T2 : class, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var group1 = (RefComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (RefComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.entts[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var data = ref entities.GetData(index);
		// 		if (data.Components.TryGetValue(typeId2, out var compIndex2)) {
		// 			action(group1.items[i], group2.items[compIndex2]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<TEvent, T1, T2>(TEvent evt, Action<TEvent, T1, T2> action) where T1 : class, IComponent where T2 : class, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var group1 = components.GetGroup<T1>(typeId1, this);
		// 	var group2 = components.GetGroup<T2>(typeId2, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.mapping[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var data = ref entities.GetContext(index);
		// 		if (data.TryGet(typeId2, out var compIndex2)) {
		// 			action(evt, group1.components[i], group2.components[compIndex2]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<TEvent, T1, T2>(TEvent evt, Action<TEvent, Entity, T1, T2> action) where T1 : class, IComponent where T2 : class, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var group1 = (ComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (ComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.mapping[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var context = ref entities.GetContext(index);
		// 		if (context.TryGet(typeId2, out var compIndex2)) {
		// 			action(evt, context.GetEntity(this), group1.components[i], group2.components[compIndex2]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<T1, T2>(ComponentAction<T1, T2> action) where T1 : struct, IComponent where T2 : struct, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var group1 = (ValComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (ValComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.entts[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var data = ref entities.GetData(index);
		// 		if (data.Components.TryGetValue(typeId2, out var compIndex2)) {
		// 			action(ref group1.items[i], ref group2.items[compIndex2]);
		// 		}
		// 	}
		// }
		
		#endregion

		#region Component_3
		//
		// public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : class, IComponent where T2 : class, IComponent where T3 : class, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var typeId3 = Components.GetTypeId<T3>();
		// 	var group1 = (ComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (ComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	var group3 = (ComponentGroup<T3>)components.GetGroup<T3>(typeId3, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.mapping[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var context = ref entities.GetContext(index);
		// 		if (context.TryGet(typeId2, out var compIndex2)
		// 		    && context.TryGet(typeId3, out var compIndex3)) {
		// 			action(group1.components[i], group2.components[compIndex2], group3.components[compIndex3]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<TE, T1, T2, T3>(TE e, Action<TE, Entity, T1, T2, T3> action) where T1 : class, IComponent where T2 : class, IComponent where T3 : class, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var typeId3 = Components.GetTypeId<T3>();
		// 	var group1 = (RefComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (RefComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	var group3 = (RefComponentGroup<T3>)components.GetGroup<T3>(typeId3, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.entts[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var data = ref entities.GetData(index);
		// 		if (data.Components.TryGetValue(typeId2, out var compIndex2)
		// 		    && data.Components.TryGetValue(typeId3, out var compIndex3)) {
		// 			action(e, data.Entity, group1.items[i], group2.items[compIndex2], group3.items[compIndex3]);
		// 		}
		// 	}
		// }
		//
		// public void ForEach<T1, T2, T3>(ComponentAction<T1, T2, T3> action) where T1 : struct, IComponent where T2 : struct, IComponent where T3 : struct, IComponent {
		// 	var typeId1 = Components.GetTypeId<T1>();
		// 	var typeId2 = Components.GetTypeId<T2>();
		// 	var typeId3 = Components.GetTypeId<T3>();
		// 	var group1 = (ComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
		// 	var group2 = (ComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
		// 	var group3 = (ComponentGroup<T3>)components.GetGroup<T3>(typeId3, this);
		// 	for (var i = 0; i < group1.Count; i++) {
		// 		var index = group1.mapping[i];
		// 		if (index == 0) {
		// 			continue;
		// 		}
		// 		ref var context = ref entities.GetContext(index);
		// 		if (context.TryGet(typeId2, out var compIndex2)
		// 		    && context.TryGet(typeId3, out var compIndex3)) {
		// 			action(ref group1.components.Ref(i), ref group2.components.Ref(compIndex2), ref group3.components.Ref(compIndex3));
		// 		}
		// 	}
		// }

		#endregion
		
		#region Component_N
		
		#endregion
	}
}
