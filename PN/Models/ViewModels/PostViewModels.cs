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
        public string Body { get; set; }

        [Required]
        [Display(Name = "Select a tag")]
        public int SelectedTadId { get; set; }
        public List<Tag> Tags { get; set; }

        [Required]
        [Display(Name = "Select a place")]
        public int SelectedPlace { get; set; }
        public int PlacesCount { get; set; }
    }

    public class PostTagViewModel
    {
        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Select a tag")]
        public int SelectedTadId { get; set; }
        public List<Tag> Tags { get; set; }

        [Required]
        [Display(Name = "Select a place")]
        public int SelectedPlace { get; set; }
        public int PlacesCount { get; set; }
    }
}