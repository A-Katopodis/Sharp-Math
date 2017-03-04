using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Math.Classes
{
    public class Buttons
    {
        public string equation{get; set;}
        public int result { get; set; }

        public Buttons(string equation , int result)
        {
            this.equation = equation;
            this.result = result;
        }
    }
}
