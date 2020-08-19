using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.ResponseObjects;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RVTR.Account.WebApi.Controllers
{
  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/account/{version:apiVersion}/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly ILogger<AccountController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public AccountController(ILogger<AccountController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Delete a user's account
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        if (_logger != null)
        {
          _logger.LogDebug("Deleting an account by its ID number...");
        }
        await _unitOfWork.Account.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        if (_logger != null)
        {
          _logger.LogInformation($"Deleted the account.");
        }
        return Accepted(MessageObject.Success);
      }
      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"Account with ID number {id} does not exist.");
        }
        return NotFound(new ErrorObject($"Account with ID number {id} does not exist"));
      }
    }

    /// <summary>
    /// Get all user accounts available
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {

      if (_logger != null)
      {
        _logger.LogInformation($"Retrieved the accounts.");
      }
      return Ok(await _unitOfWork.Account.SelectAsync());

    }

    /// <summary>
    /// Get a user's account by account ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      AccountModel accountModel;

      if (_logger != null)
      {
        _logger.LogDebug("Getting an account by its ID number...");
      }
      accountModel = await _unitOfWork.Account.SelectAsync(id);


      if (accountModel is AccountModel theAccount)
      {
        if (_logger != null)
        {
          _logger.LogInformation($"Retrieved the account with ID: {id}.");
        }
        return Ok(theAccount);
      }
      if (_logger != null)
      {
        _logger.LogWarning($"Account with ID number {id} does not exist.");
      }
      return NotFound(new ErrorObject($"Account with ID number {id} does not exist."));
    }

    /// <summary>
    /// Add an account 
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] AccountModel account)
    {
      {
        if (_logger != null)
        {
          _logger.LogDebug("Adding an account...");
        }
        await _unitOfWork.Account.InsertAsync(account);
        await _unitOfWork.CommitAsync();

        if (_logger != null)
        {
          _logger.LogInformation($"Successfully added the account {account}.");
        }
        return Accepted(MessageObject.Success);
      }
    }

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] AccountModel account)
    {
      try
      {
        if (_logger != null)
        {
          _logger.LogDebug("Updating an account...");
        }
        _unitOfWork.Account.Update(account);
        await _unitOfWork.CommitAsync();


        if (_logger != null)
        {
          _logger.LogInformation($"Successfully updated the account {account}.");
        }
        return Ok(MessageObject.Success);
      }

      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"This account does not exist.");
        }
        return NotFound(new ErrorObject($"Account with ID number {account.Id} does not exist"));
      }

    }

  }
}
