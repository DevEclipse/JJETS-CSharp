namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Managers")]
    public partial class Manager : User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoidCode { get; set; }

        public virtual ObservableCollection<Store> Stores { get; set; } = new ObservableCollection<Store>();
    }
}
