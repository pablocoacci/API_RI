namespace Presentation.API.Helpers.Models
{
    public class HttpSuccess
    {
        public HttpSuccess(object data)
        {
            Data = data;
        }

        public object Data { get; }
    }
}
