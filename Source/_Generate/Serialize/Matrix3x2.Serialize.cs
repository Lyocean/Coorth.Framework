//Generate code.
using Matrix3x2 = System.Numerics.Matrix3x2;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Matrix3x2","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Matrix3x2))]
public sealed class Matrix3x2_Formatter : Coorth.Serialize.SerializeFormatter<Matrix3x2> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Matrix3x2 value) {
		writer.BeginData<Matrix3x2>(6); {
			writer.WriteTag(nameof(Matrix3x2.M11), 1);
			writer.WriteFloat32(value.M11);
			writer.WriteTag(nameof(Matrix3x2.M12), 2);
			writer.WriteFloat32(value.M12);
			writer.WriteTag(nameof(Matrix3x2.M21), 3);
			writer.WriteFloat32(value.M21);
			writer.WriteTag(nameof(Matrix3x2.M22), 4);
			writer.WriteFloat32(value.M22);
			writer.WriteTag(nameof(Matrix3x2.M31), 5);
			writer.WriteFloat32(value.M31);
			writer.WriteTag(nameof(Matrix3x2.M32), 6);
			writer.WriteFloat32(value.M32);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Matrix3x2 value) {
		reader.BeginData<Matrix3x2>(); {
			reader.ReadTag(nameof(Matrix3x2.M11), 1);
			value.M11 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix3x2.M12), 2);
			value.M12 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix3x2.M21), 3);
			value.M21 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix3x2.M22), 4);
			value.M22 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix3x2.M31), 5);
			value.M31 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix3x2.M32), 6);
			value.M32 = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
