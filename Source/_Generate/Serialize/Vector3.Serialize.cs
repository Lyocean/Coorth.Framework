//Generate code.
using Vector3 = System.Numerics.Vector3;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Vector3","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Vector3))]
public sealed class Vector3_Formatter : Coorth.Serialize.SerializeFormatter<Vector3> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Vector3 value) {
		writer.BeginData<Vector3>(3); {
			writer.WriteTag(nameof(Vector3.X), 1);
			writer.WriteFloat32(value.X);
			writer.WriteTag(nameof(Vector3.Y), 2);
			writer.WriteFloat32(value.Y);
			writer.WriteTag(nameof(Vector3.Z), 3);
			writer.WriteFloat32(value.Z);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Vector3 value) {
		reader.BeginData<Vector3>(); {
			reader.ReadTag(nameof(Vector3.X), 1);
			value.X = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector3.Y), 2);
			value.Y = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector3.Z), 3);
			value.Z = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
