namespace PN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserListView")]
    public partial class UserListView
    {
        public int? Id { get; set; }

        [Key]
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public int? ForumCount { get; set; }

        public int? TagCount { get; set; }

        public int? PostCount { get; set; }

        public int? EditionCount { get; set; }
    }
}
