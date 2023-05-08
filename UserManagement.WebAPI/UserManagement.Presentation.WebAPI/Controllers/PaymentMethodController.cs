using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Business.ServiceInterfaces;
using UserManagement.Domain.Models;
using UserManagement.Presentation.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IMapper _mapper;

        public PaymentMethodController(IPaymentMethodService PaymentMethodService, IMapper mapper)
        {
            _paymentMethodService = PaymentMethodService;
            _mapper = mapper;
        }

        // GET: api/<PaymentMethodController>
        [HttpGet]
        public async Task<ActionResult<ICollection<PaymentMethodDto>>> Get()
        {
            var payments = await _paymentMethodService.GetAllPaymentMethods();
            
            return Ok(_mapper.Map<ICollection<PaymentMethodDto>>(payments));
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDto>> Get(int id)
        {
            var payment = await _paymentMethodService.GetPaymentMethod(id);
            if(payment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PaymentMethodDto>(payment));
        }

        // GET api/<PaymentMethodController>/ByUserId/5
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<PaymentMethodDto>> GetAllPaymentsOfUser(int userId)
        {
            var payments = await _paymentMethodService.GetAllPaymentMethodsByUserId(userId);
            return Ok(_mapper.Map<ICollection<PaymentMethodDto>>(payments));
        }

        // POST api/<PaymentMethodController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(int userId, [FromBody] PaymentMethodDto paymentMethodDto)
        {
            var newPaymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
            newPaymentMethod.UserId = userId;

            var createdPayment = await _paymentMethodService.CreatePaymentMethod(newPaymentMethod);
            if (createdPayment == null)
            {
                return NotFound();
            }

            return Ok(createdPayment);
        }

        // PUT api/<PaymentMethodController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PaymentMethodDto paymentMethodDto)
        {
            var newUpdatedPaymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
            newUpdatedPaymentMethod.Id = id;

            var updatedPayment = await _paymentMethodService.UpdatePaymentMethod(newUpdatedPaymentMethod);
            if (updatedPayment == null)
            {
                return NotFound();
            }

            return Ok(newUpdatedPaymentMethod);
        }

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAsync(int id)
        {
            var paymentToDelete =_paymentMethodService.DeletePaymentMethod(id);
            return Ok(_mapper.Map<PaymentMethodDto>(paymentToDelete));
        }
    }
}
