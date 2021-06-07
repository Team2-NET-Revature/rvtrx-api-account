using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.Domain.Models;
using RVTR.Account.Domain.Validators;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class AddressValidatorTest
  {
    [Theory]
    [InlineData("alldnalcihuiqic")]
    public void Test_NoResults_AddressValidator(string address)
    {
      Assert.False(ValidatorSwitch.validateAddress(address).Result);
    }

    [Theory]
    [InlineData("Texas")]
    public void Test_BadResult_AddressValidator(string address)
    {
      Assert.False(ValidatorSwitch.validateAddress(address).Result);
    }

    [Theory]
    [InlineData("11730 Plaza America Dr. 2nd Floor Reston, VA")]
    public void Test_GoodResult_AddressValidator(string address)
    {
      Assert.True(ValidatorSwitch.validateAddress(address).Result);
    }
  }
}
