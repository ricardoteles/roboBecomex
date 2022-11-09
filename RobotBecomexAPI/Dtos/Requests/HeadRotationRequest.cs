using System.ComponentModel.DataAnnotations;

namespace RobotBecomexAPI.Dtos.Requests
{
    public class HeadRotationRequest
    {
        [Range(1, 5)]
        public int state { get; set; }
    }
}
