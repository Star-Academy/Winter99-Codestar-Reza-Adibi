using System;
using System.Collections.Generic;

namespace Project_05 {
    public interface IUserInterface {
        string UserInput { get; }
        string UserDataPath { get; }

        /// <summary>
        /// Show the search output to user.
        /// </summary>
        /// <param name="result"> List of document ids. </param>
        void ShowOutput(List<string> result);
    }
}
