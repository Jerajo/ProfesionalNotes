namespace PN.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EditionLinesLess")]
    public partial class EditionLinesLess
    {
        public int Id { get; set; }

        public int EditionId { get; set; }

        [Required]
        public string Body { get; set; }

        public virtual Edition Edition { get; set; }
    }
}
