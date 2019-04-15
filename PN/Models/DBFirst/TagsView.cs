namespace PN.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TagsView")]
    public partial class TagsView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ForumId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string ForumName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(15)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Desciption { get; set; }

        public string ImagePath { get; set; }
    }
}
