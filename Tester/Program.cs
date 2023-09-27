using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.dB
{
   
    public class Program
    {
        public static void Main(string[] args)
        {
            IdB_Tester tester = new dB_Tester_mongoDB();
            tester.AnExample();
        }
    }
}