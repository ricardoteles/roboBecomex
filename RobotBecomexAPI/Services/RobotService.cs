using RobotBecomexAPI.Enums;
using RobotBecomexAPI.Models;
using RobotBecomexAPI.Repositories.Interfaces;
using RobotBecomexAPI.Responses;
using RobotBecomexAPI.Services.Interfaces;
using System;

namespace RobotBecomexAPI.Services
{
    public class RobotService: IRobotService
    {
        private readonly IRobotRepository _repository;

        public RobotService(IRobotRepository repository)
        {
            _repository = repository;
        }

        public Task<RobotApiResponse> getInitialRobotState()
        {
            RobotApiResponse resp = new RobotApiResponse();
            resp.Robot = _repository.getInitialRobotState();

            return Task.FromResult(resp);
        }

        public Task<RobotApiResponse> moveRobotElbow(string side, int state)
        {
            RobotApiResponse resp = new RobotApiResponse();
            Robot robot = Robot.GetInstance();
            StrengthEnum elbowState;

            elbowState = side == "left" ? robot.LeftElbow.Strength : robot.RightElbow.Strength;

            if (canMakeStateProgression((int)elbowState, state, resp))
                if (side == "left")
                    robot.LeftElbow.Strength = (StrengthEnum)state;
                else
                    robot.RightElbow.Strength = (StrengthEnum)state;

            resp.Robot = robot;
            return Task.FromResult(resp);
        }

        public Task<RobotApiResponse> rotateRobotWrist(string side, int state)
        {
            RobotApiResponse resp = new RobotApiResponse();
            Robot robot = Robot.GetInstance();
            StrengthEnum elbowState;
            RotationEnum wristState;

            elbowState = side == "left"? robot.LeftElbow.Strength : robot.RightElbow.Strength;
            wristState = side == "left" ? robot.LeftWrist.Rotation : robot.RightWrist.Rotation;

            if(elbowState != StrengthEnum.Strongly_Contracted)
                resp.ErrorMsg = "Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.";
            else if (elbowState == StrengthEnum.Strongly_Contracted && canMakeStateProgression((int)wristState, state, resp))
            {
                if (side == "left")
                    robot.LeftWrist.Rotation = (RotationEnum)state;
                else
                    robot.RightWrist.Rotation = (RotationEnum)state;
            }

            resp.Robot = robot;
            return Task.FromResult(resp);
        }

        public Task<RobotApiResponse> moveRobotHead(int state)
        {
            RobotApiResponse resp = new RobotApiResponse();
            Robot robot = Robot.GetInstance();

            if (canMakeStateProgression((int)robot.Head.Inclination, state, resp))
                robot.Head.Inclination = (InclinationEnum)state;

            resp.Robot = robot;
            return Task.FromResult(resp);
        }

        public Task<RobotApiResponse> rotateRobotHead(int state)
        {
            RobotApiResponse resp = new RobotApiResponse();
            Robot robot = Robot.GetInstance();
            InclinationEnum headState = robot.Head.Inclination;

            if (headState == InclinationEnum.Down)
                resp.ErrorMsg = "Só poderá Rotacionar a Cabeça caso sua Inclinação não esteja em estado Para Baixo.";
            else if (headState != InclinationEnum.Down && canMakeStateProgression((int)robot.Head.Rotation, state, resp))
                robot.Head.Rotation = (RotationEnum)state;

            resp.Robot = robot;
            return Task.FromResult(resp);
        }

        private bool canMakeStateProgression(int atualValue, int newValue, RobotApiResponse resp) {

            if (Math.Abs(atualValue - newValue) > 1)
            {
                resp.ErrorMsg = "Não se pode pular um dos estados.";
                return false;
            }

            return true;
        }
    }
}
