using System;

namespace TodoMachine.Contracts;

public interface ITodoSubmissionAccepted
{
    Guid TodoId { get; }
    DateTime Timestamp { get; }
    string Description { get; }
}
