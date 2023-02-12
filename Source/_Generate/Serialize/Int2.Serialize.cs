//Generate code.
namespace Coorth.Maths;

partial record struct Int2 {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Int2","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Int2))]
	public sealed class Int2_Formatter : Coorth.Serialize.SerializeFormatter<Int2> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Int2 value) {
			writer.BeginData<Int2>(2); {
				//X
				writer.WriteTag(nameof(Int2.X), 2); //Field
				writer.WriteInt32(value.X);
				//Y
				writer.WriteTag(nameof(Int2.Y), 3); //Field
				writer.WriteInt32(value.Y);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Int2 value) {
			reader.BeginData<Int2>(); {
				//X
				reader.ReadTag(nameof(Int2.X), 2);
				value.X = reader.ReadInt32();
				//Y
				reader.ReadTag(nameof(Int2.Y), 3);
				value.Y = reader.ReadInt32();
			}
			reader.EndData();
		}
	}
}
