//Generate code.
using Plane = System.Numerics.Plane;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Plane","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Plane))]
public sealed class Plane_Formatter : Coorth.Serialize.SerializeFormatter<Plane> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Plane value) {
		writer.BeginData<Plane>(2); {
			writer.WriteTag(nameof(Plane.Normal), 1);
			writer.WriteValue<System.Numerics.Vector3>(value.Normal);
			writer.WriteTag(nameof(Plane.D), 2);
			writer.WriteFloat32(value.D);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Plane value) {
		reader.BeginData<Plane>(); {
			reader.ReadTag(nameof(Plane.Normal), 1);
			value.Normal = reader.ReadValue<System.Numerics.Vector3>();
			reader.ReadTag(nameof(Plane.D), 2);
			value.D = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
