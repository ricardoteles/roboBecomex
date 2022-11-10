using System.ComponentModel.DataAnnotations;

namespace RobotBecomexAPI.Dtos.Requests
{
    public class RobotWristRequest
    {
        [Range(1, 7)]
        public int state { get; set; }
        public string side { get; set; }
    }
}
