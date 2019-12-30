using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model.Database;
using Microsoft.AspNetCore.Mvc;

namespace HealthyWayOfLife.WebApi.Controllers
{
    [Route("/api/Biometry")]
    public class BiometryWeightController : BaseController
    {
        private readonly IBiometryRepository _biometryRepository;

        public BiometryWeightController(IBiometryRepository biometryRepository)
        {
            _biometryRepository = biometryRepository;
        }

        [HttpGet("Weight")]
        public async Task<List<Biometry>> Get()
        {
            return await _biometryRepository.GetBiometryForUser(new User {Id = 1});
        }
    }
}