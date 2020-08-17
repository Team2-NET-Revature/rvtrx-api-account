using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [ApiVersion("0.0")]
    [EnableCors("Public")]
    [Route("rest/account/{version:apiVersion}/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitOfWork"></param>
        public PaymentController(ILogger<PaymentController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Payment.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Ok();
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Payment.SelectAsync());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.Payment.SelectAsync(id));
            }
            catch
            {
                return NotFound(id);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(PaymentModel payment)
        {
            await _unitOfWork.Payment.InsertAsync(payment);
            await _unitOfWork.CommitAsync();

            return Accepted(payment);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(PaymentModel payment)
        {
            _unitOfWork.Payment.Update(payment);
            await _unitOfWork.CommitAsync();

            return Accepted(payment);
        }
    }
}
