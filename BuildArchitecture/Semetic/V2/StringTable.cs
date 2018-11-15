using NHibernate.Util;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** A unique set of strings mapped to a monotonically increasing index.
 *  These indexes often useful to bytecode interpreters that have instructions
 *  referring to strings by unique integer. Indexing is from 0.
 *
 *  We can also get them back out in original order.
 *
 *  Yes, I know that this is similar to {@link string#intern()} but in this
 *  case, I need the index out not just to make these strings unique.
 */
    public class StringTable
    {
        protected LinkedHashMap<string, int?> table = new LinkedHashMap<string, int?>();
        protected int index = -1; // index we have just written
        protected List<string> strings = new List<string>();

        public int? Add(string s)
        {
            int? I = table[s];
            if (I != null) return I;
            index++;
            table.Add(s, index);
            strings.Add(s);
            return index;
        }

        /** Get the ith string or null if out of range */
        public string this[int i]
        {
            get
            {
                if (i < Size() && i >= 0)
                {
                    return strings[i];
                }
                return null;
            }
            private set { }

        }

        public int Size() { return table.Count; }

        /** Return an array, possibly of length zero, with all strings
         *  sitting at their appropriate index within the array.
         */
        public string[] ToArray()
        {
            return strings.ToArray();
        }

        /** Return a List, possibly of length zero, with all strings
         *  sitting at their appropriate index within the array.
         */
        public List<string> ToList()
        {
            return strings;
        }

        public int GetNumberOfStrings()
        {
            return index + 1;
        }

        public override string ToString()
        {
            return table.ToString();
        }
    }
}
