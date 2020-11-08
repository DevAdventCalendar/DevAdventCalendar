using System;
using System.Collections.Generic;
using System.Linq;
using DevAdventCalendarCompetition.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace DevAdventCalendarCompetition.Tests.UnitTests.Controllers.IdentityHelpers
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager(IUserValidator<ApplicationUser> userValidator, IQueryable<ApplicationUser> users)
            : base(
                new Mock<IUserPasswordStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new List<IUserValidator<ApplicationUser>> { userValidator },
                Array.Empty<IPasswordValidator<ApplicationUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {
            this.Users = users;
        }

        public override IQueryable<ApplicationUser> Users { get; }
    }
}