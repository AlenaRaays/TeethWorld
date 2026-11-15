namespace teethWo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderWarehouse")]
    public partial class OrderWarehouse
    {
        public int id { get; set; }

        public int idOrder { get; set; }

        public int idWarehouse { get; set; }

        public virtual Order Order { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
