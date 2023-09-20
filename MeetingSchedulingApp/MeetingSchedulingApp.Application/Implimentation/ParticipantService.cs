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
    public class ParticipantService :IParticipantService
    {
        #region Fields
        private readonly IUnitOfWork _unitofwork;
        #endregion

        #region Constructor
        public ParticipantService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Participants> Create(Participants participants)
        {
            try
            {
                var result = await _unitofwork.Participants.AddData(participants);
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
        public async Task<Participants> Update(Participants participants)
        {
            try
            {
                var result = await _unitofwork.Participants.EditData(participants);
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
        public async Task<Participants> Delete(int id)
        {
            try
            {
                var result = await _unitofwork.Participants.DeleteData(id);
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
        public async Task<Participants> Get(int id)
        {
            try
            {
                return await _unitofwork.Participants.GetDataById(id);
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<Participants>> GetAll(int id)
        {
            try
            {
                return (await _unitofwork.Participants.GetAllByExpression(s => s.User_Id == id)).OrderByDescending(s => s.Id);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Participants>> GetAll()
        {
            try
            {
                return await _unitofwork.Participants.GetData();
            }
            catch
            {
                throw;
            }

        }
        public async Task<IEnumerable<Participants>> GetAllByMeetingId(int id)
        {
            try
            {
                return (await _unitofwork.Participants.GetAllByExpression(s => s.Meeting_Id == id)).OrderByDescending(s => s.Id);
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
