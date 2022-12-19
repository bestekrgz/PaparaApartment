

namespace PaparaApartment.Core.Utilities.Result
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}
