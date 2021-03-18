using System.Collections.Generic;

namespace Project_05 {
    public class Token {
        public int ID { set; get; }
        public string TokenText { set; get; }
        public ICollection<Document> Documents { get; set; }
    }
}
