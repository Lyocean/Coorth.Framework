//Generate code.
namespace Coorth.Framework;

partial struct ActivableComponent {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Framework.ActivableComponent","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(ActivableComponent))]
	public sealed class ActivableComponent_Formatter : Coorth.Serialize.SerializeFormatter<ActivableComponent> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in ActivableComponent value) {
			writer.BeginData<ActivableComponent>(1); {
				//mask
				writer.WriteTag(nameof(ActivableComponent.mask), 1); //Field
				writer.WriteValue<Coorth.Collections.BitMask64>(value.mask);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref ActivableComponent value) {
			reader.BeginData<ActivableComponent>(); {
				//mask
				reader.ReadTag(nameof(ActivableComponent.mask), 1);
				value.mask = reader.ReadValue<Coorth.Collections.BitMask64>();
			}
			reader.EndData();
		}
	}
}
