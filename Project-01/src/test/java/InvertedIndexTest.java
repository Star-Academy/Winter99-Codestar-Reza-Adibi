import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class InvertedIndexTest {

    @Test
    public void constructorTest() {
        assertDoesNotThrow(() -> new InvertedIndex());
    }

    @Test
    public void addDocumentsTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashMap<String, String> documents = new HashMap<>();
        documents.put("docID1", "test text1");
        documents.put("docID2", "test text2");
        HashMap<String, HashMap<String, ArrayList<Integer>>> correctResult = new HashMap<>();
        addDataDirectlyToInvertedIndexMap(correctResult, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID2", "0"},
                {"text2", "docID2", "1"},
        });
        invertedIndex.addDocuments(documents);
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void addDocumentTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashMap<String, HashMap<String, ArrayList<Integer>>> correctResult = new HashMap<>();
        addDataDirectlyToInvertedIndexMap(correctResult, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID1", "2"},
        });
        invertedIndex.addDocument("docID1", "test text1 test");
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void getWordDocIDsTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult;

        correctResult.add("docID1");
        testResult = invertedIndex.getTokenDocIDs("text1");
        assertEquals(correctResult, testResult);

        correctResult.clear();
        correctResult.add("docID1");
        correctResult.add("docID2");
        testResult = invertedIndex.getTokenDocIDs("test");
        assertEquals(correctResult, testResult);

        correctResult.clear();
        testResult = invertedIndex.getTokenDocIDs("lala");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void isEmptyTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        assertEquals(false, invertedIndex.isEmpty());

        invertedIndex.invertedIndexMap.clear();
        assertEquals(true, invertedIndex.isEmpty());
    }

    @Test
    public void containsWordTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text", "docID1", "1"},
                {"test", "docID2", "0"},
        });

        assertEquals(true, invertedIndex.containsWord("test"));

        assertEquals(false, invertedIndex.containsWord("nup"));
    }

    @Test
    public void getAllDocIDsTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashSet<String> correctResult = new HashSet<>();

        assertEquals(correctResult, invertedIndex.getAllDocIDs());

        invertedIndex.addDocument("docID1", "text");
        invertedIndex.addDocument("docID2", "text");
        correctResult.add("docID1");
        correctResult.add("docID2");
        assertEquals(correctResult, invertedIndex.getAllDocIDs());
    }

    /**
     * get array of arrays of strings and return HashMap of HashMaps of ArrayLists.
     *
     * @param map                     ex: [["hello", "docID1", "0"], ["hell", "docID2", "0"]].
     * @param arrayOfWord_DocID_Index ex:{"hello"=>{"docID1"=>[0]}, "hell"=>{"docID2"=>[0]}}.
     */
    public void addDataDirectlyToInvertedIndexMap(HashMap<String, HashMap<String, ArrayList<Integer>>> map, String[][] arrayOfWord_DocID_Index) {
        for (String[] word_DocID_Index : arrayOfWord_DocID_Index) {
            if (!map.containsKey(word_DocID_Index[0]))
                map.put(word_DocID_Index[0], new HashMap<>());
            if (!map.get(word_DocID_Index[0]).containsKey(word_DocID_Index[1]))
                map.get(word_DocID_Index[0]).put(word_DocID_Index[1], new ArrayList<>());
            map.get(word_DocID_Index[0]).get(word_DocID_Index[1]).add(Integer.parseInt(word_DocID_Index[2]));
        }
    }
}
