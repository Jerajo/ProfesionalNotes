namespace PN.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInformation")]
    public partial class UserInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInformation()
        {
            UserForumSubscription = new List<UserForumSubscription>();
            Edition = new List<Edition>();
            Post = new List<Post>();
            Country = new List<Country>();
        }

        public int Id { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        public string ImagePath { get; set; }

        public int? LastPostId { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<UserForumSubscription> UserForumSubscription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Edition> Edition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Post> Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Country> Country { get; set; }
    }
}
