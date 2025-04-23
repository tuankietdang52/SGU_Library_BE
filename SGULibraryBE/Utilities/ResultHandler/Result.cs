using Google.Protobuf.WellKnownTypes;
using System.Diagnostics.CodeAnalysis;

namespace SGULibraryBE.Utilities.ResultHandler
{
    public class Result
    {
        public bool IsSuccess { get; }

        [NotNull]
        public Error? Error { get; }

        protected Result()
        {
            IsSuccess = true;
            Error = default;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static implicit operator Result(Error error) => new(error);

        public static Result Success() => new();
        public static Result Failure(Error error) => new(error);
    }

    public class Result<T> : Result
    {

        private readonly T? _value;
        public T Value
        {
            get => IsSuccess ? _value! : throw new InvalidOperationException("Not valid because result is false");
        }

        private Result(T value) : base()
        {
            _value = value;
        }

        private Result(Error error) : base(error)
        {

        }

        public static implicit operator Result<T>(Error error) => new(error);

        public static Result<T> Success(T value) => new(value);
        public static new Result<T> Failure(Error error) => new(error);
    }
}
