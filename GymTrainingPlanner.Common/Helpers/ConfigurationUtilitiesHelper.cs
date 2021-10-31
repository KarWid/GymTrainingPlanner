namespace GymTrainingPlanner.Common.Helpers
{
    using System;
    using GymTrainingPlanner.Common.Constants;

    public interface IConfigurationUtilitiesHelper
    {
        string ConnectionString { get; }
        string EmailTemporaryDirectory { get; }
    }

    public class ConfigurationUtilitiesHelper : IConfigurationUtilitiesHelper
    {
        public string ConnectionString
        {
            get
            {
                var result = Environment.GetEnvironmentVariable(Constant.EnvironmentVariablesConstant.CONNECTION_STRING);
                if (string.IsNullOrEmpty(result))
                {
                    throw new Exception("Connection string is not defined.");
                }

                return result;
            }
        }

        public string EmailTemporaryDirectory
        {
            get
            {
                var result = Environment.GetEnvironmentVariable(Constant.EnvironmentVariablesConstant.EMAIL_TEMP_DIRECTORY);
                if (string.IsNullOrEmpty(result))
                {
                    throw new Exception("Email temporary directory is not defined.");
                }

                return result;
            }
        }
    }
}
