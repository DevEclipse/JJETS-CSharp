namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TransactionItem : Base
    {

        public int Quantity { get; set; } = 1;

        public virtual Stock Stock { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
