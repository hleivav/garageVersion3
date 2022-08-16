using GarageVersion3.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace GarageVersion3.Web.Validations
{
    public class ToYoungToDrive : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                var viewModel = validationContext.ObjectInstance as MemberCreateViewModel;

                if (viewModel is not null)
                {
                    string currentDate = DateTime.Now.ToString();
                    currentDate = currentDate.Replace("-", string.Empty);
                    currentDate = currentDate.Substring(0,8);
                    if (Int32.Parse(viewModel.PersNrId.Substring(0,8)) > Int32.Parse(currentDate))
                    {
                        return ValidationResult.Success;
                    }
                }
            }


            return base.IsValid(value, validationContext);
        }
    }
}
