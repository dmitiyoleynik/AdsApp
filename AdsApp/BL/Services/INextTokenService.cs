using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface INextTokenService
    {
        public string Encode(NextToken token);
        public NextToken Decode(string encodedToken);
    }
}
