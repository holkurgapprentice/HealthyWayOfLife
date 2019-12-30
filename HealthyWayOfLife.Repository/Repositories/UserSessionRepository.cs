using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly HealthyWayOfLifeDbContext _context;

        public UserSessionRepository(HealthyWayOfLifeDbContext context)
        {
            _context = context;
        }

        public Task InsertUserSession(Session userSession)
        {
            _context.Add(userSession);
            return _context.SaveChangesAsync();
        }

        public Task<Session> GetExistingUserSession(string token)
        {
            return _context.Sessions.FirstOrDefaultAsync(t => t.Token == token);
        }

        public Task<Session> GetExistingUserSessionWithUser(string token)
        {
            return _context.Sessions.Include(x => x.User).FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task CloseOpenSessions(int userId)
        {
            List<Session> sessions = await _context.Sessions.Where(x => x.UserId == userId && x.SessionState == SessionState.Open).ToListAsync();
            if (sessions != null)
            {
                foreach (Session session in sessions)
                {
                    session.SessionState = SessionState.Closed;
                    session.UpdateDate = DateTime.UtcNow;
                    session.EndTime = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateSessionRefreshInfo(Session session)
        {
            try
            {
                _context.Update(session);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new CustomCodeException(HttpStatusCode.InternalServerError, e.ToString(), LogType.Error);
            }

        }
    }
}