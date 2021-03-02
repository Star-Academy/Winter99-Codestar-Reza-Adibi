import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

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
        correctResult.put("test", new HashMap<>());
        correctResult.put("text1", new HashMap<>());
        correctResult.put("text2", new HashMap<>());
        correctResult.get("test").put("docID1", new ArrayList<>());
        correctResult.get("test").put("docID2", new ArrayList<>());
        correctResult.get("text1").put("docID1", new ArrayList<>());
        correctResult.get("text2").put("docID2", new ArrayList<>());
        correctResult.get("test").get("docID1").add(0);
        correctResult.get("test").get("docID2").add(0);
        correctResult.get("text1").get("docID1").add(1);
        correctResult.get("text2").get("docID2").add(1);

        invertedIndex.addDocuments(documents);
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void addDocumentTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashMap<String, HashMap<String, ArrayList<Integer>>> correctResult = new HashMap<>();
        correctResult.put("test", new HashMap<>());
        correctResult.put("text1", new HashMap<>());
        correctResult.get("test").put("docID1", new ArrayList<>());
        correctResult.get("text1").put("docID1", new ArrayList<>());
        correctResult.get("test").get("docID1").add(0);
        correctResult.get("test").get("docID1").add(2);
        correctResult.get("text1").get("docID1").add(1);

        invertedIndex.addDocument("docID1", "test text1 test");
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void getWordDocIDsTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.invertedIndexMap.put("test", new HashMap<>());
        invertedIndex.invertedIndexMap.put("text1", new HashMap<>());
        invertedIndex.invertedIndexMap.get("test").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").put("docID2", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("text1").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").get("docID1").add(0);
        invertedIndex.invertedIndexMap.get("test").get("docID2").add(0);
        invertedIndex.invertedIndexMap.get("text1").get("docID1").add(1);
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
    }

    @Test
    public void isEmptyTest() {
        InvertedIndex invertedIndex = new InvertedIndex();

        assertEquals(true, invertedIndex.isEmpty());

        invertedIndex.invertedIndexMap.put("test", new HashMap<>());
        invertedIndex.invertedIndexMap.put("text1", new HashMap<>());
        invertedIndex.invertedIndexMap.get("test").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").put("docID2", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("text1").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").get("docID1").add(0);
        invertedIndex.invertedIndexMap.get("test").get("docID2").add(0);
        invertedIndex.invertedIndexMap.get("text1").get("docID1").add(1);

        assertEquals(false, invertedIndex.isEmpty());
    }

    @Test
    public void containsWordTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.invertedIndexMap.put("test", new HashMap<>());
        invertedIndex.invertedIndexMap.put("text1", new HashMap<>());
        invertedIndex.invertedIndexMap.get("test").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").put("docID2", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("text1").put("docID1", new ArrayList<>());
        invertedIndex.invertedIndexMap.get("test").get("docID1").add(0);
        invertedIndex.invertedIndexMap.get("test").get("docID2").add(0);
        invertedIndex.invertedIndexMap.get("text1").get("docID1").add(1);

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
}
