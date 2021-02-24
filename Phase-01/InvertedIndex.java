import java.util.*;

public class InvertedIndex {
    private final Map<String, Map<String, ArrayList<Integer>>> invertedIndexMap;
    private final Set<String> allDocIDs;

    public InvertedIndex() {
        invertedIndexMap = new HashMap<>();
        allDocIDs = new HashSet<>();
    }

    /**
     * get multiple contents( {@code String} ) and add their words to InvertedIndex.
     *
     * @param docs Map( document Name => document Text ).
     */
    public void AddDocuments(Map<String, String> docs) {
        for (String docID : docs.keySet()) {
            allDocIDs.add(docID);
            AddDocument(docID, docs.get(docID));
        }
    }

    /**
     * get single content( {@code String} ) and add its words to InvertedIndex.
     *
     * @param id      id of document.
     * @param content text of document.
     */
    public void AddDocument(String id, String content) {
        String[] words = content.toLowerCase().split("\\W+");
        int wordsCount = 0;
        for (String word : words) {
            // todo: get word's base and filter useless words.
            /* add new word to inverted index. */
            if (!invertedIndexMap.containsKey(word)) {
                invertedIndexMap.put(word, new HashMap<>());
            }
            /* add new docID to words docID HashSet. */
            if (!invertedIndexMap.get(word).containsKey(id))
                invertedIndexMap.get(word).put(id, new ArrayList<>());
            invertedIndexMap.get(word).get(id).add(wordsCount++);
        }
    }

    /**
     * search for a word in all indexed documents.
     *
     * @param word the word that you want to search for it.
     * @return Map(document Name = > word indexes).
     */
    public HashSet<String> GetWordDocIDs(String word) {
        return (HashSet<String>) invertedIndexMap.get(word).keySet();
    }

    /**
     * return true if this data structure contains no word.
     *
     * @return return {@code true} if this data structure contains no word otherwise
     * return {@code false}.
     */
    public boolean IsEmpty() {
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
    public HashSet<String> GetAllDocIDs() {
        return (HashSet<String>) allDocIDs;
    }
}