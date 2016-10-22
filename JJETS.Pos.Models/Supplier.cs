namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier : Base
    {
        public virtual ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
    }
}
