namespace GymTrainingPlanner.Common.Models.Responses
{
    using System.Collections.Generic;
    using GymTrainingPlanner.Common.Enums;

    public class DatabaseErrorApiResponse : ApiResponse
    {
        public DatabaseErrorApiResponse(List<string> errors)
            : base(ResponseStatus.DatabaseError, errors) { }
    }
}
