using System.Collections.Generic;

namespace BuildArchitecture
{
    public class ErrorInformation
    {
        public int StartIndex { get; set; } 
        public string DisplayText { get; set; } //text display on lightbulb control
        public int Length { get; set; }
        /// <summary>
        /// Error code pattern: XXYYYY
        /// Error code : XX = ER
        /// Warning code : XX = WA
        /// Information code : XX = IF
        /// </summary>
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<ReplaceCodeInfomation> ReplaceCode { get; set; }
        public bool HasReplace
        {
            get
            {
                if (this.ReplaceCode != null) return true;
                else return false;
            }
        }
    }
}
