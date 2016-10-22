namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction : Base
    {
        public double? Total { get; set; } = 0;

        public double? Amount { get; set; } = 0;

        public double? Change { get; set; } = 0;

        public double? Tax { get; set; } = 0;

        public double? Discount { get; set; } = 0;

        public int? StoreId { get; set; }


        public int? EmployeeId { get; set; }

        public int? CustomerId { get; set; }

        public StatusTransaction Status { get; set; } = StatusTransaction.New;


        public virtual Store Store { get; set; }

        public virtual Customer Customer { get; set; }


        public virtual Employee Employee { get; set; }

        public virtual ObservableCollection<TransactionItem> TransactionItems { get; set; } = new ObservableCollection<TransactionItem>();
    }
}
