using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevAdventCalendarCompetition.TestResultService.Tests.Models
{
    public class ResultModel
    {
        public List<Result> GetWeek1ResultList(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            return new List<Result>()
            {
                new Result()
                {
                    Id = 1,
                    UserId = UserModel.userD.Id,
                    User = UserModel.userD,
                    Week1Points = 260,
                    Week1Place = 1
                },
                new Result()
                {
                    Id = 2,
                    UserId = UserModel.userC.Id,
                    User = UserModel.userC,
                    Week1Points = 260,
                    Week1Place = 2
                },
                new Result()
                {
                    Id = 3,
                    UserId = UserModel.userA.Id,
                    User = UserModel.userA,
                    Week1Points = 250,
                    Week1Place = 3
                },
                new Result()
                {
                    Id = 4,
                    UserId = UserModel.userB.Id,
                    User = UserModel.userB,
                    Week1Points = 250,
                    Week1Place = 4
                },
                new Result()
                {
                    Id = 5,
                    UserId = UserModel.userG.Id,
                    User = UserModel.userG,
                    Week1Points = 250,
                    Week1Place = 5
                },
                new Result()
                {
                    Id = 6,
                    UserId = UserModel.userH.Id,
                    User = UserModel.userH,
                    Week1Points = 130,
                    Week1Place = 6
                },
                new Result()
                {
                    Id = 7,
                    UserId = UserModel.userE.Id,
                    User = UserModel.userE,
                    Week1Points = 0,
                    Week1Place = 7
                },
                new Result()
                {
                    Id = 8,
                    UserId = UserModel.userF.Id,
                    User = UserModel.userF,
                    Week1Points = 0,
                    Week1Place = 8
                },
                new Result()
                {
                    Id = 9,
                    UserId = UserModel.userI.Id,
                    User = UserModel.userI,
                    Week1Points = 0,
                    Week1Place = 9
                }
            };
        }

        public List<Result> GetWeek2ResultList(ApplicationDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            return new List<Result>()
            {
                new Result()
                {
                    Id = 1,
                    UserId = UserModel.userI.Id,
                    User = UserModel.userI,
                    Week2Points = 130,
                    Week2Place = 1
                },
                new Result()
                {
                    Id = 2,
                    UserId = UserModel.userA.Id,
                    User = UserModel.userA,
                    Week2Points = 0,
                    Week2Place = 2
                },
                new Result()
                {
                    Id = 3,
                    UserId = UserModel.userB.Id,
                    User = UserModel.userB,
                    Week2Points = 0,
                    Week2Place = 3
                },
                new Result()
                {
                    Id = 4,
                    UserId = UserModel.userC.Id,
                    User = UserModel.userC,
                    Week2Points = 0,
                    Week2Place = 4
                },
                new Result()
                {
                    Id = 5,
                    UserId = UserModel.userD.Id,
                    User = UserModel.userD,
                    Week2Points = 0,
                    Week2Place = 5
                },
                new Result()
                {
                    Id = 6,
                    UserId = UserModel.userE.Id,
                    User = UserModel.userE,
                    Week2Points = 0,
                    Week2Place = 6
                },
                new Result()
                {
                    Id = 7,
                    UserId = UserModel.userF.Id,
                    User = UserModel.userF,
                    Week2Points = 0,
                    Week2Place = 7
                },
                new Result()
                {
                    Id = 8,
                    UserId = UserModel.userG.Id,
                    User = UserModel.userG,
                    Week2Points = 0,
                    Week2Place = 8
                },
                new Result()
                {
                    Id = 9,
                    UserId = UserModel.userH.Id,
                    User = UserModel.userH,
                    Week2Points = 0,
                    Week2Place = 9
                }
            };
        }
    }
}
