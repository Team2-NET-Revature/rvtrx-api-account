using System.Collections.Generic;
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
  /// Represents the _Profile Controller_ class
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
    /// The _Profile Controller_ constructor
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
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
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
        return Ok(MessageObject.Success);
      }
      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"Profile with ID number {id} does not exist.");
        }
        return NotFound(new ErrorObject($"Profile with ID number {id} does not exist."));
      }
    }

    /// <summary>
    /// Get all profiles 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProfileModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      if (_logger != null)
      {
        _logger.LogInformation($"Retrieved the profiles.");
      }
      return Ok(await _unitOfWork.Profile.SelectAsync());
    }

    /// <summary>
    /// Get a user's profile with profile ID number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      ProfileModel profileModel;

      if (_logger != null)
      {
        _logger.LogDebug("Getting a profile by its ID number...");
      }
      profileModel = await _unitOfWork.Profile.SelectAsync(id);


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
    }

    /// <summary>
    /// Add a profile to an account
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(ProfileModel profile)
    {
      if (_logger != null)
      {
        _logger.LogDebug("Adding a profile...");
      }
      await _unitOfWork.Profile.InsertAsync(profile);
      await _unitOfWork.CommitAsync();

      if (_logger != null)
      {
        _logger.LogInformation($"Successfully added the profile {profile}.");
      }
      return Ok(MessageObject.Success);
    }

    /// <summary>
    /// Update a user's profile
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(ProfileModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ProfileModel profile)
    {
      try
      {
        if (_logger != null)
        {
          _logger.LogDebug("Updating a profile...");
        }
        _unitOfWork.Profile.Update(profile);
        await _unitOfWork.CommitAsync();

        if (_logger != null)
        {
          _logger.LogInformation($"Successfully updated the profile {profile}.");
        }
        return Ok(MessageObject.Success);
      }
      catch
      {
        if (_logger != null)
        {
          _logger.LogWarning($"This profile does not exist.");
        }
        return NotFound(new ErrorObject($"Profile with ID number {profile.Id} does not exist."));
      }

    }

  }
}
