//Generate code.
namespace Coorth.Maths;

partial record struct TransformTRS {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.TransformTRS","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(TransformTRS))]
	public sealed class TransformTRS_Formatter : Coorth.Serialize.SerializeFormatter<TransformTRS> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in TransformTRS value) {
			writer.BeginData<TransformTRS>(3); {
				//Position
				writer.WriteTag(nameof(TransformTRS.Position), 2); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Position);
				//Rotation
				writer.WriteTag(nameof(TransformTRS.Rotation), 3); //Field
				writer.WriteValue<System.Numerics.Quaternion>(value.Rotation);
				//Scaling
				writer.WriteTag(nameof(TransformTRS.Scaling), 4); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Scaling);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref TransformTRS value) {
			reader.BeginData<TransformTRS>(); {
				//Position
				reader.ReadTag(nameof(TransformTRS.Position), 2);
				value.Position = reader.ReadValue<System.Numerics.Vector3>();
				//Rotation
				reader.ReadTag(nameof(TransformTRS.Rotation), 3);
				value.Rotation = reader.ReadValue<System.Numerics.Quaternion>();
				//Scaling
				reader.ReadTag(nameof(TransformTRS.Scaling), 4);
				value.Scaling = reader.ReadValue<System.Numerics.Vector3>();
			}
			reader.EndData();
		}
	}
}
