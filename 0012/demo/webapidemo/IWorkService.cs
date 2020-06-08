using System;
using System.Threading;
using System.Threading.Tasks;

namespace webapidemo
{
    public interface IWorkService
    {
        Task DoWork();
    }
}
