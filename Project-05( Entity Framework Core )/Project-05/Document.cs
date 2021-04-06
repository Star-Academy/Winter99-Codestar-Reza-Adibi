using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_05 {
    public class Document {
        [ExcludeFromCodeCoverage]
        public int ID { set; get; }
        [ExcludeFromCodeCoverage]
        public string DocumentPath { set; get; }
        [ExcludeFromCodeCoverage]
        public ICollection<Token> Tokens { get; set; }
    }
}
