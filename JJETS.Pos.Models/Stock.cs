namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock : Base
    {
        public double AddedPrice { get; set; } = 0;

        public int Quantity { get; set; } = 0;

        public virtual Store Store { get; set; }

        public virtual Item Item { get; set; }

        public virtual ObservableCollection<TransactionItem> TransactionItems { get; set; } = new ObservableCollection<TransactionItem>();
    }
}
