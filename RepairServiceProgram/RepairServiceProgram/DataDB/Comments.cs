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
    
    public partial class Comments
    {
        public int CommentID { get; set; }
        public string Message { get; set; }
        public int MasterEmployeeID { get; set; }
        public int OrderID { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
