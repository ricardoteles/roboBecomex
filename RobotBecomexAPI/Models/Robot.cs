using RobotBecomexAPI.Enums;

namespace RobotBecomexAPI.Models
{
    public sealed class Robot
    {
        private static Robot _instance;
        private static readonly object _instanceLock = new();

        public static Robot GetInstance() 
        {
            if (_instance == null) {
                lock (_instanceLock) {
                    if (_instance == null)
                    {
                        _instance = initialRobot();
                    }
                }
            }
            return _instance;
        }

        public Head Head { get; set; }
        public Elbow LeftElbow { get; set; }
        public Elbow RightElbow { get; set; }
        public Wrist LeftWrist { get; set; }
        public Wrist RightWrist { get; set; }

        private static Robot initialRobot() {
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
