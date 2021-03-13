using System;

namespace Project_03 {
    interface IUserInterface {
        public String UserInput { get; }
        public void ShowOutput(string text);
    }
}
