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
    public static HashSet<String> AndWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (invertedIndex == null || invertedIndex.IsEmpty())
            throw new Exception("database is empty!");
        if (words == null || words.isEmpty())
            throw new Exception("words List is empty!");
        String startingWord = GetWordWithLeastDocIDs(invertedIndex, words);
        initialSet = removeUncommonItems(
                initialSet != null ? initialSet : (HashSet<String>) invertedIndex.SearchForWord(startingWord).keySet(),
                (HashSet<String>) invertedIndex.SearchForWord(startingWord).keySet()
        );
        for (String word : words) {
            if (initialSet.isEmpty())
                return new HashSet<>();
            initialSet = removeUncommonItems(initialSet, (HashSet<String>) invertedIndex.SearchForWord(word).keySet());
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
    public static HashSet<String> OrWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (invertedIndex == null || invertedIndex.IsEmpty())
            throw new Exception("database is empty!");
        if (words == null || words.isEmpty())
            throw new Exception("words List is empty!");
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

    /**
     * get "inverted index( "words => docIDs" database )" and "initial set" and
     * "list of words", then remove all docIDs of each word from "initial set" and return
     * it.
     *
     * @param invertedIndex a "word-docID" database.
     * @param initialSet    set of docIDs( String ).
     * @param words         list of words( String ).
     * @return set of docIDs( String ) or null( error ).
     */
    public static HashSet<String> NotWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (words == null || words.isEmpty())
            throw new Exception("words List is empty!");
        if (initialSet == null || initialSet.isEmpty())
            throw new Exception("initialSet is empty!");
        if (invertedIndex == null || invertedIndex.IsEmpty())
            throw new Exception("database is empty!");
        HashSet<String> wordsDocIDs = OrWords(invertedIndex, new HashSet<>(), words);
        /* check which one is a smaller set, initialSet or wordsDocIDs then remove common items by looping trough smaller set. */
        if (initialSet.size() > wordsDocIDs.size()) {
            for (String docID : wordsDocIDs) {
                initialSet.remove(docID);
            }
        } else {
            HashSet<String> initialSetTemp = new HashSet<>(initialSet);
            for (String docID : initialSetTemp
            ) {
                if (wordsDocIDs.contains(docID))
                    initialSet.remove(docID);
            }
        }
        return initialSet;
    }

    /**
     * remove uncommon items from firstSet.
     *
     * @param firstSet  base set of items.
     * @param secondSet set of items to find common items in base set.
     * @return set of common items.
     */
    private static HashSet<String> removeUncommonItems(HashSet<String> firstSet, HashSet<String> secondSet) {
        HashSet<String> firstSetTemp = new HashSet<>(firstSet);
        for (String docID : firstSetTemp) {
            if (!secondSet.contains(docID))
                firstSet.remove(docID);
        }
        if (firstSet.isEmpty())
            return new HashSet<>();
        return firstSet;
    }

    /**
     * get docID sets of all given words and return word with least docIDs.
     *
     * @param invertedIndex our word-docID database.
     * @param words         list of words.
     * @return word with least docIDs.
     * @throws Exception
     */
    private static String GetWordWithLeastDocIDs(InvertedIndex invertedIndex, ArrayList<String> words) throws Exception {
        String chosenWord = words.get(0);
        int minDocIDCount = Integer.MAX_VALUE;
        for (String word : words) {
            if (!invertedIndex.containsWord(word))
                throw new Exception("database doesn't contain word!");
            HashSet<String> wordDocIDs = new HashSet<>(invertedIndex.SearchForWord(word).keySet());
            if (wordDocIDs.size() < minDocIDCount) {
                minDocIDCount = wordDocIDs.size();
                chosenWord = word;
            }
        }
        return chosenWord;
    }
}
