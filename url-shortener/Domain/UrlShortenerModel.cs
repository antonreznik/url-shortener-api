using System.Text;

namespace Domain
{
    public class UrlShortenerModel
    {
        public int Id { get; private set; }
        public string OriginalUrl { get; private set; }
        public string ShortCode { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private UrlShortenerModel()
        {

        }

        public static UrlShortenerModel Create(string originalUrl, long uniqueId)
        {
            return new UrlShortenerModel
            {
                OriginalUrl = originalUrl,
                ShortCode = Base62Encode(uniqueId),
                CreatedAt = DateTime.UtcNow
            };
        }



        private static string Base62Encode(long value)
        {
            const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (value == 0)
            {
                return "0";
            }

            var result = new StringBuilder();

            while (value > 0)
            {
                var remainder = (int)(value % 62);
                result.Insert(0, Alphabet[remainder]);
                value /= 62;
            }

            return result.ToString();
        }
    }
}
