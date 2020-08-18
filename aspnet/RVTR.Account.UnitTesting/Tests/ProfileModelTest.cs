using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
    public class ProfileModelTest
    {
        public static readonly IEnumerable<Object[]> _profiles = new List<Object[]>
    {
      new object[]
      {
        new ProfileModel()
        {
          Id = 0,
          Email = "email@email.com",
          familyName = "family",
          givenName = "given",
          Phone = "1234567890",
          Type = "Adult",
          AccountId = 0,
          Account = null,
        }
      }
    };

        [Theory]
        [MemberData(nameof(_profiles))]
        public void Test_Create_ProfileModel(ProfileModel profile)
        {
            var validationContext = new ValidationContext(profile);
            var actual = Validator.TryValidateObject(profile, validationContext, null, true);

            Assert.True(actual);
        }

        [Theory]
        [MemberData(nameof(_profiles))]
        public void Test_Validate_ProfileModel(ProfileModel profile)
        {
            var validationContext = new ValidationContext(profile);

            Assert.Empty(profile.Validate(validationContext));
        }
    }
}
