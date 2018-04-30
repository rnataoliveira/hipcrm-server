namespace server.Shared
{
    public class CommandResult
    {
        public string FailureReason { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(FailureReason);

        public static CommandResult Success { get; } = new CommandResult();
        public static CommandResult Fail(string reason) => new CommandResult { FailureReason = reason };

        public static implicit operator bool(CommandResult result) => result.IsSuccess;
    }

    public class CommandResult<T> : CommandResult
    {
        public T Data { get; set; }

        public new static CommandResult<T> Fail(string reason) => new CommandResult<T>() { FailureReason = reason };
        public new static CommandResult<T> Success(T data) => new CommandResult<T> { Data = data };
    }
}