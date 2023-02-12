//Generate code.
namespace Coorth.Maths;

partial record struct Int3 {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Int3","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Int3))]
	public sealed class Int3_Formatter : Coorth.Serialize.SerializeFormatter<Int3> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Int3 value) {
			writer.BeginData<Int3>(3); {
				//X
				writer.WriteTag(nameof(Int3.X), 2); //Field
				writer.WriteInt32(value.X);
				//Y
				writer.WriteTag(nameof(Int3.Y), 3); //Field
				writer.WriteInt32(value.Y);
				//Z
				writer.WriteTag(nameof(Int3.Z), 4); //Field
				writer.WriteInt32(value.Z);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Int3 value) {
			reader.BeginData<Int3>(); {
				//X
				reader.ReadTag(nameof(Int3.X), 2);
				value.X = reader.ReadInt32();
				//Y
				reader.ReadTag(nameof(Int3.Y), 3);
				value.Y = reader.ReadInt32();
				//Z
				reader.ReadTag(nameof(Int3.Z), 4);
				value.Z = reader.ReadInt32();
			}
			reader.EndData();
		}
	}
}
