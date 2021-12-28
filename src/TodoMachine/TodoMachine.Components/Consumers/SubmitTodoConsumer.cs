using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoMachine.Contracts;

namespace TodoMachine.Components;
public class SubmitTodoConsumer : IConsumer<ISubmitTodo>
{
    private readonly ILogger<SubmitTodoConsumer> _logger;

    public SubmitTodoConsumer(ILogger<SubmitTodoConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ISubmitTodo> context)
    {
        _logger.LogDebug("SubmitTodoConsume: {Description}", context.Message.Description);
        if (context.Message.Description.Contains("TEST"))
        {
            await context.RespondAsync<ITodoSubmissionRejected>(new
            {
                TodoId = context.Message.TodoId,
                InVar.Timestamp,
                Description = context.Message.Description,
                Reason = "Don't use test strings in production"
            });
        }

        await context.RespondAsync<ITodoSubmissionAccepted>(new
        {
            InVar.Timestamp,
            context.Message.TodoId,
            context.Message.Description
        });
    }
}
