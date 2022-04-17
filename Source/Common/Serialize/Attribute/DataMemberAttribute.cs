// using System;
//
// namespace Coorth {
//     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
//     public class DataMemberAttribute : Attribute, IComparable<DataMemberAttribute> {
//
//         public bool IsRequired { get; set; } = false;
//
//         public string? Name { get; set; }
//
//         public int Order { get; set; }
//         
//         public DataMemberAttribute() {
//         }
//         
//         public DataMemberAttribute(int order) {
//             this.Order = order;
//         }
//         
//         public int CompareTo(DataMemberAttribute? other) {
//             if (ReferenceEquals(this, other)) { return 0; }
//             if (ReferenceEquals(null, other)) { return 1; }
//             var indexComparison = Order.CompareTo(other.Order);
//             return indexComparison;
//         }
//     }
// }