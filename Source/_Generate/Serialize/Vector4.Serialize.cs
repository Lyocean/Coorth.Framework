//Generate code.
using Vector4 = System.Numerics.Vector4;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Vector4","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Vector4))]
public sealed class Vector4_Formatter : Coorth.Serialize.SerializeFormatter<Vector4> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Vector4 value) {
		writer.BeginData<Vector4>(4); {
			writer.WriteTag(nameof(Vector4.X), 1);
			writer.WriteFloat32(value.X);
			writer.WriteTag(nameof(Vector4.Y), 2);
			writer.WriteFloat32(value.Y);
			writer.WriteTag(nameof(Vector4.Z), 3);
			writer.WriteFloat32(value.Z);
			writer.WriteTag(nameof(Vector4.W), 4);
			writer.WriteFloat32(value.W);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Vector4 value) {
		reader.BeginData<Vector4>(); {
			reader.ReadTag(nameof(Vector4.X), 1);
			value.X = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector4.Y), 2);
			value.Y = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector4.Z), 3);
			value.Z = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector4.W), 4);
			value.W = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
