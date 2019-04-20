namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserEditionListView")]
    public partial class UserEditionListView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime Posted { get; set; }

        public int? FavoriteCount { get; set; }

        public int? ReadLaterCout { get; set; }

        public int? HasReadCount { get; set; }

        public int? PositiveVoteCount { get; set; }

        public int? NegativeVoteCount { get; set; }
    }
}
