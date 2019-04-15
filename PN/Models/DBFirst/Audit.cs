namespace PN.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Audit")]
    public partial class Audit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string UserId { get; set; }

        [Required]
        public string Query { get; set; }

        public DateTime Time { get; set; }

        public int Affect { get; set; }
    }
}
