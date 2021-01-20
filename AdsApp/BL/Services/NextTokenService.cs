using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BL.Services
{
    class NextTokenService : INextTokenService
    {
        public NextTokenService()
        {

        }
        public NextToken Decode(string encodedToken)
        {
            var decodedToken = JsonSerializer.Deserialize<NextToken>(encodedToken);

            return decodedToken;
        }

        public string Encode(NextToken token)
        {
            var encodedToken = JsonSerializer.Serialize<NextToken>(token);

            return encodedToken;
        }
    }
}
