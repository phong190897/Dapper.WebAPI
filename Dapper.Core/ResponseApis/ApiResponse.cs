using Dapper.Core.CustomEntities;

namespace Dapper.Application
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }

        public ApiResponse()
        {

        }

        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
        public Metadata Meta { get; set; }
    }
}
