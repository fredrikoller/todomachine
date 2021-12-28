using System;

namespace TodoMachine.Contracts;

public interface ISubmitTodo
{
    Guid TodoId { get; }
    DateTime Timestamp { get; }
    string Description { get; }
}
