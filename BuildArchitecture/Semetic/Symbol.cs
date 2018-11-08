using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    delegate string testc();

    public class Symbol
    {
        public string Name { get; set; }
        public List<string> Modifier { get; set; }
        public string Alias { get; set; }

        public Symbol(string name, List<string> modifier = null, string alias = null)
        {
            this.Name = name;
            this.Modifier = modifier;
            this.Alias = alias;
        }

        public Symbol(string name, string[] modifier = null, string alias = null)
        {
            this.Name = name;
            this.Modifier = new List<string>(modifier);
            this.Alias = alias;
        }
    }
}
