import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class TokenizerTest {
    @Test
    public void constructorTestValidInputText() {
        assertDoesNotThrow(() -> new Tokenizer("this is tokenizer!"));
    }

    @Test
    public void constructorTestEmptyInputText() {
        assertDoesNotThrow(() -> new Tokenizer(""));
    }

    @Test
    public void getTokensTestSingleWord() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("hello"));
        Tokenizer tokenizer = new Tokenizer("hello");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getTokensTestSingleUppercaseWord() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("hello"));
        Tokenizer tokenizer = new Tokenizer("HELLO");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getTokensTestSingleNumber() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("202020"));
        Tokenizer tokenizer = new Tokenizer("202020");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getTokensTestSingleUnderLineString() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("___"));
        Tokenizer tokenizer = new Tokenizer("___");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getTokensTestWordsAndSeparators() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("___", "hell", "159357"));
        Tokenizer tokenizer = new Tokenizer("___+))\"\';:<>?/\\[]{},.\n(*&^%$#@!~=hell 159357");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void getTokensTestRepeatWord() {
        ArrayList<String> correctResult = new ArrayList<>(Arrays.asList("hello", "hello"));
        Tokenizer tokenizer = new Tokenizer("hello hello");
        ArrayList<String> testResult = tokenizer.getTokens();
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToTokenTestLowercaseString() {
        String correctResult = "hello";
        String testResult = Tokenizer.wordToToken("hello");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void stringToTokenTestUppercaseString() {
        String correctResult = "hello";
        String testResult = Tokenizer.wordToToken("HELLO");
        assertEquals(correctResult, testResult);
    }
}
