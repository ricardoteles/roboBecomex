using RobotBecomexAPI.Enums;
using RobotBecomexAPI.Models;
using RobotBecomexAPI.Repositories.Interfaces;

namespace RobotBecomexAPI.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        public Robot getInitialRobotState()
        {
            return Robot.GetInstance();
        }
    }
}
