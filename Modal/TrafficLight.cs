using System.ComponentModel.DataAnnotations;
using log4net;

namespace traffic_lights_sytem_app.Modal
{
    // Enum for traffic directions
    public enum TrafficDirection
    {
        South,
        West,
        North,
        East
    }

    // Enum for light colors
    public enum LightColor
    {
        Red,
        Yellow,
        Green
    }

    // Model class for TrafficLight
    public class TrafficLight
    {
        // Unique identifier for the traffic light
        public int Id { get; set; }

        // Direction of the traffic
        [EnumDataType(typeof(TrafficDirection))]
        public TrafficDirection Direction { get; set; }

        // Color of the traffic light
        [EnumDataType(typeof(LightColor))]
        public LightColor Color { get; set; }
    }
}
