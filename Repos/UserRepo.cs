using Microsoft.EntityFrameworkCore;
using MSRCP_Server.Models;

namespace MSRCP_Server.Repos;

public class UserRepo : IUserRepo
{
    private readonly DataContext context;
    public UserRepo(DataContext context)
    {
        this.context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        var result = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User> GetAsync(int id)
    {
        var result = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (result != null)
        {
            result.CurrentWorkData = await GetTodaysWorkData(id);
        }
        return result;
    }

    public async Task<ICollection<User>> GetTeamAsync(int adminId)
    {
        return await context.Users
            .Where(u => u.Id != adminId)
            .Include(u => u.WorkDatas
                .Where(w => w.StartTime.Date.CompareTo(DateTime.Now.Date) == 0))
            .Select(u => new User { FirstName = u.FirstName, LastName = u.LastName, Id = u.Id})
            .OrderByDescending(u => u.WorkDatas.First().Status)
            .ToListAsync();
    }

    public async Task<ICollection<WorkData>> GetUserHistoryAsync(int id)
    {
        return await context.WorkDatas.Where(w => w.User.Id == id).ToListAsync();
    }

    public async Task<User> UpdateAsync(User user)
    {
        var result = context.Update(user);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<WorkData> ScanCodeAsync(int userId, string code)
    {
        var workData = await GetTodaysWorkData(userId);
        var isValidCode = context.QrCodes.Any(qr => qr.Code == code && qr.ValidDate == DateOnly.FromDateTime(DateTime.Now));
        var user = await GetAsync(userId);
        if (isValidCode && user != null)
        {
            if (workData == null)
            {
                var result = await context.WorkDatas.AddAsync(new WorkData { User = user, StartTime = DateTime.Now.ToUniversalTime(), Status = (int)WorkDataStatus.WORKING });
                await context.SaveChangesAsync();
                return result.Entity;
            } else
            {
                workData.EndTime = DateTime.Now.ToUniversalTime();
                workData.Status = (int)WorkDataStatus.COMPLETE;
                var result = context.WorkDatas.Update(workData);
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;
    }

    public async Task<User> GetAsync(string userName)
    {
        var result = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        if (result != null)
        {
            result.CurrentWorkData = await GetTodaysWorkData(result.Id);
        }
        return result;
    }

    public async Task<QR> GenerateQRAsync(QR qr)
    {
        var result = await context.QrCodes.AddAsync(qr);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public bool CodeAlreadyExists(string code)
    {
        return context.QrCodes.Any(r => r.Code == code);
    }

    private async Task<WorkData> GetTodaysWorkData(int userId)
    {
        var result = await context.WorkDatas
            .Include(w => w.User)
            .OrderByDescending(w => w.StartTime)
            .FirstOrDefaultAsync(w => w.User.Id == userId);

        if (result != null && DateOnly.FromDateTime(result.StartTime).CompareTo(DateOnly.FromDateTime(DateTime.Now)) == 0)
            return result;
        else return null;
    }
}
