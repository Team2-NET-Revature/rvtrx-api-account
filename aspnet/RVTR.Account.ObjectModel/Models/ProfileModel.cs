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

    private string _email;
    public string Email
    {
      get => _email;
      set
      {
        _email = value;
      }
    }

    private string _familyName;
    public string familyName
    {
      get => _familyName;
      set
      {
        _familyName = value;
      }
    }

    private string _givenName;
    public string givenName
    {
      get => _givenName;
      set
      {
        _givenName = value;
      }
    }

    private string _phone;
    public string Phone
    {
      get => _phone;
      set
      {
        _phone = value;
      }
    }

    private string _type;
    public string Type
    {
      get => _type;
      set
      {
        _type = value;
      }
    }

    public int? AccountId { get; set; }


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
