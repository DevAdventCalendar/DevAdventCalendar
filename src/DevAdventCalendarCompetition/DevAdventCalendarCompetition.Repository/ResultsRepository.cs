using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public List<Result> GetTestResultsForWeek(int weekNumber, int resultsCountToGet, int paginationIndex)
        {
            if (weekNumber < 1 || weekNumber > 4)
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                throw new ArgumentException("Invalid week number.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }

            Expression<Func<Result, bool>> resultsFilter = GetFilter(weekNumber);
            Expression<Func<Result, int?>> resultsOrder = GetOrder(weekNumber);

            return this._dbContext.Results
                .Include(u => u.User)
                .Where(resultsFilter)
                .OrderBy(resultsOrder)
                .Skip(resultsCountToGet * (paginationIndex - 1))
                .Take(resultsCountToGet)
                .ToList();
        }

        public int GetTotalTestResultsCount(int weekNumber)
        {
            return this._dbContext.Results
                .Count(GetFilter(weekNumber));
        }

        private static Expression<Func<Result, bool>> GetFilter(int weekNumber)
        {
            return weekNumber switch
            {
                1 => r => r.Week1Points != null,
                2 => r => r.Week2Points != null,
                3 => r => r.Week3Points != null,
                4 => r => r.FinalPoints != null,
                _ => null,
            };
        }

        private static Expression<Func<Result, int?>> GetOrder(int weekNumber)
        {
            return weekNumber switch
            {
                1 => r => r.Week1Place,
                2 => r => r.Week2Place,
                3 => r => r.Week3Place,
                4 => r => r.FinalPlace,
                _ => null,
            };
        }
    }
}