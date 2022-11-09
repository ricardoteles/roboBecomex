using RobotBecomexAPI.Models;
using RobotBecomexAPI.Responses;

namespace RobotBecomexAPI.Services.Interfaces
{
    public interface IRobotService
    {
        Task<RobotApiResponse> getInitialRobotState();
        Task<RobotApiResponse> moveRobotElbow(string side, int state);
        Task<RobotApiResponse> rotateRobotWrist(string side, int state);
        Task<RobotApiResponse> moveRobotHead(int state);
        Task<RobotApiResponse> rotateRobotHead(int state);
    }
}
