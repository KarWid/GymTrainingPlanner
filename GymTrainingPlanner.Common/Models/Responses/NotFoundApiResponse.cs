namespace GymTrainingPlanner.Common.Models.Responses
{
    using System.Collections.Generic;
    using GymTrainingPlanner.Common.Enums;

    public class NotFoundApiResponse : ApiResponse
    {
        public NotFoundApiResponse(string error) : base(ResponseStatus.NotFound, new List<string> { error }) { }

        public NotFoundApiResponse(List<string> errors) : base(ResponseStatus.NotFound, errors) { }
    }
}
