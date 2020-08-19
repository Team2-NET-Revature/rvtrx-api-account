using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    public int Id { get; set; }

    private string _city;
    public string City
    {
      get => _city;
      set
      {
        _city = value;
      }
    }

    private string _country;
    public string Country
    {
      get => _country;
      set
      {
        _country = value;
      }
    }

    private string _postalcode;
    public string PostalCode
    {
      get => _postalcode;
      set
      {
        _postalcode = value;
      }
    }

    private string _stateprovince;
    public string StateProvince
    {
      get => _stateprovince;
      set
      {
        _stateprovince = value;
      }
    }

    private string _street;
    public string Street
    {
      get => _street;
      set
      {
        _street = value;
      }
    }

    public int AccountId { get; set; }

    public AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(City))
      {
        yield return new ValidationResult("City cannot be null.");
      }
      if (string.IsNullOrEmpty(Country))
      {
        yield return new ValidationResult("Country cannot be null.");
      }
      if (string.IsNullOrEmpty(PostalCode))
      {
        yield return new ValidationResult("PostalCode cannot be null.");
      }
      if (string.IsNullOrEmpty(StateProvince))
      {
        yield return new ValidationResult("StateProvince cannot be null.");
      }
      if (string.IsNullOrEmpty(Street))
      {
        yield return new ValidationResult("Street cannot be null.");
      }
    }
  }
}