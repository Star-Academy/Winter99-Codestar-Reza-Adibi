import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.ArrayList;
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
    public void constructorTest() {
        assertDoesNotThrow(() -> new Filterer(invertedIndex));
        assertThrows(Exception.class, () -> new Filterer(null));
        assertThrows(Exception.class, () -> new Filterer(new InvertedIndex()));
    }

    @Test
    public void getWordWithLeastDocIDsTest() {
        ArrayList<String> words = new ArrayList<>();
        words.add("this");
        words.add("is");
        words.add("one");
        String testResult;

        try {
            String correctResult = "one";
            testResult = filterer.getWordWithLeastDocIDs(words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        words.add("dubdub");
        assertThrows(Exception.class, () -> filterer.getWordWithLeastDocIDs(words));
    }

    @Test
    public void removeUncommonItemsTest() {
        HashSet<String> firstSet = new HashSet<>();
        firstSet.add("hello");
        firstSet.add("i");
        firstSet.add("am");
        firstSet.add("john");
        HashSet<String> secondSet = new HashSet<>();
        secondSet.add("you");
        secondSet.add("know");
        secondSet.add("nothing");
        secondSet.add("john");
        HashSet<String> correctResult;
        HashSet<String> testResult;

        /* john is in both sets. */
        correctResult = new HashSet<>();
        correctResult.add("john");
        testResult = filterer.removeUncommonItems(firstSet, secondSet);
        assertEquals(correctResult, testResult);

        /* no common item in firstSet and secondSet. */
        correctResult = new HashSet<>();
        secondSet.remove("john");
        testResult = filterer.removeUncommonItems(firstSet, secondSet);
        assertEquals(correctResult, testResult);

        /* one set is empty. */
        correctResult = new HashSet<>();
        testResult = filterer.removeUncommonItems(firstSet, new HashSet<>());
        assertEquals(correctResult, testResult);
    }

    @Test
    public void notWordsTest() {
        HashSet<String> initialSet = new HashSet<>();
        ArrayList<String> words = new ArrayList<>();
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult;

        /* remove "testFile1" from initialSet( initialSet size is greater than word's docID set ).*/
        words.add("one");
        correctResult.add("testFile1");
        initialSet.add("testFile1");
        initialSet.add("testFile2");
        try {
            testResult = filterer.notWords(initialSet, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* remove "testFile1" from initialSet( initialSet size is lesser than word's docID set ).*/
        words.clear();
        words.add("is");
        correctResult.clear();
        initialSet.clear();
        initialSet.add("testFile1");
        try {
            testResult = filterer.notWords(initialSet, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* we have a valid and an invalid filter. */
        words.clear();
        words.add("one");
        words.add("hell");
        correctResult.clear();
        correctResult.add("testFile1");
        initialSet.clear();
        initialSet.add("testFile1");
        initialSet.add("testFile2");
        try {
            testResult = filterer.notWords(initialSet, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* we have no filter. */
        try {
            testResult = filterer.notWords(initialSet, new ArrayList<>());
            assertEquals(initialSet, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* initial set is empty. */
        assertThrows(Exception.class, () -> filterer.notWords(new HashSet<>(), words));
    }

    @Test
    public void andWordsTest() {
        HashSet<String> initialSet = new HashSet<>();
        ArrayList<String> words = new ArrayList<>();
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult;

        /* remove "testFile1" from initial set. */
        words.add("one");
        correctResult.add("testFile2");
        initialSet.add("testFile1");
        initialSet.add("testFile2");
        try {
            testResult = filterer.andWords(initialSet, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* two valid filters with common docID. */
        words.clear();
        words.add("one");
        words.add("is");
        correctResult.clear();
        correctResult.add("testFile2");
        initialSet.clear();
        try {
            testResult = filterer.andWords(null, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* two valid filters with uncommon docID. */
        words.clear();
        words.add("one");
        words.add("two");
        words.add("first");
        words.add("hoho");
        correctResult.clear();
        initialSet.clear();
        try {
            testResult = filterer.andWords(null, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* a valid and an invalid filter. */
        words.clear();
        words.add("one");
        words.add("hell");
        correctResult.clear();
        initialSet.clear();
        assertThrows(Exception.class, () -> filterer.andWords(null, words));

        /* we have no filters. */
        try {
            testResult = filterer.andWords(initialSet, new ArrayList<>());
            assertEquals(new HashSet<>(), testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void orWordsTest() {
        HashSet<String> initialSet = new HashSet<>();
        ArrayList<String> words = new ArrayList<>();
        HashSet<String> correctResult = new HashSet<>();
        HashSet<String> testResult;

        /* add "testFile2" to initialSet. */
        words.add("one");
        correctResult.add("testFile1");
        correctResult.add("testFile2");
        initialSet.add("testFile1");
        try {
            testResult = filterer.orWords(initialSet, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* two valid filters. */
        words.clear();
        words.add("one");
        words.add("first");
        correctResult.clear();
        correctResult.add("testFile1");
        correctResult.add("testFile2");
        initialSet.clear();
        try {
            testResult = filterer.orWords(null, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* a valid and an invalid filter. */
        words.clear();
        words.add("one");
        words.add("hell");
        correctResult.clear();
        correctResult.add("testFile2");
        initialSet.clear();
        try {
            testResult = filterer.orWords(null, words);
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }

        /* we have no filters. */
        try {
            testResult = filterer.orWords(initialSet, new ArrayList<>());
            assertEquals(new HashSet<>(), testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void filterDocIDsTest() {
        HashMap<String, ArrayList<String>> filters;
        HashSet<String> correctResult;
        HashSet<String> testResult;

        /* only "and" filter. */
        correctResult = new HashSet<>();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"and"}, new String[]{"one"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* only "or" filter. */
        correctResult.clear();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"or"}, new String[]{"one"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* only "not" filter. */
        correctResult.clear();
        correctResult.add("testFile1");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"not"}, new String[]{"one"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* "and" and "or" filters. */
        correctResult.clear();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"or", "and"}, new String[]{"one", "is"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* "not" and "or" filters. */
        correctResult.clear();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"or", "not"}, new String[]{"is", "first"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* "and" and "not" filters. */
        correctResult.clear();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"and", "not"}, new String[]{"is", "first"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* all filters. */
        correctResult.clear();
        correctResult.add("testFile2");
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"or", "and", "not"}, new String[]{"is", "test", "first"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* invalid "or" and "not" filters. */
        correctResult.clear();
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"or", "not"}, new String[]{"bye", "something"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);

        /* invalid "and" and "not" filters. */
        correctResult.clear();
        filters = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(filters, new String[]{"and", "not"}, new String[]{"something", "lala"});
        testResult = filterer.filterDocIDs(filters);
        assertEquals(correctResult, testResult);
    }
}
