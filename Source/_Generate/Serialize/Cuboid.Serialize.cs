//Generate code.
namespace Coorth.Maths;

partial record struct Cuboid {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Cuboid","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Cuboid))]
	public sealed class Cuboid_Formatter : Coorth.Serialize.SerializeFormatter<Cuboid> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Cuboid value) {
			writer.BeginData<Cuboid>(6); {
				//X
				writer.WriteTag(nameof(Cuboid.X), 1); //Field
				writer.WriteFloat32(value.X);
				//Y
				writer.WriteTag(nameof(Cuboid.Y), 2); //Field
				writer.WriteFloat32(value.Y);
				//Z
				writer.WriteTag(nameof(Cuboid.Z), 3); //Field
				writer.WriteFloat32(value.Z);
				//W
				writer.WriteTag(nameof(Cuboid.W), 4); //Field
				writer.WriteFloat32(value.W);
				//H
				writer.WriteTag(nameof(Cuboid.H), 5); //Field
				writer.WriteFloat32(value.H);
				//D
				writer.WriteTag(nameof(Cuboid.D), 6); //Field
				writer.WriteFloat32(value.D);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Cuboid value) {
			reader.BeginData<Cuboid>(); {
				//X
				reader.ReadTag(nameof(Cuboid.X), 1);
				value.X = reader.ReadFloat32();
				//Y
				reader.ReadTag(nameof(Cuboid.Y), 2);
				value.Y = reader.ReadFloat32();
				//Z
				reader.ReadTag(nameof(Cuboid.Z), 3);
				value.Z = reader.ReadFloat32();
				//W
				reader.ReadTag(nameof(Cuboid.W), 4);
				value.W = reader.ReadFloat32();
				//H
				reader.ReadTag(nameof(Cuboid.H), 5);
				value.H = reader.ReadFloat32();
				//D
				reader.ReadTag(nameof(Cuboid.D), 6);
				value.D = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
