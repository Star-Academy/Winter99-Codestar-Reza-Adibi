using System;
using System.Collections.Generic;
using System.Text;

namespace Project_03 {
    public interface IOperator {
        public InvertedIndex InvertedIndex { get; }
        public string Token { get; }
        public List<string> Filter(List<string> inputList);
    }
}
