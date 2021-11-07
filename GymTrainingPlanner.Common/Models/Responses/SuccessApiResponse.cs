namespace GymTrainingPlanner.Common.Models.Responses
{
    using GymTrainingPlanner.Common.Enums;

    public class SuccessApiResponse<T> : ApiResponse<T>
    {
        public SuccessApiResponse(T result) : base(result, ResponseStatus.Success, null) { }
    }
}
