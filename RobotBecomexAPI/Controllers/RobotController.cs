using Microsoft.AspNetCore.Mvc;
using RobotBecomexAPI.Dtos.Requests;
using RobotBecomexAPI.Responses;
using RobotBecomexAPI.Services.Interfaces;

namespace RobotBecomexAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotService _service;

        public RobotController(IRobotService service)
        {
            _service = service;
        }

        [HttpGet("initialState")]
        public async Task<RobotApiResponse> InitialRobotState()
        {
            return await _service.getInitialRobotState();
        }

        [HttpPut("move/elbow")]
        public async Task<RobotApiResponse?> MoveRobotElbow([FromBody] RobotElbowRequest robotResquest)
        {
            return await _service.moveRobotElbow(robotResquest.side, robotResquest.state);
        }

        [HttpPut("rotate/wrist")]
        public async Task<RobotApiResponse?> rotateRobotWrist([FromBody] RobotWristRequest robotResquest)
        {
            return await _service.rotateRobotWrist(robotResquest.side, robotResquest.state);
        }

        [HttpPut("move/head")]
        public async Task<RobotApiResponse?> MoveRobotHead([FromBody] HeadInclinationRequest robotResquest)
        {
            return await _service.moveRobotHead(robotResquest.state);
        }

        [HttpPut("rotate/head")]
        public async Task<RobotApiResponse?> rotateRobotHead([FromBody] HeadRotationRequest robotResquest)
        {
            return await _service.rotateRobotHead(robotResquest.state);
        }
    }
}
