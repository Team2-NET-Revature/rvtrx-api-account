using System.Net.Http;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Validators
{
  public class ValidatorSwitch
  {
    private static HttpClient client = new HttpClient();

    public static async Task<bool> validateAddress(string address)
    {
      return await AddressValidator.getValidation(address, client);
    }
  } 
}