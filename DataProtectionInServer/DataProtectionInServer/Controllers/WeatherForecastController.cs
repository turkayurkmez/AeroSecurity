using DataProtectionInServer.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtectionInServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHostEnvironment hostBuilder;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHostEnvironment hostBuilder)
        {
            _logger = logger;
            this.hostBuilder = hostBuilder;
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            var bycryptResult = BCrypt.Net.BCrypt.HashPassword(password);
            return Ok(bycryptResult);
        }
        [HttpPost("/login")]
        public IActionResult Login(string password)
        {
           var result =  BCrypt.Net.BCrypt.Verify(password, "$2b$10$jGHZhhVxRXI.5MNlaHR1YeulI2uyb7JvaaFSL/9vlOXAOJYOvoEUS");
            return Ok(result);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            //encryption:
            string kritik = "Bu cümle önemli";
            DataProtector dataProtector = new DataProtector(this.hostBuilder.ContentRootPath);
            var length = dataProtector.EncryptData(kritik);
            //decryption:
            var sifrelenmisVeri = dataProtector.DecryptData(length);


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Result = sifrelenmisVeri
            })
            .ToArray();
        }
    }
}
