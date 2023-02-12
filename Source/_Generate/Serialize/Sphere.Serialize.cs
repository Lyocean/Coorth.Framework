//Generate code.
namespace Coorth.Maths;

partial record struct Sphere {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Sphere","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Sphere))]
	public sealed class Sphere_Formatter : Coorth.Serialize.SerializeFormatter<Sphere> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Sphere value) {
			writer.BeginData<Sphere>(2); {
				//Center
				writer.WriteTag(nameof(Sphere.Center), 2); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Center);
				//Radius
				writer.WriteTag(nameof(Sphere.Radius), 3); //Field
				writer.WriteFloat32(value.Radius);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Sphere value) {
			reader.BeginData<Sphere>(); {
				//Center
				reader.ReadTag(nameof(Sphere.Center), 2);
				value.Center = reader.ReadValue<System.Numerics.Vector3>();
				//Radius
				reader.ReadTag(nameof(Sphere.Radius), 3);
				value.Radius = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
