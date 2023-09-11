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
    public class UserService :IUserService
    {
        #region Fields
        private readonly IUnitOfWork _unitofwork;
        #endregion

        #region Constructor
        public UserService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Users> Create(Users users)
        {
            try
            {
                var result = await _unitofwork.Users.AddData(users);
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
        public async Task<Users> Update(Users users)
        {
            try
            {
                var result = await _unitofwork.Users.EditData(users);
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
        public async Task<Users> Delete(int id) 
        {

            try
            {
                var result = await _unitofwork.Users.DeleteData(id);
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
        public async Task<Users> Get(int id)
        {
            try
            {
              return await _unitofwork.Users.GetDataById(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Users> GetByEmailId(string id)
        {
            try
            {
                return await _unitofwork.Users.GetByExpression(s=>s.Email.ToLower()==id.ToLower());
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            try
            {
                return await _unitofwork.Users.GetData();
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
