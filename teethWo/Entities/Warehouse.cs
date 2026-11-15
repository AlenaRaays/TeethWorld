namespace teethWo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Warehouse")]
    public partial class Warehouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Warehouse()
        {
            OrderWarehouse = new HashSet<OrderWarehouse>();
        }

        public int id { get; set; }

        public int idProduct { get; set; }

        public int idSize { get; set; }

        public int allowedAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderWarehouse> OrderWarehouse { get; set; }

        public virtual Product Product { get; set; }

        public virtual Size Size { get; set; }
    }
}
