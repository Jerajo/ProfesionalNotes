namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostUser")]
    public partial class PostUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsReadLater { get; set; }

        public bool HasRead { get; set; }

        public bool? Vote { get; set; }

        public virtual Post Post { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
