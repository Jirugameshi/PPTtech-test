using System.Text.RegularExpressions;

namespace AvatarAPI.Services
{
    public class AvatarService : IAvatarService
    {
        public string GetAvatarUrl(string userIdentifier)
        {
            var avatarUrl = String.Empty;

            switch(GetAvatarSource(userIdentifier))
            {
                case AvatarSourceCase.TypicodeLastDigit:
                    break;
                case AvatarSourceCase.SqliteDatabase:
                    break;
                case AvatarSourceCase.DicebearVowel:
                    break;
                case AvatarSourceCase.DicebearNonAlpha:
                    break;
                case AvatarSourceCase.Unknown:
                default:
                    break;
            }

            return avatarUrl;
        }

        private AvatarSourceCase GetAvatarSource(string userIdentifier)
        {
            AvatarSourceCase result = AvatarSourceCase.Unknown;

            var typicodeLastDigitRegex = new Regex("^.*[6-9]$");
            var sqliteLastDigitRegex = new Regex("^.*[1-5]$");
            var dicebearVowelRegex = new Regex("^.*[aeiou].*$");
            var dicebearNonAlphaRegex = new Regex("[^a-zA-Z0-9]");

            if (typicodeLastDigitRegex.IsMatch(userIdentifier))
            {
                result = AvatarSourceCase.TypicodeLastDigit;
            }
            else if (sqliteLastDigitRegex.IsMatch(userIdentifier))
            {
                result = AvatarSourceCase.SqliteDatabase;
            }
            else if (dicebearVowelRegex.IsMatch(userIdentifier))
            {
                result = AvatarSourceCase.DicebearVowel;
            }
            else if (dicebearNonAlphaRegex.IsMatch(userIdentifier))
            {
                result = AvatarSourceCase.DicebearNonAlpha;
            }

            return result;
        }
    }

    enum AvatarSourceCase
    {
        TypicodeLastDigit,
        SqliteDatabase,
        DicebearVowel,
        DicebearNonAlpha,
        Unknown
    }
}
