using System;

namespace SloVVo.Logic.Response
{
    internal class Response<T> : IResponse<T>
    {
        public Response(bool isSuccess)
        {
            Success = isSuccess;
        }

        public Response(T data) : this(true)
        {
            Data = data;
        }

        public Response(Exception ex) : this(false)
        {
            Exception = ex;
        }
        public Response() : this(true) { }

        public bool Success { get; set; }
        public T Data { get; set; }
        public Exception Exception { get; set; }
    }

    internal class Response : IResponse
    {
        public bool Success { get; set; }

        public Response(bool isSuccess)
        {
            Success = isSuccess;
        }
    }
}
