using System;

namespace BuildArchitecture.Semetic.V2
{
    /** A scope to hold predefined symbols of your language. This could
     *  be a list of type names like int or methods like print.
     */
    public class PredefinedScope : BaseScope
    {

        public override string GetName()
        {
            return "predefined";
        }
    }
}
