﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansDashboardTest.GrainInterfaces;

namespace OrleansDashboardTest.Web
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private IGrainFactory _factory;

        public AccountController(IGrainFactory factory)
        {
            _factory = factory;
        }

        [HttpPost]
        public async Task<ActionResult<double>> Post(AccountDto dto)
        {
            var grain = _factory.GetGrain<IAccount>(0);
            await grain.SetBalance(dto.Value);
            return await grain.GetBalance();
        }

        [HttpGet]
        public async Task<ActionResult<double>> Get()
        {
            var grain = _factory.GetGrain<IAccount>(0);
            return await grain.GetBalance();
        }
    }
}
