using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class PerhapsWillBeUseful
    {
        public static List<List<coordinate_cell>> CopyListCC(List<List<coordinate_cell>> input)
        {
            List<List<coordinate_cell>> Temp = new List<List<coordinate_cell>>();
            int i = 0;
            foreach (var row in input)
            {
                Temp.Add(new List<coordinate_cell>());
                foreach (var item in row)
                {
                    Temp[Temp.Count() - 1].Add(new coordinate_cell(item.value, item.position));
                    i++;
                }
            }
            return Temp;
            //return input;
        }
        public class coordinate_cell : IEquatable<coordinate_cell>
        {
            public double value { get; set; }
            public int position { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                coordinate_cell objAsPart = obj as coordinate_cell;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public override int GetHashCode()
            {
                return position;
            }
            public bool Equals(coordinate_cell other)
            {
                if (other == null) return false;
                return (this.position.Equals(other.position));
            }
            // Should also override == and != operators.
            public coordinate_cell(double _value, int _position)
            {
                value = _value;
                position = _position;
            }
        }
    }
}
