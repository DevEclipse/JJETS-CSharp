namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location : Base
    {
        public virtual ObservableCollection<Store> Store { get; set; } = new ObservableCollection<Store>();
    }
}
