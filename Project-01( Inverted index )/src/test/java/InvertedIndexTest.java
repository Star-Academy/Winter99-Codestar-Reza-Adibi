import org.junit.jupiter.api.Test;

import java.util.*;

import static org.junit.jupiter.api.Assertions.*;

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
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(correctResult, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID2", "0"},
                {"text2", "docID2", "1"},
        });
        invertedIndex.addDocuments(documents);
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void addDocumentTestValidDocument() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashMap<String, HashMap<String, ArrayList<Integer>>> correctResult = new HashMap<>();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(correctResult, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID1", "2"},
        });
        invertedIndex.addDocument("docID1", "test text1 test");
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void addDocumentTestEmptyDocument() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashMap<String, HashMap<String, ArrayList<Integer>>> correctResult = new HashMap<>();
        invertedIndex.addDocument("docID1", "");
        assertEquals(correctResult, invertedIndex.invertedIndexMap);
    }

    @Test
    public void getWordDocIDsTestTokenHasDocID() {
        InvertedIndex invertedIndex = new InvertedIndex();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("docID1", "docID2"));
        HashSet<String> testResult = invertedIndex.getTokenDocIDs("test");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getWordDocIDsTestTokenDoesNotHasDocID() {
        InvertedIndex invertedIndex = new InvertedIndex();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text1", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = invertedIndex.getTokenDocIDs("lala");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void isEmptyTestNotEmptyInvertedIndex() {
        InvertedIndex invertedIndex = new InvertedIndex();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        assertFalse(invertedIndex.isEmpty());
    }

    @Test
    public void isEmptyTestEmptyInvertedIndex() {
        InvertedIndex invertedIndex = new InvertedIndex();
        assertTrue(invertedIndex.isEmpty());
    }

    @Test
    public void containsWordTestValidWord() {
        InvertedIndex invertedIndex = new InvertedIndex();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        assertTrue(invertedIndex.containsWord("test"));
    }

    @Test
    public void containsWordTestInvalidWord() {
        InvertedIndex invertedIndex = new InvertedIndex();
        GeneralFunctions.addDataDirectlyToInvertedIndexMap(invertedIndex.invertedIndexMap, new String[][]{
                {"test", "docID1", "0"},
                {"text", "docID1", "1"},
                {"test", "docID2", "0"},
        });
        assertFalse(invertedIndex.containsWord("nup"));
    }

    @Test
    public void getAllDocIDsTestEmptyInvertedIndex() {
        InvertedIndex invertedIndex = new InvertedIndex();
        HashSet<String> correctResult = new HashSet<>();
        assertEquals(correctResult, invertedIndex.getAllDocIDs());
    }

    @Test
    public void getAllDocIDsTestNotEmptyInvertedIndex() {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addDocument("docID1", "text");
        invertedIndex.addDocument("docID2", "text");
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("docID1", "docID2"));
        assertEquals(correctResult, invertedIndex.getAllDocIDs());
    }
}
