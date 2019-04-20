namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForumUserActivityView")]
    public partial class ForumUserActivityView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Desciption { get; set; }

        [Key]
        [Column(Order = 4)]
        public string ImagePath { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string ISOLanguage { get; set; }

        public int? SubscriptionCount { get; set; }

        public int? ReadLaterCout { get; set; }

        public int? HasReadCount { get; set; }

        public int? PositiveVoteCount { get; set; }

        public int? NegativeVoteCount { get; set; }
    }
}