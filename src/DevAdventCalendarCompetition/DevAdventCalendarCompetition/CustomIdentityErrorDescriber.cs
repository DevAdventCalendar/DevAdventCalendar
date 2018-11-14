using Microsoft.AspNetCore.Identity;

namespace DevAdventCalendarCompetition
{
	public class CustomIdentityErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Wystąpił nieznany błąd." }; }
		public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Optymistyczna awaria współbieżności, obiekt został zmodyfikowany." }; }
		public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Niepoprawne hasło." }; }
		public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Niepoprawny token." }; }
		public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Użytkownik o wskazanym loginie już istnieje." }; }
		public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"Nazwa użytkownika '{userName}' jest nieprawidłowa, może jedynie zawierać litery i cyfry." }; }
		public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"Adres email '{email}' jest niepoprawny." }; }
		public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Nazwa użytkonwnika '{userName}' jest już zajęta." }; }
		public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Adres email '{email}' został juz zarejestrowany." }; }
		public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"Rola '{role}' jest nieprawidłowa." }; }
		public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"Rola '{role}' została przydzielona." }; }
		public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Użytkownik ma już ustawione hasło." }; }
		public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Blokada nie jest załączona." }; }
		public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Użytkownik jest już przycielony do role '{role}'." }; }
		public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"Użytkowni nie jest w roli '{role}'." }; }
		public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Hasło musi zawierać na końcu {length} znaków." }; }
		public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Hasło musi posiadać na końcu znak z poza alfabeturic character." }; }
		public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Hasło musi zawierać na końcu cyfrę ('0'-'9')." }; }
		public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Hasło musi zawierać na końcu małą litere ('a'-'z')." }; }
		public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Hasło musi zawierać na końcu dużą litere ('A'-'Z')." }; }
	}
}