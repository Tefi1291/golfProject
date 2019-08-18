using GolfAPI.Core.Contracts.Api;

namespace GolfAPI.Core.Factories
{
    public static class ErrorResponseFactory 
    {
        public static ErrorResponse BuildBadRequest()
        {
            return new ErrorResponse()
            {
                ErrorCode = ErrorCodes.BadRequest,
                Description = "Invalid User/password"
            }; ;
        }

        public static ErrorResponse BuildNotPermission()
        {
            return new ErrorResponse()
            {
                ErrorCode = ErrorCodes.NotPermission,
                Description = "You are not enabled to enter the site"
            };
        }

        public static ErrorResponse BuildWrongPassword()
        {
            return new ErrorResponse()
            {
                ErrorCode = ErrorCodes.WrongPassword,
                Description = ErrorCodes.WrongPassword.ToString()
            };
        }

        public static ErrorResponse BuildWrongUser()
        {
            return new ErrorResponse()
            {
                ErrorCode = ErrorCodes.WrongUsername,
                Description = ErrorCodes.WrongUsername.ToString()
            };
        }
    }
}
