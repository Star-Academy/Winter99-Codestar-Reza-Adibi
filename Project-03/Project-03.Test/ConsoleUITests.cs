using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Project_03;
using System.Diagnostics.CodeAnalysis;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class ConsoleUITests {
        [Fact]
        public void ShowOutputTestEmptyList() {
            IUserInterface ui = new ConsoleUI();
            ui.ShowOutput(new List<string>());
            Assert.True(true);
        }
        [Fact]
        public void ShowOutputTest() {
            IUserInterface ui = new ConsoleUI();
            ui.ShowOutput(new List<string> {
                "id1","id2"
            });
            Assert.True(true);
        }
    }
}
