namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customers")]
    public partial class Customer : User
    {
        public double Amount { get; set; } = 0;

        public double Discounts { get; set; } = 0;

        public double Taxes { get; set; } = 0;

        public virtual ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
    }
}
