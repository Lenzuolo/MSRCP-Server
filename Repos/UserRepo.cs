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
            result.PasswordHash = "";
            result.WorkDatas = new List<WorkData>() { await GetTodaysWorkData(id) };
        }
        return result;
    }

    public async Task<ICollection<User>> GetTeamAsync(int adminId)
    {
        return await context.Users
            .Where(u => u.Id != adminId)
            .Include(u => u.WorkDatas
                .Where(w => w.StartTime.Date.CompareTo(DateTime.Now.Date) == 0).Select(w => 
                new WorkData 
                { 
                    Status = w.EndTime != null ? (int)WorkDataStatus.COMPLETE : (int)WorkDataStatus.WORKING,
                    StartTime = w.StartTime,
                    EndTime = w.EndTime
                }))
            .Select(u => new User { FirstName = u.FirstName, LastName = u.LastName, Id = u.Id})
            .OrderBy(u => u.WorkDatas.ToList()[0].Status)
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

    public async Task<WorkData> ScanCodeAsync(User user, string code)
    {
        var workData = await GetTodaysWorkData(user.Id);
        //TODO: Pobieranie kodów z bazy kodów
        if (workData == null)
        {
            var result = await context.WorkDatas.AddAsync(new WorkData { User = user, StartTime = DateTime.Now, Status = (int)WorkDataStatus.WORKING });
            await context.SaveChangesAsync();
            return result.Entity;
        } else
        {
            workData.EndTime = DateTime.Now;
            workData.Status = (int)WorkDataStatus.COMPLETE;
            var result = context.WorkDatas.Update(workData);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }

    public async Task<User> GetUserAsync(string userName)
    {
        var result = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        //if (result != null)
        //{
        //    var workData = await GetTodaysWorkData(result.Id);
        //    if (workData != null)
        //    {
        //        result.WorkDatas.Add(workData);
        //    }
        //}
        return result;
    }

    private async Task<WorkData> GetTodaysWorkData(int userId)
    {
        var result = await context.WorkDatas.FirstOrDefaultAsync(w => w.User.Id == userId && w.StartTime.Date.CompareTo(DateTime.Now.Date) == 0);
        return result;
    }
}
