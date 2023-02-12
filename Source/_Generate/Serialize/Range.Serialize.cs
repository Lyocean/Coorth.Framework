//Generate code.
namespace Coorth.Maths;

partial record struct Range {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Maths.Range","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(Range))]
	public sealed class Range_Formatter : Coorth.Serialize.SerializeFormatter<Range> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in Range value) {
			writer.BeginData<Range>(2); {
				//Min
				writer.WriteTag(nameof(Range.Min), 2); //Field
				writer.WriteFloat64(value.Min);
				//Max
				writer.WriteTag(nameof(Range.Max), 3); //Field
				writer.WriteFloat64(value.Max);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref Range value) {
			reader.BeginData<Range>(); {
				//Min
				reader.ReadTag(nameof(Range.Min), 2);
				value.Min = reader.ReadFloat64();
				//Max
				reader.ReadTag(nameof(Range.Max), 3);
				value.Max = reader.ReadFloat64();
			}
			reader.EndData();
		}
	}
}
