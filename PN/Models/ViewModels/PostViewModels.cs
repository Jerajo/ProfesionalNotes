using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PN.Models
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Select a tag")]
        public int SelectedTadId { get; set; }
        public List<Tag> Tags { get; set; }

        [Required]
        [Display(Name = "Select a place")]
        public string SelectedPlace { get; set; }
        public int PlacesCount { get; set; }
    }

    public class HomePostTagViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Posted { get; set; }

        public Tag Tag { get; set; }

        public List<string> TagNames { get; set; }

        public List<PostLink> PostLinks { get; set; }
    }

    public class PostLink
    {
        public string Place { get; set; }
        public string Title { get; set; }
    }
}