namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public sealed partial class Store : Base
    {

        public int? ManagerId { get; set; }

        public int? LocationId { get; set; }

        public double? DiscountRate { get; set; } = 0.01;

        public double? TaxRate { get; set; } = 0.12;

        public StatusStore Status { get; set; } = StatusStore.New;


        public Manager Manager { get; set; }


        public Location Location { get; set; }

        public ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

    }
}
