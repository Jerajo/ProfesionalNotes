namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForumUser")]
    public partial class ForumUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ForumId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public int Score { get; set; }

        public bool IsSubscribed { get; set; }

        public bool IsReadLater { get; set; }

        public bool HasRead { get; set; }

        public bool? Vote { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
