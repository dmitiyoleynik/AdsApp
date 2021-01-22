using BL.Models;

namespace BL.Services
{
    public interface INextTokenService
    {
        public string Encode(NextToken token);
        public NextToken Decode(string encodedToken);
    }
}
