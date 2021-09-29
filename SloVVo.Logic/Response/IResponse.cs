using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Logic.Response
{
    interface IResponse
    {
        bool Success { get; set; }
    }

    interface IResponse<T> : IResponse
    {
        T Data { get; set; }
        Exception Exception { get; set; }
    }
}
