using System;
using System.Net.Http;
using System.Threading.Tasks;
using RVTR.Account.Domain.Interfaces;

namespace RVTR.Account.Domain.Validators
{
  public class AddressValidator : IValidator
  {
    static readonly HttpClient client;
    public string address;

    static async Task getValidation()
    {
      try
      {
        HttpResponseMessage response = await client.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyDbnHwx_6BDq2gcBfETYjbvVn-Y0QUa6mo");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }
    }

  }
}