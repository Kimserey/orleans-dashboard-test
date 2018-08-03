using System;
using System.Threading.Tasks;
using Orleans;

namespace OrleansDashboardTest.GrainInterfaces
{
    public interface IAccount: IGrainWithIntegerKey
    {
        Task SetBalance(double balance);
    }
}
