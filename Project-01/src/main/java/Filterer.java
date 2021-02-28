import java.util.ArrayList;
import java.util.HashSet;
import java.util.Map;

public class Filterer {
    /**
     * get filters and create result( set of docIDs ).
     *
     * @param invertedIndex our data base of word-docIDs.
     * @param filters       "and", "or" & "not" filters.
     * @return set of docIDs that satisfy filters.
     */
    public static HashSet<String> filterDocIDs(InvertedIndex invertedIndex, Map<String, ArrayList<String>> filters) {
        HashSet<String> andResult = null;
        HashSet<String> orResult = null;
        try {
            if (!filters.get("or").isEmpty()) {
                /* docs(a|b|c) if a, b and c are "Or" filters.*/
                orResult = orWords(invertedIndex, new HashSet<>(), filters.get("or"));
            }
            if (!filters.get("and").isEmpty()) {
                /* docs((a|b|c)&(d&e&f)) if a, b and c are "Or" filters and d, e and f are "And" filters.*/
                andResult = andWords(invertedIndex, orResult, filters.get("and"));
            }
            /* we don't have "Not" filters.*/
            if (filters.get("not").isEmpty()) {
                /*if we have "And" filters return andResult otherwise return orResult.*/
                return filters.get("and").isEmpty() ? orResult : andResult;
            }
            /* we don't have "And" filters.*/
            if (!filters.get("or").isEmpty() && filters.get("and").isEmpty()) {
                /* orResult-docs(a|b|c) if a, b and c are "Not" filters.*/
                return notWords(invertedIndex, orResult, filters.get("not"));
            }
            /* we have "And" filters and we may have "Or" filters too.*/
            else if (!filters.get("and").isEmpty()) {
                /* andResult-docs(a|b|c) if a, b and c are "Not" filters.*/
                return notWords(invertedIndex, andResult, filters.get("not"));
            }
            /* we only have "Not" filters.*/
            else {
                /* AllDocs-docs(a|b|c) if a, b and c are "Not" filters.*/
                return notWords(invertedIndex, invertedIndex.getAllDocIDs(), filters.get("not"));
            }
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
            return new HashSet<>();
        }
    }

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
    public static HashSet<String> andWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (invertedIndex == null || invertedIndex.isEmpty())
            throw new Exception("database is empty!");
        if (words == null || words.isEmpty())
            throw new Exception("words List is empty!");
        String startingWord = getWordWithLeastDocIDs(invertedIndex, words);
        initialSet = removeUncommonItems(
                initialSet != null ? initialSet : invertedIndex.getWordDocIDs(startingWord),
                invertedIndex.getWordDocIDs(startingWord)
        );
        for (String word : words) {
            if (initialSet.isEmpty())
                return new HashSet<>();
            initialSet = removeUncommonItems(initialSet, invertedIndex.getWordDocIDs(word));
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
    public static HashSet<String> orWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (invertedIndex == null || invertedIndex.isEmpty())
            throw new Exception("database is empty!");
        if (words == null || words.isEmpty())
            throw new Exception("words List is empty!");
        for (String word : words) {
            if (!invertedIndex.containsWord(word))
                continue;
            HashSet<String> wordDocs = new HashSet<>(invertedIndex.getWordDocIDs(word));
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
    public static HashSet<String> notWords(InvertedIndex invertedIndex, HashSet<String> initialSet, ArrayList<String> words) throws Exception {
        if (initialSet == null || initialSet.isEmpty())
            throw new Exception("initialSet is empty!");
        if (invertedIndex == null || invertedIndex.isEmpty())
            throw new Exception("database is empty!");
        if (words == null || words.isEmpty())
            return initialSet;
        HashSet<String> wordsDocIDs = orWords(invertedIndex, new HashSet<>(), words);
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
    private static String getWordWithLeastDocIDs(InvertedIndex invertedIndex, ArrayList<String> words) throws Exception {
        String chosenWord = words.get(0);
        int minDocIDCount = Integer.MAX_VALUE;
        for (String word : words) {
            if (!invertedIndex.containsWord(word))
                throw new Exception("database doesn't contain word!");
            HashSet<String> wordDocIDs = new HashSet<>(invertedIndex.getWordDocIDs(word));
            if (wordDocIDs.size() < minDocIDCount) {
                minDocIDCount = wordDocIDs.size();
                chosenWord = word;
            }
        }
        return chosenWord;
    }
}
