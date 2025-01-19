using Microsoft.AspNetCore.Http.HttpResults;

namespace ToolBoxAPI.Models
{
    public abstract class BaseResponse<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }

        public BaseResponse(){
            Data = default;
            Error = new Error();
        }
    }
}
