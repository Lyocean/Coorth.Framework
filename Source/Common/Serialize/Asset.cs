using System;

namespace Coorth {
    public abstract class Asset : Disposable {
        
        public readonly Guid AssetId;

        protected Asset() {
            this.AssetId = Guid.NewGuid();
        }

        protected Asset(Guid id) {
            this.AssetId = id;
        }
    }

    public interface IAssetData {
        
    }
}