//Generate code.
using Vector2 = System.Numerics.Vector2;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Vector2","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Vector2))]
public sealed class Vector2_Formatter : Coorth.Serialize.SerializeFormatter<Vector2> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Vector2 value) {
		writer.BeginData<Vector2>(2); {
			writer.WriteTag(nameof(Vector2.X), 1);
			writer.WriteFloat32(value.X);
			writer.WriteTag(nameof(Vector2.Y), 2);
			writer.WriteFloat32(value.Y);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Vector2 value) {
		reader.BeginData<Vector2>(); {
			reader.ReadTag(nameof(Vector2.X), 1);
			value.X = reader.ReadFloat32();
			reader.ReadTag(nameof(Vector2.Y), 2);
			value.Y = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
