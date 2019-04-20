namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            TagPost = new List<TagPost>();
            TagUser = new List<TagUser>();
            Forum = new List<Forum>();
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

        public virtual UserInformation UserInformation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<TagPost> TagPost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<TagUser> TagUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Forum> Forum { get; set; }
    }
}
