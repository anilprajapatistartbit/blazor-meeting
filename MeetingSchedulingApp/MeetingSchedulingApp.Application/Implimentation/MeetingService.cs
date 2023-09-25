using MeetingSchedulingApp.Application.Interfaces;
using MeetingSchedulingApp.Infrastructure;
using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.Implimentation
{
    public class MeetingService :IMeetingService
    {
        #region Fields
        private readonly IUnitOfWork _unitofwork;
        #endregion

        #region Constructor
        public MeetingService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Meetings> Create(Meetings meetings)
        {
            try
            {
                var result = await _unitofwork.Meetings.AddData(meetings);
                var resultcheck = await _unitofwork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Update
        public async Task<Meetings> Update(Meetings meetings)
        {
            try
            {
                var result = await _unitofwork.Meetings.EditData(meetings);
                var resultcheck = await _unitofwork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public async Task<Meetings> Delete(int id)
        {
            try
            {
                var result = await _unitofwork.Meetings.DeleteData(id);
                var resultcheck = await _unitofwork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Get
        public async Task<Meetings> Get(int id)
        {
            try
            {
                return await _unitofwork.Meetings.GetDataById(id);
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<Meetings>> GetAll(int id)
        {
            try
            {
                return (await _unitofwork.Meetings.GetAllByExpression(s => s.Host_Id == id)).OrderByDescending(s => s.Id);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Meetings>> GetAll()
        {
            try
            {
                return await _unitofwork.Meetings.GetData();
            }
            catch
            {
                throw;
            }

        }

        public async Task<IEnumerable<Meetings>> GetByDate(int id, bool isToday)
        {
            try
            {
                if(isToday==true)
                {
                    return (await _unitofwork.Participants.GetAllByExpression(s=>((s.User_Id== id && s.Status.ToLower()=="accept" )||(s.Meeting.Host_Id==id )) && (s.Meeting.StartDateTime.Day == DateTime.Today.Day && DateTime.Today.Month == s.Meeting.StartDateTime.Month && DateTime.Today.Year == s.Meeting.StartDateTime.Year))).Select(s=>s.Meeting).Distinct();
                }

                var data = await _unitofwork.Participants.GetAllByExpression(s =>
                ((s.User_Id == id && s.Status.ToLower() == "accept") || (s.Meeting.Host_Id == id))
                && (s.Meeting.StartDateTime >= DateTime.Today.AddDays(1) && s.Meeting.StartDateTime <= DateTime.Today.AddDays(7)));
                return data.Select(s => s.Meeting).Distinct(); ;
            }
            catch
            {
                throw;
            }
            }

            #endregion


            #endregion
        }
}
