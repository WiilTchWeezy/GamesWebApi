using Games.Api.Context;
using Games.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Games.Api.Controllers
{
    public class GamesController : ApiController
    {
        [HttpGet]
        [Route("api/game/score")]
        public dynamic GetBestScores(int gameId)
        {
            using (var dbContext = new GamesDbContext())
            {
                return Ok( dbContext.Score.Include("User").Where(x => x.GameId == gameId).OrderBy(x => x.ScorePoint).Select(x => new
                {
                    x.ScorePoint,
                    x.User.Name
                }).Take(15).ToList());
            }
        }

        [HttpPost]
        [Route("createuser")]
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
