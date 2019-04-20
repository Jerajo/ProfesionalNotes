using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PN.Models
{
    public class CreateForumViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Desciption { get; set; }

        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public UserInformation UserInformation { get; internal set; }
    }

    public class DetailsForumViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Desciption { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }
    }
}