using MeetingSchedulingApp.Application.Implimentation;
using MeetingSchedulingApp.Application.Interfaces;
using MeetingSchedulingApp.Model.DatabaseModel;
using MeetingSchedulingApp.Model.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSchedulingApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        #region Fields
        private readonly IParticipantService _participantService;
        #endregion

        #region Constructor
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }
        #endregion

        #region Get
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {

                var result = await _participantService.GetAll(id);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Participants>() { IsSuccess = true, Message = "Api Execute Successfully", Results = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Participants>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #region Create Meeting Participants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Participants participants)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                participants.CreatedAt = DateTime.Now;
                participants.UpdatedAt = DateTime.Now;
                participants.Status = "pending";
                var result = await _participantService.Create(participants);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Participants>() { IsSuccess = true, Message = "Meeting Participant created successfully", Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Participants>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #region Invite update
        [HttpPut("Invite/{id}")]
        public async Task<IActionResult> InvitePut(int id,[FromBody]bool status)
        {
            try
            {

                var participant = await _participantService.Get(id);
                if (participant == null)
                {
                    throw new Exception("Participant not found");
                }
                if (status)
                {
                    participant.Status = "accept";
                }
                else
                {
                    participant.Status = "reject";
                }
                participant.UpdatedAt = DateTime.Now;
                var result = await _participantService.Update(participant); 
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Participants>() { IsSuccess = true, Message = $"Participant {result.Status} invitation", Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Participants>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion
    }
}
