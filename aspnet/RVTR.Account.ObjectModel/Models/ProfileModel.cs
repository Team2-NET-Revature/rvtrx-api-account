using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Profile_ model
  /// </summary>
  public class ProfileModel : IValidatableObject
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Email address required")]
    [EmailAddress(ErrorMessage = "must be a real email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Family name required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string FamilyName { get; set; }

    [Required(ErrorMessage = "Given name name required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string GivenName { get; set; }

    [Required(ErrorMessage = "Phone number required")]
    [Phone(ErrorMessage = "Must be a phone number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Type is required")]
    [MinLength(1)]
    [MaxLength(50)]
    public string Type { get; set; }

    public int? AccountId { get; set; }

    [Required(ErrorMessage = "Account is required")]
    public AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Profile_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(Email))
      {
        yield return new ValidationResult("Email cannot be null.");
      }
      if (string.IsNullOrEmpty(FamilyName))
      {
        yield return new ValidationResult("familyName cannot be null.");
      }
      if (string.IsNullOrEmpty(GivenName))
      {
        yield return new ValidationResult("givenName cannot be null.");
      }
      if (string.IsNullOrEmpty(Phone))
      {
        yield return new ValidationResult("Phone cannot be null.");
      }
    }
  }
}
