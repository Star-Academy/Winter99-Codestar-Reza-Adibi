import java.io.FileReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

public class Console implements UserInterface {
    static HashMap<Character, String> filters;
    static String inputSplitRegEx = "[, .]";
    final Scanner scanner;

    static {
        filters = new HashMap<>();
        filters.put('+', "or");
        filters.put('-', "not");
        filters.put(' ', "and");
    }

    protected Console(Scanner scanner) {
        this.scanner = scanner;
    }

    /**
     * get filters from user.
     *
     * @return map(filter name = > array of words).
     */
    @Override
    public HashMap<String, ArrayList<String>> getUserInputFilters() {
        System.out.println("write your words:");
        do {
            String userInput = scanner.nextLine();
            try {
                HashMap<String, ArrayList<String>> userFilters = stringToFilter(userInput);
                scanner.close();
                return userFilters;
            } catch (Exception e) {
                System.out.println("Please input your filters!");
            }
        } while (true);
    }

    /**
     * get a text and pars it into filters.
     *
     * @param userInput
     * @return map(filter name = > array of words).
     * @throws Exception
     */
    protected HashMap<String, ArrayList<String>> stringToFilter(String userInput) throws Exception {
        if (userInput == null || userInput.isEmpty())
            throw new Exception("UserInterface.stringToFilter(): empty string received.");
        HashMap<String, ArrayList<String>> userFilters = new HashMap<>();
        for (String filterName : filters.values())
            userFilters.put(filterName, new ArrayList<>());
        String[] rawWords = userInput.split(inputSplitRegEx);
        for (String rawWord : rawWords) {
            char rawWordFirstCharacter = rawWord.charAt(0);
            if (filters.containsKey(rawWordFirstCharacter)) {
                String wordToken = Tokenizer.wordToToken(rawWord.substring(1));
                userFilters.get(filters.get(rawWordFirstCharacter)).add(wordToken);
            } else {
                String wordToken = Tokenizer.wordToToken(rawWord);
                userFilters.get(filters.get(' ')).add(wordToken);
            }
        }
        return userFilters;
    }
}
