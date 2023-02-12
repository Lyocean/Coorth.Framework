//Generate code.
using Matrix4x4 = System.Numerics.Matrix4x4;
namespace Coorth.Serialize;

[System.CodeDom.Compiler.GeneratedCode("System.Numerics.Matrix4x4","1.0.0")]
[Coorth.Serialize.SerializeFormatter(typeof(Matrix4x4))]
public sealed class Matrix4x4_Formatter : Coorth.Serialize.SerializeFormatter<Matrix4x4> {
	public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Matrix4x4 value) {
		writer.BeginData<Matrix4x4>(16); {
			writer.WriteTag(nameof(Matrix4x4.M11), 1);
			writer.WriteFloat32(value.M11);
			writer.WriteTag(nameof(Matrix4x4.M12), 2);
			writer.WriteFloat32(value.M12);
			writer.WriteTag(nameof(Matrix4x4.M13), 3);
			writer.WriteFloat32(value.M13);
			writer.WriteTag(nameof(Matrix4x4.M14), 4);
			writer.WriteFloat32(value.M14);
			writer.WriteTag(nameof(Matrix4x4.M21), 5);
			writer.WriteFloat32(value.M21);
			writer.WriteTag(nameof(Matrix4x4.M22), 6);
			writer.WriteFloat32(value.M22);
			writer.WriteTag(nameof(Matrix4x4.M23), 7);
			writer.WriteFloat32(value.M23);
			writer.WriteTag(nameof(Matrix4x4.M24), 8);
			writer.WriteFloat32(value.M24);
			writer.WriteTag(nameof(Matrix4x4.M31), 9);
			writer.WriteFloat32(value.M31);
			writer.WriteTag(nameof(Matrix4x4.M32), 10);
			writer.WriteFloat32(value.M32);
			writer.WriteTag(nameof(Matrix4x4.M33), 11);
			writer.WriteFloat32(value.M33);
			writer.WriteTag(nameof(Matrix4x4.M34), 12);
			writer.WriteFloat32(value.M34);
			writer.WriteTag(nameof(Matrix4x4.M41), 13);
			writer.WriteFloat32(value.M41);
			writer.WriteTag(nameof(Matrix4x4.M42), 14);
			writer.WriteFloat32(value.M42);
			writer.WriteTag(nameof(Matrix4x4.M43), 15);
			writer.WriteFloat32(value.M43);
			writer.WriteTag(nameof(Matrix4x4.M44), 16);
			writer.WriteFloat32(value.M44);
		}
		writer.EndData();
	}
	public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Matrix4x4 value) {
		reader.BeginData<Matrix4x4>(); {
			reader.ReadTag(nameof(Matrix4x4.M11), 1);
			value.M11 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M12), 2);
			value.M12 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M13), 3);
			value.M13 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M14), 4);
			value.M14 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M21), 5);
			value.M21 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M22), 6);
			value.M22 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M23), 7);
			value.M23 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M24), 8);
			value.M24 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M31), 9);
			value.M31 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M32), 10);
			value.M32 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M33), 11);
			value.M33 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M34), 12);
			value.M34 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M41), 13);
			value.M41 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M42), 14);
			value.M42 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M43), 15);
			value.M43 = reader.ReadFloat32();
			reader.ReadTag(nameof(Matrix4x4.M44), 16);
			value.M44 = reader.ReadFloat32();
		}
		reader.EndData();
	}
}
