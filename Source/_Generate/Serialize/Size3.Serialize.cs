//Generate code.
namespace Coorth.Maths;

partial record struct Size3 {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Size3","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Size3))]
	public sealed class Size3_Formatter : Coorth.Serialize.SerializeFormatter<Size3> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Size3 value) {
			writer.BeginData<Size3>(3); {
				//X
				writer.WriteTag(nameof(Size3.X), 1); //Field
				writer.WriteInt32(value.X);
				//Y
				writer.WriteTag(nameof(Size3.Y), 2); //Field
				writer.WriteInt32(value.Y);
				//Z
				writer.WriteTag(nameof(Size3.Z), 3); //Field
				writer.WriteInt32(value.Z);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Size3 value) {
			reader.BeginData<Size3>(); {
				//X
				reader.ReadTag(nameof(Size3.X), 1);
				value.X = reader.ReadInt32();
				//Y
				reader.ReadTag(nameof(Size3.Y), 2);
				value.Y = reader.ReadInt32();
				//Z
				reader.ReadTag(nameof(Size3.Z), 3);
				value.Z = reader.ReadInt32();
			}
			reader.EndData();
		}
	}
}
