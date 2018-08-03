using Orleans;
using OrleansDashboardTest.GrainInterfaces;
using System;
using System.Threading.Tasks;

namespace OrleansDashboardTest.Grains
{
    public class AccountGrain : Grain<AccountState>, IAccount
    {
        public Task SetBalance(double balance)
        {
            State.Balance = balance;
            return WriteStateAsync();
        }
    }
    public class AccountState
    {
        public double Balance { get; set; }
    }

}
