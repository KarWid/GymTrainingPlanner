namespace GymTrainingPlanner.Common.Models.Responses
{
    using System.Collections.Generic;
    using GymTrainingPlanner.Common.Enums;

    public abstract class ApiResponse
    {
        public ResponseStatus Status { get; set; }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(ResponseStatus status, List<string> errors)
        {
            Success = status == ResponseStatus.Success;
            Status = status;
            Errors = errors;
        }
    }

    public abstract class ApiResponse<T> : ApiResponse
    {
        public T Result { get; }

        //TODO @KWidla: change tokens - use userId
        public ApiResponse(T result, ResponseStatus status, List<string> errors) : base(status, errors)
        {
            Result = result;
        }
    }
}
