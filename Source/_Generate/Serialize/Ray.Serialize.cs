//Generate code.
namespace Coorth.Maths;

partial record struct Ray {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Ray","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Ray))]
	public sealed class Ray_Formatter : Coorth.Serialize.SerializeFormatter<Ray> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Ray value) {
			writer.BeginData<Ray>(2); {
				//Position
				writer.WriteTag(nameof(Ray.Position), 2); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Position);
				//Direction
				writer.WriteTag(nameof(Ray.Direction), 3); //Field
				writer.WriteValue<System.Numerics.Vector3>(value.Direction);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Ray value) {
			reader.BeginData<Ray>(); {
				//Position
				reader.ReadTag(nameof(Ray.Position), 2);
				value.Position = reader.ReadValue<System.Numerics.Vector3>();
				//Direction
				reader.ReadTag(nameof(Ray.Direction), 3);
				value.Direction = reader.ReadValue<System.Numerics.Vector3>();
			}
			reader.EndData();
		}
	}
}
