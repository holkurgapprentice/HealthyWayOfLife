using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace HealthyWayOfLife.WebApi.Controllers
{
    public class BiometryController : BaseController
    {
        private readonly IBiometryRepository _biometryRepository;

        public BiometryController(IBiometryRepository biometryRepository)
        {
            _biometryRepository = biometryRepository;
        }

        [HttpGet]
        public async Task<List<Biometry>> Get()
        {
            return await _biometryRepository.GetBiometryForUser(new User {Id = 1});
        }

        [HttpPost]
        public async Task<Biometry> Post([FromBody] Biometry biometry)
        {
            return await _biometryRepository.AddBiometryForUser(biometry);
        }
    }
}