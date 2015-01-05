using System.Collections.Generic;

namespace AppStudio.Data
{
    public static class OAuthTokensRepository
    {
        private static Dictionary<long, OAuthTokens> Tokens { get; set; }

        static OAuthTokensRepository()
        {
            Tokens = new Dictionary<long, OAuthTokens>();


            Tokens.Add(1849, new OAuthTokens
                            {
                                { "ConsumerKey", "MpwNN3gPEaamxEtXPySVlhFTP" },
                                { "ConsumerSecret", "Cy7qwtVJ1UE8YIIES2PlvIY1iWlOO7giR0Cebc3ZQmK7zJw6pq" },
                                { "AccessToken", "624504982-aJGAy2VQRI0uCNLqC2PXSSYupEve3n8SmXCWtYA2" },
                                { "AccessTokenSecret", "7aIzCfTZAk7q3S47LfHRFcDvoKfne39OFfBN1XimBhy4B" }
                            });

        }

        public static OAuthTokens GetTokens(long key)
        {
            if (Tokens.ContainsKey(key))
            {
                return Tokens[key];
            }
            return null;
        }

    }
}
