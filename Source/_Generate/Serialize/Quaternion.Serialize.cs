//Generate code.
using Quaternion = System.Numerics.Quaternion;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Quaternion","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Quaternion))]
public sealed class Quaternion_Formatter : Coorth.Serialize.SerializeFormatter<Quaternion> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Quaternion value) {
		writer.BeginData<Quaternion>(4); {
			writer.WriteTag(nameof(Quaternion.X), 1);
			writer.WriteFloat32(value.X);
			writer.WriteTag(nameof(Quaternion.Y), 2);
			writer.WriteFloat32(value.Y);
			writer.WriteTag(nameof(Quaternion.Z), 3);
			writer.WriteFloat32(value.Z);
			writer.WriteTag(nameof(Quaternion.W), 4);
			writer.WriteFloat32(value.W);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Quaternion value) {
		reader.BeginData<Quaternion>(); {
			reader.ReadTag(nameof(Quaternion.X), 1);
			value.X = reader.ReadFloat32();
			reader.ReadTag(nameof(Quaternion.Y), 2);
			value.Y = reader.ReadFloat32();
			reader.ReadTag(nameof(Quaternion.Z), 3);
			value.Z = reader.ReadFloat32();
			reader.ReadTag(nameof(Quaternion.W), 4);
			value.W = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
