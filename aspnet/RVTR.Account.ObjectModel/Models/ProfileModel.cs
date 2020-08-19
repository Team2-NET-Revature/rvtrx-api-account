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

    public string Email;
    
    public string familyName;
   
    public string givenName;
    
    public string Phone;

    public string Type;

    public int? AccountId { get; set; }


    public AccountModel Account { get; set; }
     
    /// <summary>
    /// Represents the _Profile_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(Email))
      {
        yield return new ValidationResult("Email cannot be null.");
      }
      if (string.IsNullOrEmpty(familyName))
      {
        yield return new ValidationResult("familyName cannot be null.");
      }
      if (string.IsNullOrEmpty(givenName))
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
