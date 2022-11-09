using System.ComponentModel.DataAnnotations;

namespace RobotBecomexAPI.Dtos.Requests
{
    public class HeadInclinationRequest
    {
        [Range(1, 3)]
        public int state { get; set; }
    }
}
