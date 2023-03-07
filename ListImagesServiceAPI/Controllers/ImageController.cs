using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace uploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private string _imagePath = string.Empty;

        // Konstruktøren til controlleren som tager en logger og en IConfiguration-objekt som input
        public ImageController(ILogger<ImageController> logger, IConfiguration configuration)
        {
            // Henter billedstien fra IConfiguration-objektet og gemmer den i en variabel
            _imagePath = configuration["ImagePath"] ?? String.Empty;
            // Gemmer loggeren til senere brug
            _logger = logger;
        }

        // En HTTP GET-metode som returnerer en liste med URI'er for alle billeder i billedstien
        [HttpGet("listImages")]
        [Produces("application/json")]
        public IActionResult ListImages()
        {
            // Opretter en tom liste for URI'erne
            List<Uri> billeder = new List<Uri>();
            // Tjekker om billedstien eksisterer
            if (Directory.Exists(_imagePath))
            {
                // Henter en liste over filerne i billedstien
                string[] filer = Directory.GetFiles(_imagePath);
                // Går igennem hver fil og tilføjer URI'en til listen over billeder
                foreach (var fil in filer)
                {
                    var billedeURI = new Uri(fil, UriKind.RelativeOrAbsolute);
                    billeder.Add(billedeURI);
                }
            }
            // Returnerer listen over URI'er for alle billeder
            return Ok(billeder);
        }
    }
}
