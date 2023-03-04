//Generate code.
namespace Coorth.Framework;

partial struct ActivationComponent {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Framework.ActivableComponent","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(ActivationComponent))]
	public sealed class ActivableComponent_Formatter : Coorth.Serialize.SerializeFormatter<ActivationComponent> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in ActivationComponent value) {
			writer.BeginData<ActivationComponent>(1); {
				//mask
				writer.WriteTag(nameof(ActivationComponent.mask), 1); //Field
				writer.WriteValue<Coorth.Collections.BitMask64>(value.mask);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref ActivationComponent value) {
			reader.BeginData<ActivationComponent>(); {
				//mask
				reader.ReadTag(nameof(ActivationComponent.mask), 1);
				value.mask = reader.ReadValue<Coorth.Collections.BitMask64>();
			}
			reader.EndData();
		}
	}
}
