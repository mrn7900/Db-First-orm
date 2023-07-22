using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Herobio
    {
        public int Id { get; set; }
        public int? Heroid { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string AlterEgos { get; set; }
        public string Aliases { get; set; }
        public string PlaceOfBirth { get; set; }
        public string FirstAppearance { get; set; }
        public string Publisher { get; set; }
        public string Alignment { get; set; }
    }
}
