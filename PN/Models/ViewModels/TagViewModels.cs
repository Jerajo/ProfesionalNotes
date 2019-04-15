using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PN.Models
{
    public class CreateTagViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Desciption { get; set; }

        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Select a Forum")]
        public int SelectedForumId { get; set; }

        public List<Forum> Forums { get; set; }
    }

    public class DetailsTagViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Desciption { get; set; }

        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }

        public List<Forum> Forums { get; set; }
    }
}