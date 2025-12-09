using Projekcik.Entities;
using System.Collections.Generic;

namespace Projekcik.Models
{
    public class BracketViewModel
    {
        public List<Games> Matches { get; set; }
        public int CurrentRound { get; set; }
    }
}
