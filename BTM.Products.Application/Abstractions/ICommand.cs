using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Products.Application.Abstractions
{
    public interface ICommand
    {
        // This can be used to mark types that represent commands
    }

    public interface ICommand<TResult>
    {
    }
}
