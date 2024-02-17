using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using traffic_lights_sytem_app.Modal;

namespace traffic_lights_sytem_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficLightController : ControllerBase
    {
        private readonly TrafficLightDbContext _context;
        private readonly ILogger<TrafficLightController> _logger;

        public TrafficLightController(TrafficLightDbContext context, ILogger<TrafficLightController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("GetTrafficData")]
        public async Task<IActionResult> GetTrafficData()
        {
            try
            {
                // Log the time when the request is received
                var currentTime = DateTime.Now;
                _logger.LogInformation($"Request received at {currentTime}");

                // Check if it's peak hours
                bool isPeakHours = (currentTime.Hour >= 8 && currentTime.Hour < 10) || (currentTime.Hour >= 17 && currentTime.Hour < 19);

                // Loop through each traffic light
                foreach (var light in _context.TrafficLights)
                {
                    // Check if the light is red
                    if (light.Color == LightColor.Red)
                    {
                        // Change red lights to green every 4 seconds
                        if (currentTime.Second % 4 == 0)
                        {
                            light.Color = LightColor.Green;
                        }
                    }
                    // Check if the light is green
                    else if (light.Color == LightColor.Green)
                    {
                        // Adjust timings for peak hours
                        if (isPeakHours)
                        {
                            if ((light.Direction == TrafficDirection.North || light.Direction == TrafficDirection.South) && currentTime.Second % 40 == 0)
                            {
                                light.Color = LightColor.Yellow; // Show yellow for 5 seconds
                            }
                            else if ((light.Direction == TrafficDirection.West || light.Direction == TrafficDirection.East) && currentTime.Second % 10 == 0)
                            {
                                light.Color = LightColor.Yellow; // Show yellow for 5 seconds
                            }
                        }
                        else
                        {
                            // Normal timing
                            if (currentTime.Second % 20 == 0)
                            {
                                light.Color = LightColor.Yellow; // Show yellow for 5 seconds
                            }
                        }
                    }
                    // Check if the light is yellow
                    else if (light.Color == LightColor.Yellow)
                    {
                        // Change yellow lights to red every 5 seconds
                        if (currentTime.Second % 5 == 0)
                        {
                            light.Color = LightColor.Red;
                        }
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve and return all traffic lights
                var trafficLights = _context.TrafficLights.ToList();
                return Ok(trafficLights);
            }
            // Handle exceptions
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
            // Log completion of request processing
            finally
            {
                _logger.LogInformation("Request processing completed.");
            }
        }
    }
}
