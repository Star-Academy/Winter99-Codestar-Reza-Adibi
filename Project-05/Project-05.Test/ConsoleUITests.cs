using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Project_05;
using System.Diagnostics.CodeAnalysis;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class ConsoleUITests {
        private IUserInterface ui;
        public ConsoleUITests() {
            ui = new ConsoleUI();
        }
        [Fact]
        public void ShowOutputTestEmptyList() {
            ui.ShowOutput(new List<string>());
            Assert.True(true);
        }
        [Fact]
        public void ShowOutputTest() {
            ui.ShowOutput(new List<string> {
                "id1","id2"
            });
            Assert.True(true);
        }
    }
}
