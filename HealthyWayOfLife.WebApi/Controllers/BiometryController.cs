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

        [HttpGet("id")]
        public async Task<Biometry> Get(int id)
        {
            return await _biometryRepository.GetBiometryForUserWithId(new User { Id = 1 }, id);
        }

        [HttpPut]
        public async Task<Biometry> Put([FromBody] Biometry biometry)
        {
            return await _biometryRepository.UpdateBiometryForUser(biometry);
        }
        [HttpPost]
        public async Task<Biometry> Post([FromBody] Biometry biometry)
        {
            return await _biometryRepository.AddBiometryForUser(biometry);
        }

        [HttpDelete("id")]
        public async Task<bool> Delete(int id)
        {
            return (await _biometryRepository.ArchiveBiometryForUser(new User {Id = 1}, id)) != 0 ? true : false;

        }
    }
}