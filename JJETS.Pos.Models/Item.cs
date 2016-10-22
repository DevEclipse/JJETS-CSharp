namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item : Base
    {
        public double CostPrice { get; set; } = 0;

        public double RetailPrice { get; set; } = 0;


        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }


        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
    }
}
