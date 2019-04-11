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
        public string UserId { get; set; }

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

        public int? Score { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(25)]
        public string CountryName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string PostTitle { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime PostTime { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EditionId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string EditionTitle { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime EditionPosted { get; set; }
    }
}
