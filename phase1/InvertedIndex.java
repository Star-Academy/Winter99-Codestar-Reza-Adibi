import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class InvertedIndex {
    Map<String, Map<String, ArrayList<Integer>>> invertedIndexMap = null;

    public InvertedIndex() {
        invertedIndexMap = new HashMap<String, Map<String, ArrayList<Integer>>>();
    }

    /**
     * get multuple contents( strings ) and add their words to InvertedIndex.
     * 
     * @param docs Map( documentName => documentText ).
     */
    public void AddDocumets(Map<String, String> docs) {
        for (String docID : docs.keySet()) {
            AddDocumet(docID, docs.get(docID));
        }
    }

    /**
     * get single content( string ) and add its words to InvertedIndex.
     * 
     * @param id      name of document.
     * @param content text of document.
     */
    public void AddDocumet(String id, String content) {
        String[] words = content.toLowerCase().split("\\W+");
        int wordsCount = 0;
        for (String word : words) {
            // todo: get word's base and filter usless words.
            if (!invertedIndexMap.containsKey(word))
                invertedIndexMap.put(word, new HashMap<String, ArrayList<Integer>>());
            if (!invertedIndexMap.get(word).containsKey(id))
                invertedIndexMap.get(word).put(id, new ArrayList<>());
            invertedIndexMap.get(word).get(id).add(wordsCount++);
        }
    }

    /**
     * search for a word in all indexed documents.
     * 
     * @param word the word that you want to search for it.
     * @return Map( documentName => word indexes ).
     */
    public Map<String, ArrayList<Integer>> SearchForWord(String word) {
        return invertedIndexMap.get(word);
    }
}