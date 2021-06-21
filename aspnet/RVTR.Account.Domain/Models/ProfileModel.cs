using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.Domain.Models
{
  /// <summary>
  /// Represents the _Profile_ model
  /// </summary>
  public class ProfileModel : AEntity, IValidatableObject
  {

    public bool IsAccountHolder { get; set;}
    public bool IsActive { get; set; }


    [Required(ErrorMessage = "Email address required")]
    [EmailAddress(ErrorMessage = "must be a real email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Family name required")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Family name can only use letters and cannot be an empty string.")]
    public string FamilyName { get; set; }

    [Required(ErrorMessage = "Given name name required")]
    [MaxLength(50, ErrorMessage = "Max length of 50 characters")]
    [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Given name can only use letters and cannot be an empty string.")]
    public string GivenName { get; set; }

    [Required(ErrorMessage = "Phone number required")]
    [Phone(ErrorMessage = "Must be a phone number")]
    public string Phone { get; set; }

  
    public string Type { get; set; }

    public int AccountModelId { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime DateOfBirth { get; set; }
    public bool IsAdult { get; set; }
   
    

    /// <summary>
    /// Checks Age by Year and (Month and day)
    /// </summary>
    public void SetAge(DateTime birthDate)
    {
      var adultAge = 18;
      var now = DateTime.Today;
      var age = now.Year - birthDate.Year;
      if (birthDate.Date > now.AddYears(-age))
      {
        age--;
      }
      if (age < adultAge)
      {
        Type = "Minor";
        IsAdult = false;
      }
      else
      {
        Type = "Adult";
        IsAdult = true;
      }


    }

    [RegularExpression(@"^(http(s?):\/\/)[^\s]*$", ErrorMessage = "Image URI must be a real image URI.")]
    public string ImageUri { get; set; } = "https://bulma.io/images/placeholders/256x256.png"; //Default is bulma placeholder

    /// <summary>
    /// Represents the _Profile_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      SetAge(DateOfBirth);
      if (GivenName == FamilyName)
      {
        yield return new ValidationResult("Given name and Family name can't be the same.");
      }
    }
  }
}
