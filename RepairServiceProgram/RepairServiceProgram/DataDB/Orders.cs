//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairServiceProgram.DataDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.Comments = new HashSet<Comments>();
        }
    
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> MasterEmployeeID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int TechID { get; set; }
        public string Description { get; set; }
        public int OrderStatusID { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<int> RepairPartsID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
        public virtual OrdersStatus OrdersStatus { get; set; }
        public virtual RepairParts RepairParts { get; set; }
        public virtual Techs Techs { get; set; }
    }
}
