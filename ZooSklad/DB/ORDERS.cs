//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZooSklad.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDERS
    {
        public int Id_Order { get; set; }
        public int Id_Product { get; set; }
        public System.DateTime Date_Order { get; set; }
        public int Amt_Order { get; set; }
        public int Id_Client { get; set; }
        public string Status_Order { get; set; }
    
        public virtual CLIENT CLIENT { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
