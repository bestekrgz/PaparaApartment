

namespace PaparaApartment.Core.Utilities.Result
{
 
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Result(bool success, string message):this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        
    }
}
