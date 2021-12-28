using System;

namespace TodoMachine.Contracts;

public interface ITodoSubmissionRejected
{
    Guid TodoId { get; }
    DateTime Timestamp { get; }
    string Description { get; }
    string Reason { get; }
}
