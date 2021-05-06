import java.util.ArrayList;
import java.util.Arrays;
import java.util.Locale;

public class Tokenizer {
    final String inputText;
    static String tokenRegEx = "\\W+";

    public Tokenizer(String text) {
        inputText = text;
    }

    /**
     * generate tokens and return them.
     *
     * @return array list of tokens.
     */
    public ArrayList<String> getTokens() {
        ArrayList<String> tokens = new ArrayList<>();
        String[] words = inputText.split(tokenRegEx);
        for (String word : words) {
            String token = wordToToken(word);
            if (!token.isEmpty())
                tokens.add(token);
        }
        return tokens;
    }

    /**
     * get a word and return it's token.
     *
     * @param word
     * @return the word's token.
     */
    public static String wordToToken(String word) {
        return word.toLowerCase();
    }
}
