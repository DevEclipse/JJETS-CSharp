using System.Collections.ObjectModel;

namespace JJETS.Pos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Cryptography.X509Certificates;

    public partial class User : Base
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public double? Balance { get; set; } = 0;

        public DateTime? LogInTime { get; set; }

        public DateTime? LogOutTime { get; set; }

        public int? HoursOnline { get; set; }

        public int? Retries { get; set; } = 3;

        public DateTime PenaltyTime { get; set; } = DateTime.Now;

        public StatusUser? Status { get; set; } = StatusUser.New;

        [StringLength(5)]
        public string VerificationCode { get; set; }
        
        public int? ContactNumber { get; set; }

        public virtual ObservableCollection<User> Subscribers { get; set; } = new ObservableCollection<User>();
        public virtual ObservableCollection<Notification> Notifications { get; set; } = new ObservableCollection<Notification>();

    }
}
