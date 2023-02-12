//Generate code.
namespace Coorth.Maths;

partial record struct Size2 {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Size2","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Size2))]
	public sealed class Size2_Formatter : Coorth.Serialize.SerializeFormatter<Size2> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Size2 value) {
			writer.BeginData<Size2>(2); {
				//W
				writer.WriteTag(nameof(Size2.W), 2); //Field
				writer.WriteInt32(value.W);
				//H
				writer.WriteTag(nameof(Size2.H), 3); //Field
				writer.WriteInt32(value.H);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Size2 value) {
			reader.BeginData<Size2>(); {
				//W
				reader.ReadTag(nameof(Size2.W), 2);
				value.W = reader.ReadInt32();
				//H
				reader.ReadTag(nameof(Size2.H), 3);
				value.H = reader.ReadInt32();
			}
			reader.EndData();
		}
	}
}
