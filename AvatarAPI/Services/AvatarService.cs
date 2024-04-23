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
                    avatarUrl = GetTypicodeAvatarUrl(userIdentifier);
                    break;
                case AvatarSourceCase.SqliteDatabase:
                    avatarUrl = GetSqliteAvatarUrl(userIdentifier);
                    break;
                case AvatarSourceCase.DicebearVowel:
                    avatarUrl = GetDicebearVowelAvatarUrl(userIdentifier);
                    break;
                case AvatarSourceCase.DicebearNonAlpha:
                    avatarUrl = GetDicebearNonAlphaAvatarUrl(userIdentifier);
                    break;
                case AvatarSourceCase.Unknown:
                default:
                    avatarUrl = GetDefaultAvatarUrl();
                    break;
            }

            return avatarUrl;
        }
        private string GetTypicodeAvatarUrl(string userIdentifier)
        {
            var lastDigit = userIdentifier.Last();

            var url = $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastDigit}";

            return url;
        }
        private string GetSqliteAvatarUrl(string userIdentifier)
        {
            throw new NotImplementedException();
        }
        private string GetDicebearVowelAvatarUrl(string userIdentifier)
        {
            var vowelRegex = new Regex("[aeiou]");

            var vowel = vowelRegex.Match(userIdentifier).Value;

            var url = $"https://api.dicebear.com/8.x/pixel-art/png?seed={vowel}&size=150";

            return url;
        }
        private string GetDicebearNonAlphaAvatarUrl(string userIdentifier)
        {
            var rand = new Random();

            var randNum = rand.NextInt64(1, 6);

            var url = $"https://api.dicebear.com/8.x/pixel-art/png?seed={randNum}&size=150";

            return url;
        }

        private string GetDefaultAvatarUrl() => "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";

        private AvatarSourceCase GetAvatarSource(string userIdentifier)
        {
            AvatarSourceCase result = AvatarSourceCase.Unknown;

            var typicodeLastDigitRegex = new Regex("^.*[6-9]$");
            var sqliteLastDigitRegex = new Regex("^.*[1-5]$");
            var dicebearVowelRegex = new Regex("[aeiou]");
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
