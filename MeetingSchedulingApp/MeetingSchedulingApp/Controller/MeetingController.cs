using MeetingSchedulingApp.Application.Interfaces;
using MeetingSchedulingApp.Model.DatabaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeetingSchedulingApp.Model.ResponseModel;
using MeetingSchedulingApp.Pages.Meetings;

namespace MeetingSchedulingApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        #region Fields
        private readonly IMeetingService _meetingService;
        #endregion

        #region Constructor
        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }
        #endregion

        #region Method

        #region Create Meeting
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody]Meetings meetings)
        {
            try
            {
                if(!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                meetings.CreatedAt = DateTime.Now;
                meetings.UpdatedAt = DateTime.Now;
                meetings.Status = "pending";
                var result = await _meetingService.Create(meetings);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Meetings>() { IsSuccess = true, Message = "Meeting created successfully",Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Meetings>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #region Get
        [HttpGet("Dashboard/{id}")]
        public async Task<IActionResult> GetDashboard(int id)
        {
            try
            {

                var today = await _meetingService.GetByDate(id, true);
                var upcoming = await _meetingService.GetByDate(id,false);
                var upcominggroup = upcoming.GroupBy(g => g.StartDateTime.ToString("D") );
                List<MeetingGroupByDate> meetingGroups = new();
                foreach(var group in upcominggroup)
                {
                    meetingGroups.Add(new() { scheduledDate = group.FirstOrDefault().StartDateTime, Meetings = group.ToList() });
                }
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Dashboard>() { IsSuccess = true, Message = "Api Execute Successfully", Result = new() { Upcoming = meetingGroups ,Today = today} });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Dashboard>() { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
               
                var result = await _meetingService.GetAll(id);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Meetings>() { IsSuccess = true, Message = "Api Execute Successfully", Results = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Meetings>() { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var result = await _meetingService.Get(id);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Meetings>() { IsSuccess = true, Message = "Api Execute Successfully", Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Meetings>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Meetings meetings)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var meeting = await _meetingService.Get(id);
                meeting.Title = meetings.Title;
                meeting.Description = meetings.Description;
                meeting.Location = meetings.Location;
                meeting.Meeting_Type = meetings.Meeting_Type;
                meeting.StartDateTime = meetings.StartDateTime;
                meeting.EndDateTime = meeting.EndDateTime;
                meeting.UpdatedAt = DateTime.Now;
                meeting.Status= meetings.Status;
                var result = await _meetingService.Update(meeting);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Meetings>() { IsSuccess = true, Message = "Meeting updated successfully", Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Meetings>() { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            try
            {

                var result = await _meetingService.Delete(id);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Meetings>() { IsSuccess = true, Message = "Meeting removed successfully", Result = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Meetings>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #endregion
    }
}
