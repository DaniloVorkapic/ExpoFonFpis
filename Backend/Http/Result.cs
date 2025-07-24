using Backend.Enums;
using MediatR;

namespace Backend.Http
{
    public class Result<T> : IRequest
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public T Value { get; private set; }

        private Result(bool isSuccess, string error, T value)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, value);
        public static Result<T> Failure(string error) => new(false, error, default);
    }
}
