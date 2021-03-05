import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.MockedStatic;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
public class ConsoleTest {
    private Console console;
    @Mock
    private Scanner scannerMock;

    @BeforeAll
    public static void initialStaticFunctins() {
        MockedStatic<Tokenizer> mockStatic = Mockito.mockStatic(Tokenizer.class);
        when(Tokenizer.wordToToken("simple")).thenReturn("simple");
        when(Tokenizer.wordToToken("test")).thenReturn("test");
        when(Tokenizer.wordToToken("hahaha")).thenReturn("hahaha");
    }

    @BeforeEach
    public void initialMockClasses() {
        assertNotNull(scannerMock);
        console = new Console(scannerMock);
    }

    @Test
    public void getUserInputFiltersTest() {
        /* get "and" filter. */
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        when(scannerMock.nextLine()).thenReturn("simple");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        /* get "or" filter. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"or"}, new String[]{"test"});
        when(scannerMock.nextLine()).thenReturn("+test");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        /* get "not" filter. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        when(scannerMock.nextLine()).thenReturn("-hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        /* get all filters. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        when(scannerMock.nextLine()).thenReturn("simple +test -hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        /* get empty string then all filters. */
        when(scannerMock.nextLine()).thenReturn("").thenReturn("simple +test -hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToFilterTest() {
        HashMap<String, ArrayList<String>> correctResult;
        HashMap<String, ArrayList<String>> testResult = null;

        /* find "and" filter in string. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        try {
            testResult = console.stringToFilter("simple");
        } catch (Exception e) {
            e.printStackTrace();
        }
        assertEquals(correctResult, testResult);

        /* find "or" filter in string. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"or"}, new String[]{"test"});
        try {
            testResult = console.stringToFilter("+test");
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
        assertEquals(correctResult, testResult);

        /* find "not" filter in string. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        try {
            testResult = console.stringToFilter("-hahaha");
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
        assertEquals(correctResult, testResult);

        /* find all filters in string. */
        correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToAnswerMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        try {
            testResult = console.stringToFilter("simple +test -hahaha");
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
        assertEquals(correctResult, testResult);

        /* get empty string. */
        assertThrows(Exception.class, () -> console.stringToFilter(""));
    }

}
