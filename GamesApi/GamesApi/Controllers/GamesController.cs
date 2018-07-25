using GamesApi.Context;
using GamesApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : Controller
    {
        [HttpGet("/bestscores")]
        public dynamic GetBestScores(int gameId)
        {
            using (var dbContext = new GamesDbContext())
            {
                return dbContext.Score.Include("User").Where(x => x.GameId == gameId).OrderBy(x => x.ScorePoint).Select(x => new
                {
                    x.ScorePoint,
                    x.User.Name
                }).Take(15).ToList();
            }
        }

        [HttpPost("/createuser")]
        public async Task PostUser([FromBody] User user)
        {
            using (var dbContext = new GamesDbContext())
            {
                var currentUser = await dbContext.User.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
                if (currentUser == null)
                {
                    user.Id = Guid.NewGuid();
                    await dbContext.Add(user);
                }
                else
                {
                    currentUser.Email = user.Email;
                    currentUser.Name = user.Name;
                    await dbContext.Edit(currentUser);
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}