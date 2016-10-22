namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin : User
    {
        [Required]
        [StringLength(50)]
        public string SecretPassword { get; set; }
    }
}
