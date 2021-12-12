using System;

namespace Coorth.Common {
    [Component, StoreContract("D118FB05-0CEB-48EE-A45A-E8EFD7ABECC4")]
    public struct ActiveComponent : IComponent {
        [StoreMember(1)] 
        private BitMask64 mask;
        public BitMask64 Mask => mask;

        public void Set(int index, bool value) {
            if (index < 0 || BitMask64.CAPACITY <= index) {
                throw new IndexOutOfRangeException();
            }
            mask[index] = value;
        }

        public readonly bool Get(int index) {
            if (index < 0 || BitMask64.CAPACITY <= index) {
                throw new IndexOutOfRangeException();
            }
            return mask[index];
        }
    }
}