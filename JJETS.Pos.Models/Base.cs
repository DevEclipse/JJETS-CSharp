using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JJETS.Pos.Models
{
    public partial class Base
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public byte[] Image { get; set; }

        public User CreatedBy { get; set; }

        public string Description { get; set; }

    }

        #region Status Enums
        public enum StatusUser
        {
            New,
            Offline,
            Online,
            Blocked
        }

        public enum StatusTransaction
        {
            New,
            Paid,
            Pending,
            Voided,
            Returned,
            Suspended,
            Parked,
            Claimed
        }

        public enum StatusStore
        {
            New,
            Open,
            Closed,
        }
        #endregion
}
