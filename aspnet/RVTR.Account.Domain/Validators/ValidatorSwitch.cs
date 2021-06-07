using System.Net.Http;
using RVTR.Account.Domain.Interfaces;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Validators
{
  public class ValidatorSwitch : IValidator
  {
    protected HttpClient client;

    public static async Task<bool> validateAddress(string address)
    {
      return await AddressValidator.getValidation(address);
    }
  } 
}