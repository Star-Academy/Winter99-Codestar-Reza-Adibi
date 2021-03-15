using System;
using System.Collections.Generic;

namespace Project_03 {
    public interface IUserInterface {
        public String UserInput { get; }
        /// <summary>
        /// Show the search output to user.
        /// </summary>
        /// <param name="result">List of document ids.</param>
        public void ShowOutput(List<string> result);
    }
}
