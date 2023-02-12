//Generate code.
namespace Coorth.Maths;

partial record struct Rectangle {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Rectangle","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Rectangle))]
	public sealed class Rectangle_Formatter : Coorth.Serialize.SerializeFormatter<Rectangle> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Rectangle value) {
			writer.BeginData<Rectangle>(4); {
				//X
				writer.WriteTag(nameof(Rectangle.X), 1); //Field
				writer.WriteFloat32(value.X);
				//Y
				writer.WriteTag(nameof(Rectangle.Y), 2); //Field
				writer.WriteFloat32(value.Y);
				//W
				writer.WriteTag(nameof(Rectangle.W), 3); //Field
				writer.WriteFloat32(value.W);
				//H
				writer.WriteTag(nameof(Rectangle.H), 4); //Field
				writer.WriteFloat32(value.H);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Rectangle value) {
			reader.BeginData<Rectangle>(); {
				//X
				reader.ReadTag(nameof(Rectangle.X), 1);
				value.X = reader.ReadFloat32();
				//Y
				reader.ReadTag(nameof(Rectangle.Y), 2);
				value.Y = reader.ReadFloat32();
				//W
				reader.ReadTag(nameof(Rectangle.W), 3);
				value.W = reader.ReadFloat32();
				//H
				reader.ReadTag(nameof(Rectangle.H), 4);
				value.H = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
