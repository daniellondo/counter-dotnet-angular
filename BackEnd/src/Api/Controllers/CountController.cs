namespace Api.Controllers
{
    using Domain.Dtos.Count;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Count
        /// <summary>
        /// Get Count
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCountQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // POST: api/Count
        /// <summary>
        /// Add Count
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCountCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // PUT: api/Count
        /// <summary>
        /// Update Count
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCountCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }

    }
}
