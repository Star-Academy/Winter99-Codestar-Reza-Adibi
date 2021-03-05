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
    public void getUserInputFiltersTestSingleAndFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        when(scannerMock.nextLine()).thenReturn("simple");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getUserInputFiltersTestSingleOrFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"or"}, new String[]{"test"});
        when(scannerMock.nextLine()).thenReturn("+test");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getUserInputFiltersTestSingleNotFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        when(scannerMock.nextLine()).thenReturn("-hahaha");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getUserInputFiltersTestAllFilters() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        when(scannerMock.nextLine()).thenReturn("simple +test -hahaha");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getUserInputFiltersTestEmptyInput() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        when(scannerMock.nextLine()).thenReturn("").thenReturn("simple +test -hahaha");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToFilterTestSingleAndFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        try {
            HashMap<String, ArrayList<String>> testResult = console.stringToFilter("simple");
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void stringToFilterTestSingleOrFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"or"}, new String[]{"test"});
        try {
            HashMap<String, ArrayList<String>> testResult = console.stringToFilter("+test");
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void stringToFilterTestSingleNotFilter() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        try {
            HashMap<String, ArrayList<String>> testResult = console.stringToFilter("-hahaha");
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void stringToFilterTestAllFilters() {
        HashMap<String, ArrayList<String>> correctResult = GeneralFunctions.initialFiltersMap();
        GeneralFunctions.insertDataToFiltersMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        try {
            HashMap<String, ArrayList<String>> testResult = console.stringToFilter("simple +test -hahaha");
            assertEquals(correctResult, testResult);
        } catch (Exception e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void stringToFilterTestEmptyString() {
        assertThrows(Exception.class, () -> console.stringToFilter(""));
    }

}
