namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification : Base
    {
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
