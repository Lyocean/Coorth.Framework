//Generate code.
namespace Coorth.Maths;

partial record struct Frustum {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Frustum","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Frustum))]
	public sealed class Frustum_Formatter : Coorth.Serialize.SerializeFormatter<Frustum> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Frustum value) {
			writer.BeginData<Frustum>(6); {
				//Near
				writer.WriteTag(nameof(Frustum.Near), 1); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Near);
				//Far
				writer.WriteTag(nameof(Frustum.Far), 2); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Far);
				//Left
				writer.WriteTag(nameof(Frustum.Left), 3); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Left);
				//Right
				writer.WriteTag(nameof(Frustum.Right), 4); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Right);
				//Top
				writer.WriteTag(nameof(Frustum.Top), 5); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Top);
				//Bottom
				writer.WriteTag(nameof(Frustum.Bottom), 6); //Field
				writer.WriteValue<System.Numerics.Plane>(value.Bottom);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Frustum value) {
			reader.BeginData<Frustum>(); {
				//Near
				reader.ReadTag(nameof(Frustum.Near), 1);
				value.Near = reader.ReadValue<System.Numerics.Plane>();
				//Far
				reader.ReadTag(nameof(Frustum.Far), 2);
				value.Far = reader.ReadValue<System.Numerics.Plane>();
				//Left
				reader.ReadTag(nameof(Frustum.Left), 3);
				value.Left = reader.ReadValue<System.Numerics.Plane>();
				//Right
				reader.ReadTag(nameof(Frustum.Right), 4);
				value.Right = reader.ReadValue<System.Numerics.Plane>();
				//Top
				reader.ReadTag(nameof(Frustum.Top), 5);
				value.Top = reader.ReadValue<System.Numerics.Plane>();
				//Bottom
				reader.ReadTag(nameof(Frustum.Bottom), 6);
				value.Bottom = reader.ReadValue<System.Numerics.Plane>();
			}
			reader.EndData();
		}
	}
}
