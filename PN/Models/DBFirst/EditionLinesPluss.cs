namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EditionLinesPluss")]
    public partial class EditionLinesPluss
    {
        public int Id { get; set; }

        public int EditionId { get; set; }

        [Required]
        public string Body { get; set; }

        public virtual Edition Edition { get; set; }
    }
}