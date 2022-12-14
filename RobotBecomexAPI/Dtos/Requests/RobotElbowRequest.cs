using System.ComponentModel.DataAnnotations;

namespace RobotBecomexAPI.Dtos.Requests
{
    public class RobotElbowRequest
    {
        [Range(1, 4)]
        public int state { get; set; }
        public string side { get; set; }
    }
}
