//Generate code.
namespace Coorth.Tasks.Ticking;

partial class TickSetting {
	[System.CodeDom.Compiler.GeneratedCode("Coorth.Tasks.Ticking.TickSetting","1.0.0")]
	[Coorth.Serialize.SerializeFormatter(typeof(TickSetting))]
	public sealed class TickSetting_Formatter : Coorth.Serialize.SerializeFormatter<TickSetting> {
		public override void SerializeWriting(in Coorth.Serialize.SerializeWriter writer, scoped in TickSetting value) {
			writer.BeginData<TickSetting>(4); {
				//MaxStepPerFrame
				writer.WriteTag(nameof(TickSetting.MaxStepPerFrame), 1); //Property
				writer.WriteInt32(value.MaxStepPerFrame);
				//StepFrameRate
				writer.WriteTag(nameof(TickSetting.StepFrameRate), 2); //Property
				writer.WriteFloat32(value.StepFrameRate);
				//TickFrameRate
				writer.WriteTag(nameof(TickSetting.TickFrameRate), 3); //Property
				writer.WriteFloat32(value.TickFrameRate);
				//TimeScale
				writer.WriteTag(nameof(TickSetting.TimeScale), 4); //Property
				writer.WriteFloat32(value.TimeScale);
			}
			writer.EndData();
		}
		public override void SerializeReading(in Coorth.Serialize.SerializeReader reader, scoped ref TickSetting? value) {
			value ??= new TickSetting();
			reader.BeginData<TickSetting>(); {
				//MaxStepPerFrame
				reader.ReadTag(nameof(TickSetting.MaxStepPerFrame), 1);
				value.MaxStepPerFrame = reader.ReadInt32();
				//StepFrameRate
				reader.ReadTag(nameof(TickSetting.StepFrameRate), 2);
				value.StepFrameRate = reader.ReadFloat32();
				//TickFrameRate
				reader.ReadTag(nameof(TickSetting.TickFrameRate), 3);
				value.TickFrameRate = reader.ReadFloat32();
				//TimeScale
				reader.ReadTag(nameof(TickSetting.TimeScale), 4);
				value.TimeScale = reader.ReadFloat32();
			}
			reader.EndData();
		}
	}
}
