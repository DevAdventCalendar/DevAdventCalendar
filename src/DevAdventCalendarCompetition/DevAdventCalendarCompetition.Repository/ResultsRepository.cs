using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition.Repository
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ResultsRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserPosition GetUserPosition(string userId)
        {
            var result = this._dbContext.Results
                .FirstOrDefault(x => x.UserId == userId);

            var userPosition = new UserPosition();

            if (result == null)
            {
                return userPosition;
            }

            if (result.FinalPlace > 0)
            {
                userPosition.FinalPlace = result.FinalPlace.Value;
            }

            if (result.Week3Place > 0)
            {
                userPosition.Week3Place = result.Week3Place.Value;
            }

            if (result.Week2Place > 0)
            {
                userPosition.Week2Place = result.Week2Place.Value;
            }

            if (result.Week1Place > 0)
            {
                userPosition.Week1Place = result.Week1Place.Value;
            }

            return userPosition;
        }

        public List<Result> GetTestResultsForWeek(int weekNumber)
        {
            switch (weekNumber)
            {
                case 1:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week1Points != null)
                        .OrderBy(r => r.Week1Place)
                        .ToList();
                case 2:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week2Points != null)
                        .OrderBy(r => r.Week2Place)
                        .ToList();
                case 3:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.Week3Points != null)
                        .OrderBy(r => r.Week3Place)
                        .ToList();
                case 4:
                    return this._dbContext.Results
                        .Include(u => u.User)
                        .Where(r => r.FinalPoints != null)
                        .OrderBy(r => r.FinalPlace)
                        .ToList();
                default:
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    throw new ArgumentException("Invalid week number.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }
        }
    }
}