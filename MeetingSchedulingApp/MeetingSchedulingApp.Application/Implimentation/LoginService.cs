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
    public  class LoginService : ILoginService
    {
        #region Fields
        private readonly IUnitOfWork _unitofwork;
        #endregion

        #region Constructor
        public LoginService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Login> Create(Login login)
        {
            try
            {
                var result = await _unitofwork.Logins.AddData(login);
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
        public async Task<Login> GetByEmail(string id)
        {
            try
            {
                return await _unitofwork.Logins.GetByExpression(s=>s.User.Email.ToLower()==id.ToLower());
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> GetById_Email(string eid, int id)
        {
            try
            {
                return await _unitofwork.Logins.GetByExpression(s => s.User_Id == id && s.User.Email.ToLower() == eid.ToLower());

            }
            catch { throw; };
        }
       
        #endregion

        #region Update
        public async Task<Login> Update(Login login)
        {
            try
            {
                var result = await _unitofwork.Logins.EditData(login);
                var resultcheck = await _unitofwork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
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
