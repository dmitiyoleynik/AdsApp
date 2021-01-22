using BL.Models;
using System.Text.Json;

namespace BL.Services
{
    public class NextTokenService : INextTokenService
    {
        public NextToken Decode(string token)
        {
            var decodedToken = Base64Decode(token);
            var deserializedToken = JsonSerializer.Deserialize<NextToken>(decodedToken);

            return deserializedToken;
        }

        public string Encode(NextToken token)
        {
            var serializedToken = JsonSerializer.Serialize(token);
            var encodedToken = Base64Encode(serializedToken);

            return encodedToken;
        }

        public string Base64Encode(string text)
        {
            var TextBytes = System.Text.Encoding.UTF8.GetBytes(text);

            return System.Convert.ToBase64String(TextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
