using Moq;
using NUnit.Framework;
using RobotBecomexAPI;
using RobotBecomexAPI.Enums;
using RobotBecomexAPI.Models;
using RobotBecomexAPI.Repositories.Interfaces;
using RobotBecomexAPI.Responses;
using RobotBecomexAPI.Services;
using RobotBecomexAPITest.Mocks;

namespace RobotBecomexAPITest
{
    [TestFixture]
    public class RobotServiceTest
    {
        private RobotService _instance;
        private Mock<IRobotRepository> _repository;
        Robot robo;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRobotRepository>();
            _instance = new RobotService(_repository.Object);

            robo = RobotMock.getInitializedRobotMock();
        }

        [Test]
        public void RobotService_GetInitialRobotState_ShouldReturnRobotInStoppedMode_WhenInitialized()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();
            _repository.Setup(_ => _.getInitialRobotState()).Returns(robo);

            // Act
            var result = _instance.getInitialRobotState();

            // Assert
            Assert.AreEqual(result.Result.Robot, robo);
        }

        [Test]
        public void RobotService_MoveRobotElbow_ShouldReturnRobotLeftElbowSlightlyContracted_WhenPassedValidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotElbow("left", 2);

            // Assert
            Assert.AreEqual(result.Result.Robot.LeftElbow.Strength, StrengthEnum.Slightly_Contracted);
            Assert.AreEqual(result.Result.Robot.RightElbow.Strength, StrengthEnum.Stopped);
        }

        [Test]
        public void RobotService_MoveRobotElbow_ShouldReturnRobotRightElbowSlightlyContracted_WhenPassedValidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();
            Robot.GetInstance().LeftElbow.Strength = StrengthEnum.Stopped;

            // Act
            var result = _instance.moveRobotElbow("right", 2);

            // Assert
            Assert.AreEqual(result.Result.Robot.RightElbow.Strength, StrengthEnum.Slightly_Contracted);
            Assert.AreEqual(result.Result.Robot.LeftElbow.Strength, StrengthEnum.Stopped);
        }

        [Test]
        public void RobotService_MoveRobotElbow_ShouldReturnAnErrorMsg_WhenPassedInvalidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotElbow("right", 4);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Não se pode pular um dos estados.");
        }

        [Test]
        public void RobotService_RotateRobotWrist_ShouldReturnAnErrorMsg_WhenElbowIsNotStronglyContracted()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.rotateRobotWrist("left", 2);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.");
        }

        [Test]
        public void RobotService_RotateRobotWrist_ShouldReturnAnErrorMsg_WhenPassedValidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotElbow("right", 2);
            result = _instance.moveRobotElbow("right", 3);
            result = _instance.moveRobotElbow("right", 4);
            result = _instance.rotateRobotWrist("right", 7);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Não se pode pular um dos estados.");
        }

        [Test]
        public void RobotService_RotateRobotWrist_ShouldChangeRightWristRotation_WhenElbowIsStronglyContracted()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotElbow("right", 2);
            result = _instance.moveRobotElbow("right", 3);
            result = _instance.moveRobotElbow("right", 4);
            result = _instance.rotateRobotWrist("right", 4);

            // Assert
            Assert.AreEqual(result.Result.Robot.RightWrist.Rotation, RotationEnum.Rotation_45);
        }

        [Test]
        public void RobotService_MoveRobotHead_ShouldReturnRobotHeadInclination_WhenPassedValidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotHead(1);

            // Assert
            Assert.AreEqual(result.Result.Robot.Head.Inclination, InclinationEnum.Up);
        }

        [Test]
        public void RobotService_MoveRobotHead_ShouldReturnAnErrorMsg_WhenPassedInvalidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();

            // Act
            var result = _instance.moveRobotHead(1);
            result = _instance.moveRobotHead(3);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Não se pode pular um dos estados.");
        }

        [Test]
        public void RobotService_RotateRobotHead_ShouldReturnRobotHeadRotation_WhenPassedValidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();
            Robot.GetInstance().Head.Inclination = InclinationEnum.Stopped;

            // Act
            var result = _instance.rotateRobotHead(2);

            // Assert
            Assert.AreEqual(result.Result.Robot.Head.Rotation, RotationEnum.Rotation_Minus_45);
        }

        [Test]
        public void RobotService_RotateRobotHead_ShouldReturnAnErrorMsg_WhenHeadIsDown()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();
            Robot.GetInstance().Head.Inclination = InclinationEnum.Stopped;

            // Act
            var result = _instance.moveRobotHead(3);
            result = _instance.rotateRobotHead(2);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Só poderá Rotacionar a Cabeça caso sua Inclinação da Cabeça não esteja em estado Para Baixo.");
        }

        [Test]
        public void RobotService_RotateRobotHead_ShouldReturnAnErrorMsg_WhenPassedInvalidValue()
        {
            // Arrange
            RobotApiResponse resp = new RobotApiResponse();
            Robot.GetInstance().Head.Inclination = InclinationEnum.Stopped;

            // Act
            var result = _instance.rotateRobotHead(5);

            // Assert
            Assert.AreEqual(result.Result.ErrorMsg, "Não se pode pular um dos estados.");
        }
    }
}
