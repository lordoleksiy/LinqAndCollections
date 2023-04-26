using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BLL.Infrastructure;

public class ValidationException: Exception
{
    public int Status { get; }
    public ValidationException(string message, int StatusCode): base(message)
    {
        Status = StatusCode;
    }

}
