import java.util.ArrayList;
import java.util.HashSet;

public class Filterer {
    /**
     * get inverted index( "words => docs" database ) and initial set and list of
     * words, then use and to find docs that are included in initial set and docs
     * list of each word.
     * 
     * @param invertedIndex a "word-doc" database.
     * @param initialSet    set of docIDs( String ) or null.
     * @param words         list of words( String ).
     * @return set of docIDs( String ) or null( error ).
     */
    public static HashSet<String> AndWords(InvertedIndex invertedIndex, HashSet<String> initialSet,
            ArrayList<String> words) {
        if (words.isEmpty())
            return null;
        if (invertedIndex.IsEmpty())
            return null;
        for (String word : words) {
            if (!invertedIndex.containsWord(word))
                return new HashSet<>();
            HashSet<String> wordDocs = new HashSet<>(invertedIndex.SearchForWord(word).keySet());
            System.out.println("test");
            if (initialSet == null) {
                initialSet = wordDocs;
            } else {
                HashSet<String> initialSetTemp = new HashSet<String>();
                initialSetTemp.addAll(initialSet);
                for (String doc : initialSetTemp) {
                    if (!wordDocs.contains(doc))
                        initialSet.remove(doc);
                }
                if (initialSet.isEmpty())
                    return new HashSet<>();
            }
        }
        return initialSet;
    }
}
