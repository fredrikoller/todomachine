using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoMachine.Contracts;

namespace TodoMachine.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IRequestClient<ISubmitTodo> _client;

    public TodoController(ILogger<TodoController> logger, IRequestClient<ISubmitTodo> client)
    {
        _logger = logger;
        _client = client;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ITodoSubmissionAccepted), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ITodoSubmissionRejected), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(Guid id, string description)
    {
        _logger.LogInformation("Yay");
        var (accepted, rejected) = await _client.GetResponse<ITodoSubmissionAccepted, ITodoSubmissionRejected>(new
        {
            TodoId = id,
            InVar.Timestamp,
            Description = description
        });

        if (accepted.IsCompletedSuccessfully)
        {
            var response = await accepted;

            return Accepted(response.Message);
        }
        else
        {
            var response = await rejected;

            return BadRequest(response.Message);
        }

    }
}
