using RobotBecomexAPI;
using RobotBecomexAPI.Enums;
using RobotBecomexAPI.Models;

namespace RobotBecomexAPITest.Mocks
{
    public static class RobotMock
    {
        public static Robot getInitializedRobotMock() { 
            return new Robot
            {
                Head = new Head
                {
                    Inclination = InclinationEnum.Stopped,
                    Rotation = RotationEnum.Stopped
                },
                LeftElbow = new Elbow
                {
                    Strength = StrengthEnum.Stopped
                },
                RightElbow = new Elbow
                {
                    Strength = StrengthEnum.Stopped
                },
                LeftWrist = new Wrist
                {
                    Rotation = RotationEnum.Stopped
                },
                RightWrist = new Wrist
                {
                    Rotation = RotationEnum.Stopped
                }
            };
        }
    }
}
