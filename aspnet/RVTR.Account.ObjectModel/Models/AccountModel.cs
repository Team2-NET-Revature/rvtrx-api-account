using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Account_ model
  /// </summary>
  public class AccountModel : IValidatableObject
  {
    public int Id { get; set; }

    public AddressModel Address { get; set; }

    private string _name;
    public string Name
    {
      get => _name;
      set
      {
        _name = value;
      }
    }

    public IEnumerable<PaymentModel> Payments { get; set; }

    public IEnumerable<ProfileModel> Profiles { get; set; }

    /// <summary>
    /// Represents the _Account_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(Name))
      {
        yield return new ValidationResult("Account name cannot be null.");
      }
    }
  }
}