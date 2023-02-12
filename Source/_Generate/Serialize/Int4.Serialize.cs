//Generate code.
namespace Coorth.Maths;

partial record struct Int4 {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Int4","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Int4))]
	public sealed class Int4_Formatter : Coorth.Serialize.SerializeFormatter<Int4> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Int4 value) {
			writer.BeginData<Int4>(4); {
				//X
				writer.WriteTag(nameof(Int4.X), 2); //Field
				writer.WriteInt32(value.X);
				//Y
				writer.WriteTag(nameof(Int4.Y), 3); //Field
				writer.WriteInt32(value.Y);
				//Z
				writer.WriteTag(nameof(Int4.Z), 4); //Field
				writer.WriteInt32(value.Z);
				//W
				writer.WriteTag(nameof(Int4.W), 5); //Field
				writer.WriteInt32(value.W);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Int4 value) {
			reader.BeginData<Int4>(); {
				//X
				reader.ReadTag(nameof(Int4.X), 2);
				value.X = reader.ReadInt32();
				//Y
				reader.ReadTag(nameof(Int4.Y), 3);
				value.Y = reader.ReadInt32();
				//Z
				reader.ReadTag(nameof(Int4.Z), 4);
				value.Z = reader.ReadInt32();
				//W
				reader.ReadTag(nameof(Int4.W), 5);
				value.W = reader.ReadInt32();
			}
			reader.EndData();
		}
	}
}
