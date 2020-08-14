using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using System.Threading.Tasks;
using RVTR.Account.WebApi.ResponseObjects;
using System;
using Microsoft.AspNetCore.Http;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
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
        return Ok(MessageObject.Success);
      }
      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"Account with ID number {id} does not exist.");
        }
        return NotFound(new ErrorObject ($"Account with ID number {id} does not exist"));
      }
    }

    /// <summary>
    /// Get all user accounts available
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
      if (!ModelState.IsValid)
      {
        if (_logger != null)
        {
          _logger.LogError("A bad request was sent for the accounts.");
        }
        return BadRequest(new ErrorObject("Invalid data sent"));
      }
      else
      {
        if (_logger != null)
        {
          _logger.LogInformation($"Retrieved the accounts.");
        }
        return Ok(await _unitOfWork.Account.SelectAsync());
      }
    }

    /// <summary>
    /// Get a user's account by account ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      AccountModel accountModel;
      try
      {
        if(_logger != null)
        {
          _logger.LogDebug("Getting an account by its ID number...");
        }
        accountModel = await _unitOfWork.Account.SelectAsync(id);
      }
      catch (ArgumentException e)
      {
        if(_logger != null)
        {
          _logger.LogError("A bad request was sent for the account.");
        }
        return BadRequest(new ValidationError(e));

      }
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
      return NotFound(new ErrorObject ($"Account with ID number {id} does not exist."));
    }

    /// <summary>
    /// Add an account 
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] AccountModel account)
    {
      if(!ModelState.IsValid)
      {
        if (_logger != null)
        {
          _logger.LogError("A bad request was sent for the account.");
        }
        return BadRequest(new ErrorObject("Invalid account data sent"));
      }
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
      return Ok(MessageObject.Success);
      
    }

    /// <summary>
    /// Update an existing account
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] AccountModel account)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          if (_logger != null)
          {
            _logger.LogError("A bad request was sent for the account.");
          }
          return BadRequest(new ErrorObject("Invalid account data sent"));
        }
        if (_logger != null)
        {
          _logger.LogDebug("Updating an account...");
        }
        _unitOfWork.Account.Update(account);
        await _unitOfWork.CommitAsync();


        //return Accepted(account);
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
