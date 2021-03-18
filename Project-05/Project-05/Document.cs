using System.Collections.Generic;

namespace Project_05 {
    public class Document {
        public int ID { set; get; }
        public string DocumentPath { set; get; }
        public ICollection<Token> Tokens { get; set; }
    }
}
