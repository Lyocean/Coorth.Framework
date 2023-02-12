//Generate code.
namespace Coorth.Maths;

partial record struct Capsule {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Capsule","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Capsule))]
	public sealed class Capsule_Formatter : Coorth.Serialize.SerializeFormatter<Capsule> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Capsule value) {
			writer.BeginData<Capsule>(3); {
				//Center
				writer.WriteTag(nameof(Capsule.Center), 2); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Center);
				//Radius
				writer.WriteTag(nameof(Capsule.Radius), 3); //Field
				writer.WriteFloat32(value.Radius);
				//Height
				writer.WriteTag(nameof(Capsule.Height), 4); //Field
				writer.WriteFloat32(value.Height);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Capsule value) {
			reader.BeginData<Capsule>(); {
				//Center
				reader.ReadTag(nameof(Capsule.Center), 2);
				value.Center = reader.ReadValue<System.Numerics.Vector3>();
				//Radius
				reader.ReadTag(nameof(Capsule.Radius), 3);
				value.Radius = reader.ReadFloat32();
				//Height
				reader.ReadTag(nameof(Capsule.Height), 4);
				value.Height = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
