using PN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PN.Services
{
    public class UserNameEmailValidation : ValidationAttribute
    {
        public UserNameEmailValidation() : base() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string identity = value as string;

            if (identity != null)
            {
                if (identity.IndexOf('@') > -1)
                {
                    //Validate email format
                    string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(identity))
                    {
                        //var messageError = FormatErrorMessage(validationContext.MemberName);
                        return new ValidationResult("The Email isn't in the correct format.");
                    }
                }
                else
                {
                    //validate Username format
                    string emailRegex = @"^[a-zA-Z0-9]{4,10}$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(identity))
                    {
                        //var messageError = FormatErrorMessage(validationContext.MemberName);
                        return new ValidationResult("The User Name needs to be 4 to 10 character.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}