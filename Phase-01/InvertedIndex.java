import java.util.*;

public class InvertedIndex {
    private Map<String, Map<String, ArrayList<Integer>>> invertedIndexMap = null;
    private Set<String> allDocIDs = null;

    public InvertedIndex() {
        invertedIndexMap = new HashMap<String, Map<String, ArrayList<Integer>>>();
        allDocIDs = new HashSet<String>();
    }

    /**
     * get multuple contents( {@code String} ) and add their words to InvertedIndex.
     *
     * @param docs Map( document Name => document Text ).
     */
    public void AddDocumets(Map<String, String> docs) {
        for (String docID : docs.keySet()) {
            allDocIDs.add(docID);
            AddDocumet(docID, docs.get(docID));
        }
    }

    /**
     * get single content( {@code String} ) and add its words to InvertedIndex.
     *
     * @param id      id of document.
     * @param content text of document.
     */
    public void AddDocumet(String id, String content) {
        String[] words = content.toLowerCase().split("\\W+");
        int wordsCount = 0;
        for (String word : words) {
            // todo: get word's base and filter usless words.
            /* add new word to inverted index. */
            if (!invertedIndexMap.containsKey(word)) {
                invertedIndexMap.put(word, new HashMap<String, ArrayList<Integer>>());
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
    public Map<String, ArrayList<Integer>> SearchForWord(String word) {
        return invertedIndexMap.get(word);
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
    public Set<String> GetAllDocIDs() {
        return allDocIDs;
    }
}