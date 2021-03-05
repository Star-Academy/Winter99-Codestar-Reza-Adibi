import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.*;

@ExtendWith(MockitoExtension.class)
public class FiltererTest {
    private Filterer filterer;
    static InvertedIndex invertedIndex;

    static {
        invertedIndex = new InvertedIndex();
        invertedIndex.addDocument("testFile1", "this is the first test FiLe hoho!");
        invertedIndex.addDocument("testFile2", "this is Not the test FiLe one it's tf two!");
    }

    @BeforeEach
    public void initialClasses() {
        try {
            filterer = new Filterer(invertedIndex);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void constructorTestInputValidInvertedIndex() {
        assertDoesNotThrow(() -> new Filterer(invertedIndex));
    }

    @Test
    public void constructorTestInputEmptyValidInvertedIndex() {
        assertThrows(Exception.class, () -> new Filterer(new InvertedIndex()));
    }

    @Test
    public void constructorTestInputNull() {
        assertThrows(Exception.class, () -> new Filterer(null));
    }

    @Test
    public void getWordWithLeastDocIDsTestWordsAreValid() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("this", "is", "one"));
        String correctResult = "one";
        try {
            String testResult = filterer.getWordWithLeastDocIDs(words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void getWordWithLeastDocIDsTestSomeWordsAreInvalid() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("dubdub"));
        assertThrows(Exception.class, () -> filterer.getWordWithLeastDocIDs(words));
    }

    @Test
    public void removeUncommonItemsTestCommonItemExists() {
        HashSet<String> firstSet = new HashSet<>(Arrays.asList("hello", "i", "am", "john"));
        HashSet<String> secondSet = new HashSet<>(Arrays.asList("you", "know", "nothing", "john"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("john"));
        HashSet<String> testResult = filterer.removeUncommonItems(firstSet, secondSet);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void removeUncommonItemsTestCommonItemDoesNotExists() {
        HashSet<String> firstSet = new HashSet<>(Arrays.asList("hello", "i", "am", "john"));
        HashSet<String> secondSet = new HashSet<>(Arrays.asList("you", "know", "nothing"));
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.removeUncommonItems(firstSet, secondSet);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void removeUncommonItemsTestOneSetIsEmpty() {
        HashSet<String> firstSet = new HashSet<>(Arrays.asList("hello", "i", "am", "john"));
        HashSet<String> secondSet = new HashSet<>();
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.removeUncommonItems(firstSet, secondSet);
        assertEquals(correctResult, testResult);
    }

    /**
     * Scenario1: initialSet size is greater than words docIDSet size.
     */
    @Test
    public void notWordsTestRemoveFromInitialSetScenario1() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one"));
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile1"));
        HashSet<String> testResult = filterer.notWords(initialSet, words);
        assertEquals(correctResult, testResult);
    }

    /**
     * Scenario2: initialSet size is lesser than words docIDSet size.
     */
    @Test
    public void notWordsTestRemoveFromInitialSetScenario2() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("is"));
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1"));
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.notWords(initialSet, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void notWordsTestInvalidFilter() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("hell"));
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> testResult = filterer.notWords(initialSet, words);
        assertEquals(initialSet, testResult);
    }

    @Test
    public void notWordsTestEmptyFilterList() {
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> testResult = filterer.notWords(initialSet, new ArrayList<>());
        assertEquals(initialSet, testResult);
    }

    @Test
    public void notWordsTestEmptyInitialSet() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one"));
        HashSet<String> testResult = filterer.notWords(new HashSet<>(), words);
        assertEquals(new HashSet<>(), testResult);
    }

    @Test
    public void andWordsTestReMoveFromInitialSet() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one"));
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.andWords(initialSet, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void andWordsTestMultipleFiltersWithCommonDocID() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one", "is"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.andWords(null, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void andWordsTestMultipleFiltersWithoutCommonDocID() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one", "two", "first"));
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.andWords(null, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void andWordsTestInvalidFilter() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("hell"));
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.andWords(null, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void andWordsTestEmptyFilterList() {
        HashSet<String> testResult = filterer.andWords(null, new ArrayList<>());
        assertEquals(new HashSet<>(), testResult);
    }

    @Test
    public void orWordsTestAddToInitialSet() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one"));
        HashSet<String> initialSet = new HashSet<>(Arrays.asList("testFile1"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> testResult = filterer.orWords(initialSet, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void orWordsTestMultipleFilters() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("one", "first"));
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile1", "testFile2"));
        HashSet<String> testResult = filterer.orWords(null, words);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void orWordsTestInvalidFilter() {
        ArrayList<String> words = new ArrayList<>(Arrays.asList("hell"));
        HashSet<String> testResult = filterer.orWords(null, words);
        assertNull(testResult);
    }

    @Test
    public void orWordsTestEmptyFilterList() {
        HashSet<String> initialSet = null;
        HashSet<String> testResult = filterer.orWords(initialSet, new ArrayList<>());
        assertEquals(initialSet, testResult);
    }

    @Test
    public void filterDocIDsTestSingleAndFilter() {
        HashSet<String> correctResult = new HashSet<>();
        correctResult.add("testFile2");
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"and"}, new String[]{"one"});
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestSingleOrFilter() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"or"}, new String[]{"one"});
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestSingleNotFilter() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile1"));
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"not"}, new String[]{"one"});
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestSingleAndSingleOrFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"or", "and"}, new String[]{"one", "is"});
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestSingleNotSingleOrFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"or", "not"}, new String[]{"is", "first"});
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestSingleAndSingleNotFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"and", "not"}, new String[]{"is", "first"});
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestAllFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"or", "and", "not"}, new String[]{"is", "test", "first"});
        HashSet<String> correctResult = new HashSet<>(Arrays.asList("testFile2"));
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestInvalidSingleOrInvalidSingleNotFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"or", "not"}, new String[]{"bye", "something"});
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }

    @Test
    public void filterDocIDsTestInvalidSingleAndInvalidSingleNotFilters() {
        HashMap<String, ArrayList<String>> filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(filters, new String[]{"and", "not"}, new String[]{"something", "lala"});
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }
}
