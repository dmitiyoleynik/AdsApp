﻿using System;

namespace DAL.EFModels
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
