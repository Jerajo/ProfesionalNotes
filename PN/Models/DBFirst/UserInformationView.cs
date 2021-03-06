namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInformationView")]
    public partial class UserInformationView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string Sex { get; set; }

        public string ImagePath { get; set; }

        public int? LastPostId { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string ISORegion { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(258)]
        public string NativeRegion { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string ISOLanguage { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(258)]
        public string NativeLanguage { get; set; }
    }
}
