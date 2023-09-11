using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingSchedulingApp.Model.DatabaseModel;

namespace MeetingSchedulingApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private ApplicationDbContext _context;
        private GenericRepository<Login> _logins;
        private GenericRepository<Users> _users;
        private GenericRepository<Meetings> _meetings;
        private GenericRepository<Participants> _participants;
        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IGenericRepository<Login> Logins
        {
            get
            {
                return _logins ??
                    (_logins = new GenericRepository<Login>(_context));
            }
        }
        public IGenericRepository<Users> Users
        {
            get
            {
                return _users ??
                    (_users = new GenericRepository<Users>(_context));
            }
        }
        public IGenericRepository<Meetings> Meetings
        {
            get
            {
                return _meetings ??
                    (_meetings = new GenericRepository<Meetings>(_context));
            }
        }
        public IGenericRepository<Participants> Participants
        {
            get
            {
                return _participants ??
                    (_participants = new GenericRepository<Participants>(_context));
            }
        }

        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
