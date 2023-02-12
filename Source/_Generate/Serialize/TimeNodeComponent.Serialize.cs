//Generate code.
namespace Coorth.Framework;

partial struct TimeNodeComponent {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Framework.TimeNodeComponent","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(TimeNodeComponent))]
	public sealed class TimeNodeComponent_Formatter : Coorth.Serialize.SerializeFormatter<TimeNodeComponent> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in TimeNodeComponent value) {
			writer.BeginData<TimeNodeComponent>(3); {
				//TimeScale
				writer.WriteTag(nameof(TimeNodeComponent.TimeScale), 1); //Field
				writer.WriteFloat32(value.TimeScale);
				//DeltaTime
				writer.WriteTag(nameof(TimeNodeComponent.DeltaTime), 2); //Field
				writer.WriteTimeSpan(value.DeltaTime);
				//LastTime
				writer.WriteTag(nameof(TimeNodeComponent.LastTime), 3); //Field
				writer.WriteDateTime(value.LastTime);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref TimeNodeComponent value) {
			reader.BeginData<TimeNodeComponent>(); {
				//TimeScale
				reader.ReadTag(nameof(TimeNodeComponent.TimeScale), 1);
				value.TimeScale = reader.ReadFloat32();
				//DeltaTime
				reader.ReadTag(nameof(TimeNodeComponent.DeltaTime), 2);
				value.DeltaTime = reader.ReadTimeSpan();
				//LastTime
				reader.ReadTag(nameof(TimeNodeComponent.LastTime), 3);
				value.LastTime = reader.ReadDateTime();
			}
			reader.EndData();
		}
	}
}
