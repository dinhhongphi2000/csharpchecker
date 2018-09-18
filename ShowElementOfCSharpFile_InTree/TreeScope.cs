using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowElementOfCSharpFile_InTree
{
    class TreeScope
    {
        public string Name;
        public string Type;
        public int Level = -1;
        public List<TreeScope> Child;
        public readonly TreeScope Parent;


        public TreeScope(TreeScope parent, string name, string type)
        {
            Console.WriteLine(parent);
            this.Parent = parent;
            this.Name = name;
            this.Type = type;
            if(parent != null)
                this.Level = parent.Level + 1;
        }

        public TreeScope AddChild(string name, string type)
        {
            if (this.Child == null)
                this.Child = new List<TreeScope>();
            bool isExist = false;
            int i;
            for(i = 0; i < this.Child.Count; i++)
            {
                if(this.Child[i].Name == name)
                {
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                //add new item
                TreeScope newItem = new TreeScope(this, name, type);
                this.Child.Add(newItem);
                return newItem;
            }
            //return existed item
            else return this.Child[i];
        }
    }
}
