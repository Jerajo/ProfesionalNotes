namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Edition")]
    public partial class Edition
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string LinesLess { get; set; }

        public string LinesPluss { get; set; }

        public DateTime Posted { get; set; }

        public virtual Post Post { get; set; }

        public virtual UserInformation UserInformation { get; set; }
    }
}
