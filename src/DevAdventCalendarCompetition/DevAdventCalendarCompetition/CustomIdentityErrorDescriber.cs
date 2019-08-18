using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError { Code = nameof(this.DefaultError), Description = $"Wystąpił nieznany błąd." };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError { Code = nameof(this.ConcurrencyFailure), Description = "Optymistyczna awaria współbieżności, obiekt został zmodyfikowany." };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = nameof(this.PasswordMismatch), Description = "Niepoprawne hasło." };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError { Code = nameof(this.InvalidToken), Description = "Niepoprawny token." };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError { Code = nameof(this.LoginAlreadyAssociated), Description = "Użytkownik o wskazanym loginie już istnieje." };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = nameof(this.InvalidUserName), Description = $"Nazwa użytkownika '{userName}' jest nieprawidłowa, może jedynie zawierać litery i cyfry." };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = nameof(this.InvalidEmail), Description = $"Adres email '{email}' jest niepoprawny." };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = nameof(this.DuplicateUserName), Description = $"Nazwa użytkownika '{userName}' jest już zajęta." };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = nameof(this.DuplicateEmail), Description = $"Adres email '{email}' został już zarejestrowany." };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError { Code = nameof(this.InvalidRoleName), Description = $"Rola '{role}' jest nieprawidłowa." };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError { Code = nameof(this.DuplicateRoleName), Description = $"Rola '{role}' została przydzielona." };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError { Code = nameof(this.UserAlreadyHasPassword), Description = "Użytkownik ma już ustawione hasło." };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError { Code = nameof(this.UserLockoutNotEnabled), Description = "Blokada nie jest załączona." };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError { Code = nameof(this.UserAlreadyInRole), Description = $"Użytkownik jest już przydzielony do roli '{role}'." };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError { Code = nameof(this.UserNotInRole), Description = $"Użytkownik nie ma roli '{role}'." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = nameof(this.PasswordTooShort), Description = $"Hasło musi zawierać minimum {length} znaków." };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Code = nameof(this.PasswordRequiresNonAlphanumeric), Description = "Hasło musi zawierać znak specjalny." };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = nameof(this.PasswordRequiresDigit), Description = "Hasło musi zawierać cyfrę ('0'-'9')." };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = nameof(this.PasswordRequiresLower), Description = "Hasło musi zawierać małą literę ('a'-'z')." };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = nameof(this.PasswordRequiresUpper), Description = "Hasło musi zawierać dużą literę ('A'-'Z')." };
        }
    }
}