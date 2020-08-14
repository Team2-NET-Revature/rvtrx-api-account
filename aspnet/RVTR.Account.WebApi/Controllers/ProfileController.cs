using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using RVTR.Account.WebApi.ResponseObjects;

namespace RVTR.Account.WebApi.Controllers
{
  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/account/{version:apiVersion}/[controller]")]
  public class ProfileController : ControllerBase
  {
    private readonly ILogger<ProfileController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ProfileController(ILogger<ProfileController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Delete a user's profile
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
          _logger.LogDebug("Deleting a profile by its ID number...");
        }
        await _unitOfWork.Profile.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        if (_logger != null)
        {
          _logger.LogInformation($"Deleted the profile.");
        }
        return Ok();
      }
      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"Profile with ID number {id} does not exist.");
        }
        return NotFound(new ErrorObject($"Profile with ID number {id} does not exist"));
      }
    }

    /// <summary>
    /// Get all profiles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
      if(!ModelState.IsValid)
      {
        if(_logger != null)
        {
          _logger.LogError("A bad request was sent for the profiles.");
        }
        return BadRequest(new ErrorObject("Invalid data sent"));
      }
      else
      {
        if (_logger != null)
        {
          _logger.LogInformation($"Retrieved the profiles.");
        }
        return Ok(await _unitOfWork.Profile.SelectAsync());
      }
      
    }

    /// <summary>
    /// Get a user's profile with profile ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      ProfileModel profileModel;
      try
      {
        if (_logger != null)
        {
          _logger.LogDebug("Getting an profile by its ID number...");
        }
        profileModel = await _unitOfWork.Profile.SelectAsync(id);
      }
      catch(ArgumentException e)
      {
        if (_logger != null)
        {
          _logger.LogError("A bad request was sent for the profile.");
        }
        return BadRequest(new ValidationError(e));
      }
      if (profileModel is ProfileModel theProfile)
      {
        if (_logger != null)
        {
          _logger.LogInformation($"Retrieved the profile with ID: {id}.");
        }
        return Ok(theProfile);
      }
      if (_logger != null)
      {
        _logger.LogWarning($"Profile with ID number {id} does not exist.");
      }
      return NotFound(new ErrorObject($"Profile with ID number {id} does not exist."));
      // return NotFound(id);
    }


    /// <summary>
    /// Add a profile to an account
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      await _unitOfWork.Profile.InsertAsync(profile);
      await _unitOfWork.CommitAsync();

      return Accepted(profile);
    }

    /// <summary>
    /// Update a user's profile
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      if(!ModelState.IsValid)
      {
        if (_logger != null)
        {
          _logger.LogError("A bad request was sent for the profile.");
        }
        return BadRequest(new ErrorObject("Invalid profile data sent"));
      }
      if (_logger != null)
      {
        _logger.LogDebug("Updating a profile...");
      }
      _unitOfWork.Profile.Update(profile);
      await _unitOfWork.CommitAsync();

      if (_logger != null)
      {
        _logger.LogInformation($"Successfully updated the account {profile}.");
      }
      return Ok(MessageObject.Success);
      //return Accepted(profile);
    }
   
  }
}
