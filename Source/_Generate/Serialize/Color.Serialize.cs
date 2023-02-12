//Generate code.
namespace Coorth.Maths;

partial struct Color {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Color","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Color))]
	public sealed class Color_Formatter : Coorth.Serialize.SerializeFormatter<Color> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Color value) {
			writer.BeginData<Color>(4); {
				//R
				writer.WriteTag(nameof(Color.R), 1); //Field
				writer.WriteFloat32(value.R);
				//G
				writer.WriteTag(nameof(Color.G), 2); //Field
				writer.WriteFloat32(value.G);
				//B
				writer.WriteTag(nameof(Color.B), 3); //Field
				writer.WriteFloat32(value.B);
				//A
				writer.WriteTag(nameof(Color.A), 4); //Field
				writer.WriteFloat32(value.A);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Color value) {
			reader.BeginData<Color>(); {
				//R
				reader.ReadTag(nameof(Color.R), 1);
				value.R = reader.ReadFloat32();
				//G
				reader.ReadTag(nameof(Color.G), 2);
				value.G = reader.ReadFloat32();
				//B
				reader.ReadTag(nameof(Color.B), 3);
				value.B = reader.ReadFloat32();
				//A
				reader.ReadTag(nameof(Color.A), 4);
				value.A = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
