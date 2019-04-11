using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PN.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public int Id { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string Description { get; set; }

        [StringLength(25)]
        public string Sex { get; set; }

        public string ImagePath { get; set; }

        public int LastPostId { get; set; }

        [StringLength(25)]
        public string Status { get; set; }

        public Country Country { get; set;  }

        public List<UserForumSubscription> UserForumSubscription { get; set; }

        public List<Edition> Edition { get; set; }

        public List<Post> Post { get; set; }
    }
}