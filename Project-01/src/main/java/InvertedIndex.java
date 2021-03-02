import java.util.*;

public class InvertedIndex {
    protected final HashMap<String, Map<String, ArrayList<Integer>>> invertedIndexMap;
    protected final HashSet<String> allDocIDs;

    public InvertedIndex() {
        invertedIndexMap = new HashMap<>();
        allDocIDs = new HashSet<>();
    }

    /**
     * get multiple contents( {@code String} ) and add their words to InvertedIndex.
     *
     * @param docs Map( document Name => document Text ).
     */
    public void addDocuments(Map<String, String> docs) {
        for (String docID : docs.keySet()) {
            addDocument(docID, docs.get(docID));
        }
    }

    /**
     * get single content( {@code String} ) and add its words to InvertedIndex.
     *
     * @param docID   id of document.
     * @param content text of document.
     */
    public void addDocument(String docID, String content) {
        Tokenizer tokenizer = new Tokenizer(content);
        ArrayList<String> tokens = tokenizer.getTokens();
        allDocIDs.add(docID);
        int wordsCount = 0;
        for (String token : tokens) {
            /* add new word to inverted index. */
            if (!invertedIndexMap.containsKey(token)) {
                invertedIndexMap.put(token, new HashMap<>());
            }
            /* add new docID to words docID HashSet. */
            if (!invertedIndexMap.get(token).containsKey(docID))
                invertedIndexMap.get(token).put(docID, new ArrayList<>());
            invertedIndexMap.get(token).get(docID).add(wordsCount++);
        }
    }

    /**
     * search for a word in all indexed documents.
     *
     * @param token the word token you want to search for it.
     * @return Map(document Name = > word indexes).
     */
    public HashSet<String> getTokenDocIDs(String token) {
        return new HashSet<>(invertedIndexMap.get(token).keySet());
    }

    /**
     * return true if this data structure contains no word.
     *
     * @return return {@code true} if this data structure contains no word otherwise
     * return {@code false}.
     */
    public boolean isEmpty() {
        return invertedIndexMap.isEmpty();
    }

    /**
     * Returns {@code true} if this data structure contains the word.
     *
     * @param word word whose presence in this data structure is to be tested.
     * @return return {@code true} if this data structure contains the word.
     */
    public boolean containsWord(String word) {
        return invertedIndexMap.containsKey(word);
    }

    /**
     * get set of all docIDs of this inverted index.
     *
     * @return Set of all docIDs of this inverted index.
     */
    public HashSet<String> getAllDocIDs() {
        return allDocIDs;
    }
}