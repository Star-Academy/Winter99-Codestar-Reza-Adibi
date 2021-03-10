using System;
using System.Collections.Generic;
using System.Text;

namespace Project_03 {
    interface IUserInterface {
        public String UserInput { get; }
        public void ShowOutput(string text);
    }
}
