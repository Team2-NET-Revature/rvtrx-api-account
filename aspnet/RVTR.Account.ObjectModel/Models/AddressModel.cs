using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    
    private string _city;
    public string City
    {
      get => _city;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("City cannot be null.", nameof(value));
        }
        _city = value;
      }
    }

    
    private string _country;
    public string Country
    {
      get => _country;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("Country cannot be null.", nameof(value));
        }
        _country = value;
      }
    }

    
    private string _postalCode;
    public string PostalCode
    {
      get => _postalCode;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("Postal Code cannot be null.", nameof(value));
        }
        _postalCode = value;
      }
    }

    
    private string _stateProvince;
    public string StateProvince
    {
      get => _stateProvince;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("State cannot be null.", nameof(value));
        }
        _stateProvince = value;
      }
    }

    
    private string _street;
    public string Street
    {
      get => _street;
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("Street cannot be null.", nameof(value));
        }
        _street = value;
      }
    }

    [ForeignKey("Account")]
    [Required]
    public int? AccountId { get; set; }

    
    public AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
