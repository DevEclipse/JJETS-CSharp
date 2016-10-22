namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employees")]
    public partial class Employee : User
    {

      //  [ForeignKey("Store")]
        public int StoreId { get; set; }

        public double Salary { get; set; } = 0;


        public virtual Store Store { get; set; }
        public virtual ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
    }
}
