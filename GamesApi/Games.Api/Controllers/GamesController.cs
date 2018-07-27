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
        public async Task<IHttpActionResult> GetBestScores()
        {
            using (var dbContext = new GamesDbContext())
            {
                return Ok(await  dbContext.Score.Include("User").Where(x => x.GameId == 1).OrderBy(x => x.ScorePoint).Select(x => new
                {
                    x.ScorePoint,
                    x.User.Name,
                    x.User.Password
                }).Take(15).ToListAsync());
            }
        }

        [HttpPost]
        [Route("api/game/user")]
        public async Task<IHttpActionResult> PostUser([FromBody] User user, [FromUri] int bestScore = 0)
        {
            Guid userId;
            using (var dbContext = new GamesDbContext())
            {
                var currentUser = await dbContext.User.Where(x => x.Email == user.Email).FirstOrDefaultAsync();
                if (currentUser == null)
                {
                    userId = Guid.NewGuid();
                    user.Id = userId;
                    await dbContext.Add(user);
                }
                else
                {
                    currentUser.Email = user.Email;
                    currentUser.Name = user.Name;
                    userId = currentUser.Id;
                    await dbContext.Edit(currentUser);
                }
                if (bestScore > 0)
                {
                    var currentGameScore = await dbContext.Score.Where(x => x.UserId == userId && x.ScorePoint == bestScore).FirstOrDefaultAsync();
                    if (currentGameScore == null)
                    {
                        await dbContext.Add(new Score {
                            UserId = userId,
                            GameId = 1,
                            Id = Guid.NewGuid(),
                            ScorePoint = bestScore
                        });
                    }
                }
                await dbContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
