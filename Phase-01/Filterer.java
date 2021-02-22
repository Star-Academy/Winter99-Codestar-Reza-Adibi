import java.util.ArrayList;
import java.util.HashSet;

public class Filterer {
    /**
     * get inverted index( "words => docIDs" database ) and initial set and list of
     * words, then use "and" to find docIDs that are included in initial set and
     * docIDs list of each word.
     * 
     * @param invertedIndex a "word-docID" database.
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
            HashSet<String> wordDocIDs = new HashSet<>(invertedIndex.SearchForWord(word).keySet());
            if (initialSet == null) {
                initialSet = wordDocIDs;
            } else {
                HashSet<String> initialSetTemp = new HashSet<String>();
                initialSetTemp.addAll(initialSet);
                for (String docID : initialSetTemp) {
                    if (!wordDocIDs.contains(docID))
                        initialSet.remove(docID);
                }
                if (initialSet.isEmpty())
                    return new HashSet<>();
            }
        }
        return initialSet;
    }

    /**
     * get "inverted index( "words => docIDs" database )" and "initial set" and
     * "list of words", then add all docIDs of each word to "initial set" and return
     * it.
     * 
     * @param invertedIndex a "word-docID" database.
     * @param initialSet    set of docIDs( String ) or null.
     * @param words         list of words( String ).
     * @return set of docIDs( String ) or null( error ).
     */
    public static HashSet<String> OrWords(InvertedIndex invertedIndex, HashSet<String> initialSet,
            ArrayList<String> words) {
        if (words.isEmpty())
            return null;
        if (invertedIndex.IsEmpty())
            return null;
        for (String word : words) {
            if (!invertedIndex.containsWord(word))
                continue;
            HashSet<String> wordDocs = new HashSet<>(invertedIndex.SearchForWord(word).keySet());
            if (initialSet == null) {
                initialSet = wordDocs;
            } else {
                initialSet.addAll(wordDocs);
            }
        }
        return initialSet;
    }
}
