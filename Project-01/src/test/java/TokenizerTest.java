import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.HashSet;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class TokenizerTest {
    @Test
    public void constructorTest() {
        assertDoesNotThrow(() -> new Tokenizer("this is tokenizer!"));

        assertDoesNotThrow(() -> new Tokenizer(""));
    }

    @Test
    public void getTokensTest() {
        Tokenizer tokenizer;
        ArrayList<String> correctResult = new ArrayList<>();
        ArrayList<String> testResult;

        /* single word. */
        correctResult.add("hello");
        tokenizer = new Tokenizer("hello");
        testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);

        /* single number. */
        correctResult.clear();
        correctResult.add("202020");
        tokenizer = new Tokenizer("202020");
        testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);

        /* single word with upper case characters. */
        correctResult.clear();
        correctResult.add("hello");
        tokenizer = new Tokenizer("HeLlO");
        testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);

        /* word, underline and some invalid characters. */
        correctResult.clear();
        correctResult.add("___");
        correctResult.add("hello");
        tokenizer = new Tokenizer(">><<___----hello+-*/");
        testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);

        /* two same words. */
        correctResult.clear();
        correctResult.add("hello");
        correctResult.add("hello");
        tokenizer = new Tokenizer("hello hello");
        testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToTokenTest() {
        String correctResult;
        String testResult;

        correctResult = "hello";
        testResult = Tokenizer.wordToToken("hello");
        assertEquals(correctResult, testResult);

        correctResult = "hello";
        testResult = Tokenizer.wordToToken("HeLLo");
        assertEquals(correctResult, testResult);
    }
}
