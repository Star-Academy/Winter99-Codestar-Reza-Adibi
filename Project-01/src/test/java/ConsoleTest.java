import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;


import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
public class ConsoleTest {
    private Console console;
    @Mock
    private Scanner scannerMock;

    @BeforeEach
    public void initial() {
        assertNotNull(scannerMock);
        console = new Console(scannerMock);
    }

    @Test
    public void getUserInputFiltersTest() {
        HashMap<String, ArrayList<String>> correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        when(scannerMock.nextLine()).thenReturn("simple");
        HashMap<String, ArrayList<String>> testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"or"}, new String[]{"test"});
        when(scannerMock.nextLine()).thenReturn("+test");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        when(scannerMock.nextLine()).thenReturn("-hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        when(scannerMock.nextLine()).thenReturn("simple +test -hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);

        when(scannerMock.nextLine()).thenReturn("").thenReturn("simple +test -hahaha");
        testResult = console.getUserInputFilters();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToFilterTest() {
        HashMap<String, ArrayList<String>> correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"and"}, new String[]{"simple"});
        HashMap<String, ArrayList<String>> testResult = null;
        try {
            testResult = console.stringToFilter("simple");
        } catch (Exception e) {
            e.printStackTrace();
        }
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"or"}, new String[]{"test"});
        try {
            testResult = console.stringToFilter("+test");
        } catch (Exception e) {
            e.printStackTrace();
        }
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"not"}, new String[]{"hahaha"});
        try {
            testResult = console.stringToFilter("-hahaha");
        } catch (Exception e) {
            e.printStackTrace();
        }
        assertEquals(correctResult, testResult);

        correctResult = initialAnswersMap();
        insertDataToAnswerMap(correctResult, new String[]{"and", "or", "not"}, new String[]{"simple", "test", "hahaha"});
        try {
            testResult = console.stringToFilter("simple +test -hahaha");
        } catch (Exception e) {
            e.printStackTrace();
        }
        assertEquals(correctResult, testResult);

        assertThrows(Exception.class, () -> console.stringToFilter(""));
    }

    private HashMap initialAnswersMap() {
        HashMap<String, ArrayList<String>> map = new HashMap<>();
        map.put("and", new ArrayList<>());
        map.put("or", new ArrayList<>());
        map.put("not", new ArrayList<>());
        return map;
    }

    private void insertDataToAnswerMap(HashMap<String, ArrayList<String>> answerMap, String[] filterTypes, String[] filterValues) {
        for (int i = 0; i < filterTypes.length; i++) {
            answerMap.get(filterTypes[i]).add(filterValues[i]);
        }
    }
}
