using MLCCommonLibrary.Model.Violation.Violator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLCCommonLibrary.Model.Violation
{
    public class Comment : Users
    {

        public Comment()
        {
            this.Comments = new ViolatorComment(); 
        }
        public ViolatorComment Comments { get; set; }
       
    }
}
