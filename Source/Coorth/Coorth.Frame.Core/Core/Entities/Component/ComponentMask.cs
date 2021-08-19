﻿using System;
using System.Runtime.CompilerServices;

namespace Coorth {
    internal struct ComponentMask : IEquatable<ComponentMask> {
        
        private int[] array;

        private int Capacity => array.Length * 32;
        
        public ComponentMask(int length) {
            var arrayCapacity = (int) ((uint) (length - 1 + 32) >> 5);
            this.array = new int[arrayCapacity];
        }
        
        public ComponentMask(ComponentMask other, int length) {
            var arrayCapacity = (int) ((uint) (length - 1 + 32) >> 5);
            this.array = new int[arrayCapacity];
            var minLength = Math.Min(array.Length, other.array.Length);
            Array.Copy(other.array, this.array, minLength);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Get(int index) {
            if ((uint) index >= Capacity) {
                return false;
            }
            return (uint) (this.array[index >> 5] & 1 << index) > 0U;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int index, bool value) {
            if (index >= Capacity) {
                var arrayCapacity = (int) ((uint) (index + 32) >> 5);
                Array.Resize(ref this.array, arrayCapacity);
            }
            int num = 1 << index;
            ref int local = ref this.array[index >> 5];
            if (value) {
                local |= num;
            }
            else {
                local &= ~num;
            }
        }

        public bool Equals(ComponentMask other) {
            if (ReferenceEquals(this.array, other.array)) {
                return true;
            }
            for (var i = 0; i < array.Length; i++) {
                if (array[i] != other.array[i]) {
                    return false;
                }
            }
            if (array.Length > other.array.Length) {
                for (var i = other.array.Length; i < array.Length; i++) {
                    if (array[i] != 0) {
                        return false;
                    }
                }
            } else if (array.Length > other.array.Length) {
                for (var i = array.Length; i < other.array.Length; i++) {
                    if (other.array[i] != 0) {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public bool Contains(ComponentMask other) {
            for (int i = 0; i < other.array.Length; ++i) {
                int local = other.array[i];
                if (local != 0u && (i >= array.Length || (array[i] & local) != local)) {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj) {
            return obj is ComponentMask other && Equals(other);
        }

        public override int GetHashCode() {
            return array.GetHashCode();
        }
    }
}