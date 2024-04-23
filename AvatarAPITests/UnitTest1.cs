using AvatarAPI.Services;
using System.Text.RegularExpressions;

namespace AvatarAPITests
{
    public class AvatarUrlTests
    {
        IAvatarService _avatarService;
        public AvatarUrlTests()
        {
            _avatarService = new AvatarService();
        }

        [Fact]
        public void UserEndingSixToNine_ReturnsTypicodeUrl()
        {
            for (int i = 6; i <= 9; i++)
            {
                var testUser = $"testuser{i}";

                var url = _avatarService.GetAvatarUrl(testUser);

                Assert.Matches($"{Regex.Escape("https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/")}{i}", url);
            }
        }

        [Fact]
        public void UserEndingOneToFive_ReturnsDatabaseUrl()
        {
            for (int i = 1; i <= 5; i++)
            {
                var testUser = $"testuser{i}";

                var url = _avatarService.GetAvatarUrl(testUser);

                Assert.False(String.IsNullOrEmpty(url));
            }
        }

        [Fact]
        public void UserContainingVowel_ReturnsDicebearVowelUrl()
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            foreach (var vowel in vowels)
            {
                var testUser = $"bb{vowel}cc";

                var url = _avatarService.GetAvatarUrl(testUser);

                Assert.Matches($"{Regex.Escape("https://api.dicebear.com/8.x/pixel-art/png?seed=")}{vowel}{Regex.Escape("&size=150")}", url);
            }
        }

        [Fact]
        public void UserContainingNonAlphaNum_ReturnsDicebearRandUrl()
        {
            char[] somenonalphanums = new char[] { '#', '+', '=', '-', '$', '_' };

            foreach (var nonalphanum in somenonalphanums)
            {
                var testUser = $"bb{nonalphanum}cc";

                var url = _avatarService.GetAvatarUrl(testUser);

                Assert.Matches($"{Regex.Escape("https://api.dicebear.com/8.x/pixel-art/png?seed=")}[1-5]{Regex.Escape("&size=150")}", url);
            }
        }

        [Fact]
        public void UserCatchallCondition_ReturnsDefaultUrl()
        {
            var testUser = "bbcc";

            var url = _avatarService.GetAvatarUrl(testUser);

            Assert.Matches(Regex.Escape("https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150"), url);
        }
    }
}