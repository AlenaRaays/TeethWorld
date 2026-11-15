namespace teethWo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Warehouse = new HashSet<Warehouse>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public int idCategory { get; set; }

        public int idManufacture { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        [Required]
        [StringLength(255)]
        public string consists { get; set; }

        public double price { get; set; }

        [Required]
        [StringLength(255)]
        public string image { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacture Manufacture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse> Warehouse { get; set; }

        [NotMapped]
        public string CurrentImage
        {
            get
            {
                if (image != null)
                {
                    string path = "/Images/" + image;
                    return path;
                }
                return null;
            }
            set { }

        }

        [NotMapped]
        public string AmountWord
        {
            get
            {
                List<Warehouse> warehouse = AppConnect.teethworld.Warehouse.ToList();
                int amount = warehouse.Where(x => x.idProduct == id).Sum(x => x.allowedAmount);

                if (amount > 5)
                {
                    return "много";
                }
                return "мало";

            }
            set { }
        }

        [NotMapped]
        public int CurrentAmo
        {
            get
            {
                List<Warehouse> warehouse = AppConnect.teethworld.Warehouse.ToList();
                int amount = warehouse.Where(x => x.idProduct == id).Sum(x => x.allowedAmount);

                return amount;
            }
            set { }
        }

        [NotMapped]
        public string ColorAmo
        {
            get
            {
                List<Warehouse> warehouse = AppConnect.teethworld.Warehouse.ToList();
                int amount = warehouse.Where(x => x.idProduct == id).Sum(x => x.allowedAmount);

                if (amount <= 3)
                {
                    return "#ff8080";
                }
                return "#ffffff";
            }
            set { }
        }

        [NotMapped]
        public bool CrossedPrice
        {
            get
            {
                List<Order> orders = AppConnect.teethworld.Order.ToList();
                List<Warehouse> warehouses = AppConnect.teethworld.Warehouse.ToList();
                List<OrderWarehouse> warehouseOrders = AppConnect.teethworld.OrderWarehouse.ToList();

                var query = from OrderWarehouse w_order in warehouseOrders
                            join Order ord in orders on w_order.idOrder equals ord.id
                            join Warehouse ware in warehouses on w_order.idWarehouse equals ware.id
                            select new { ware.idProduct, ord.orderDate };

                foreach (var item in query)
                {
                    if (DateTime.Now.Month - item.orderDate.Month > 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            set { }
        }
    }
}
