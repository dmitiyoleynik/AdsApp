using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace BL.DTO
{
    public class AdResponse
    {
        public Ad Ad { get; set; }
        public string Token { get; set; }
    }
}
