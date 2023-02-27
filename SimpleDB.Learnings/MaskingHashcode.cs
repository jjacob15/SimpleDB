using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleDB.Learnings
{
    public class MaskingHashcode
    {
        public void Run()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var name = string.Join("",Enumerable.Repeat(characters, 100).Select(x=>x[random.Next(characters.Length)]));

            var hashcode = name.GetHashCode();
            var removeSignificant = hashcode & 0x00ffffff;

        }
    }
}
