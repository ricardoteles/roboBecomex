using RobotBecomexAPI.Models;

namespace RobotBecomexAPI.Responses
{
    public class RobotApiResponse
    {
        public Robot Robot { get; set; }
        public string ErrorMsg { get; set; }
    }
}
