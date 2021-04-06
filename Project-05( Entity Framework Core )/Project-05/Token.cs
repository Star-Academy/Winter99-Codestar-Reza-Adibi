using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_05 {
    public class Token {
        [ExcludeFromCodeCoverage]
        public int ID { set; get; }
        [ExcludeFromCodeCoverage]
        public string TokenText { set; get; }
        [ExcludeFromCodeCoverage]
        public ICollection<Document> Documents { get; set; }
    }
}
