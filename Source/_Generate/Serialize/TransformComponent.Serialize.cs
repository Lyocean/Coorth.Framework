//Generate code.
namespace Coorth.Framework;

partial class TransformComponent {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Framework.TransformComponent","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(TransformComponent))]
	public sealed class TransformComponent_Formatter : Coorth.Serialize.SerializeFormatter<TransformComponent> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in TransformComponent value) {
			writer.BeginData<TransformComponent>(3); {
				//localPosition
				writer.WriteTag(nameof(TransformComponent.localPosition), 1); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.localPosition);
				//localRotation
				writer.WriteTag(nameof(TransformComponent.localRotation), 2); //Field
				writer.WriteValue<System.Numerics.Quaternion>(value.localRotation);
				//localScaling
				writer.WriteTag(nameof(TransformComponent.localScaling), 3); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.localScaling);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref TransformComponent? value) {
			value ??= new TransformComponent();
			reader.BeginData<TransformComponent>(); {
				//localPosition
				reader.ReadTag(nameof(TransformComponent.localPosition), 1);
				value.localPosition = reader.ReadValue<System.Numerics.Vector3>();
				//localRotation
				reader.ReadTag(nameof(TransformComponent.localRotation), 2);
				value.localRotation = reader.ReadValue<System.Numerics.Quaternion>();
				//localScaling
				reader.ReadTag(nameof(TransformComponent.localScaling), 3);
				value.localScaling = reader.ReadValue<System.Numerics.Vector3>();
			}
			reader.EndData();
		}
	}
}
