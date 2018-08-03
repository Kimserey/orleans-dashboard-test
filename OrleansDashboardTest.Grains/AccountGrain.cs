using Orleans;
using OrleansDashboardTest.GrainInterfaces;
using System.Threading.Tasks;

namespace OrleansDashboardTest.Grains
{
    public class AccountGrain : Grain<AccountState>, IAccount
    {
        public Task<double> GetBalance()
        {
            return Task.FromResult(State.Balance);
        }

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
