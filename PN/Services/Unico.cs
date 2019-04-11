using PN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PN.Services
{
    public class Unico : ValidationAttribute
    {
        public int? Id { get; set; }

        public Unico(string messageError) : base(messageError) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var db = new AppDbContext())
                {
                    //var validateName = 1;//(Id != null) ? db.UserInformation.FirstOrDefault(x => x.UserName == (string)value && x.Id != Id) : db.UserInformation.FirstOrDefault(x => x.UserName == (string)value);

                    //if (validateName != null)
                    //{
                    //    var messageError = FormatErrorMessage(validationContext.MemberName);
                    //    return new ValidationResult(messageError);
                    //}

                }
            }

            return ValidationResult.Success;
        }
    }
}