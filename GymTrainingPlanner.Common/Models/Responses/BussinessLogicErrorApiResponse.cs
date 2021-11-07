namespace GymTrainingPlanner.Common.Models.Responses
{
    using System.Collections.Generic;
    using GymTrainingPlanner.Common.Enums;

    public class BussinessLogicErrorApiResponse : ApiResponse
    {
        public BussinessLogicErrorApiResponse(string error) : base(ResponseStatus.BusinessLogicError, new List<string> { error }) { }

        public BussinessLogicErrorApiResponse(List<string> errors) : base(ResponseStatus.BusinessLogicError, errors) { }
    }
}
