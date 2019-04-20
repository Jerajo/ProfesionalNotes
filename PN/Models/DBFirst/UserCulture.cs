namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCulture")]
    public partial class UserCulture
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string ISORegion { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string ISOLanguage { get; set; }

        public virtual Country Country { get; set; }

        public virtual Language Language { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
