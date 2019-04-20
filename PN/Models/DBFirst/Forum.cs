namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Forum")]
    public partial class Forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Forum()
        {
            ForumUser = new List<ForumUser>();
            Tag = new List<Tag>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public string Desciption { get; set; }

        [Required]
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value ?? "~/Content/images/No-Image.svg";
            }
        }
        private string _imagePath;

        [Required]
        [StringLength(2)]
        public string ISOLanguage { get; set; }

        public virtual Language Language { get; set; }

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<ForumUser> ForumUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Tag> Tag { get; set; }
    }
}
