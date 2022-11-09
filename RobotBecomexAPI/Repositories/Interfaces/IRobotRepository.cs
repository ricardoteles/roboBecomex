using RobotBecomexAPI.Models;

namespace RobotBecomexAPI.Repositories.Interfaces
{
    public interface IRobotRepository
    {
        Robot getInitialRobotState();
    }
}
