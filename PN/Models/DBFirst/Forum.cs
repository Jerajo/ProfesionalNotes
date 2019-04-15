namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Forum")]
    public partial class Forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Forum()
        {
            UserForumSubscription = new List<UserForumSubscription>();
            Tag = new List<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Desciption { get; set; }

        public string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = String.IsNullOrEmpty(value) ? "~/Content/images/No-Image.svg" : value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<UserForumSubscription> UserForumSubscription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Tag> Tag { get; set; }
    }
}
