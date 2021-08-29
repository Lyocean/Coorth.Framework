using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    public interface IAgentMessage : IMessage {
        long AgentId { get; }

        void Setup(long agentId);
    }


    public abstract class AgentMessage : ActorMessage, IAgentMessage {
        public long AgentId { get; private set; }
        
        void IAgentMessage.Setup(long agentId) {
            this.AgentId = agentId;
        }
        
        public override bool ValidateMessage() {
            return AgentId != 0 && base.ValidateMessage();
        }
    }
    
    [Message, DataContract, Guid("EE4B3E99-DFE0-4018-8913-096E8D2DA193")]
    public class MessageCreateEntity : ActorMessage {
        public readonly long AgentId;
        
        public MessageCreateEntity(long agentId) {
            this.AgentId = agentId;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && AgentId != 0;
        }
    }
    
    [Message, DataContract, Guid("4A917442-CB5B-4831-AA70-4C0FD23EF5AD")]
    public class MessageDestroyEntity : ActorMessage {
        public readonly long AgentId;

        public MessageDestroyEntity(long agentId) {
            this.AgentId = agentId;
        }
        
        public override bool ValidateMessage() {
            return AgentId != 0 && base.ValidateMessage();
        }
    }

    [Message, DataContract, Guid("BF941AEC-CD7F-499B-AC33-7A269D931FFC")]
    public class MessageAddComponent : AgentMessage {
        public readonly Type ComponentType;
        
        public MessageAddComponent(Type componentType) {
            this.ComponentType = componentType;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && ComponentType != null;
        }
    }
    
    [Message, DataContract, Guid("832D4F79-78ED-455A-9039-16658514D071")]
    public class MessageRemoveComponent : AgentMessage {
        public readonly Type ComponentType;
        
        public MessageRemoveComponent(Type componentType) {
            this.ComponentType = componentType;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && ComponentType != null;
        }
    }
    
    [Message, DataContract, Guid("C1E7F0FC-CBE1-4AFC-9EF5-DE7C9B031B0D")]
    public class MessageModifyComponent : AgentMessage {
        public readonly Type ComponentType;
        
        public MessageModifyComponent(Type componentType) {
            this.ComponentType = componentType;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && ComponentType != null;
        }
    }

    [Message, DataContract, Guid("A71CF508-C3F1-403D-9636-0FFC9A993FE2")]
    public class MessageAddSystem : ActorMessage {
        public readonly Type ParentType;
        public readonly Type SystemType;
        
        public MessageAddSystem(Type systemType, Type parentType) {
            this.SystemType = systemType;
            this.ParentType = parentType;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && SystemType != null;
        }
    }
    
    [Message, DataContract, Guid("1B594CBC-B17A-484D-BB98-4580936EC240")]
    public class MessageRemoveSystem : ActorMessage {
        public readonly Type SystemType;

        public MessageRemoveSystem(Type systemType) {
            this.SystemType = systemType;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && SystemType != null;
        }
    }
    
    [Message, DataContract, Guid("AD68BF37-CAB7-4665-BE30-CF1D13AAE75C")]
    public class MessageActiveSystem : ActorMessage {
        public readonly Type SystemType;
        public readonly bool IsActive;
        
        public MessageActiveSystem(Type systemType, bool isActive) {
            this.SystemType = systemType;
            this.IsActive = isActive;
        }
        
        public override bool ValidateMessage() {
            return base.ValidateMessage() && SystemType != null;
        }
    }
}