import java.util.*;

public class Main {
    Scanner scanner = new Scanner(System.in);

    public static void main(String[] args) {
        Main This = new Main();
        TextFileReader reader = new TextFileReader();
        Map<String, String> result = reader.readAllFileInFolder("EnglishData");
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.AddDocuments(result);
        Map<String, ArrayList<String>> filters = This.GetFiltersFromUser();
        System.out.println(Filterer.GetResult(invertedIndex, filters));
    }

    /**
     * read user input and divide them in three lists of filters( "and", "or" & "not" ).
     *
     * @return filters Map( ["and", "or", "not"] => ArrayList of words ).
     */
    private Map<String, ArrayList<String>> GetFiltersFromUser() {
        Map<String, ArrayList<String>> words = new HashMap<>();
        words.put("and", new ArrayList<>());
        words.put("or", new ArrayList<>());
        words.put("not", new ArrayList<>());
        System.out.println("write your key words:");
        String userInput = scanner.nextLine();
        String[] rawWords = userInput.toLowerCase().split("[, .]");
        for (String rawWord : rawWords) {
            switch (rawWord.charAt(0)) {
                case '+' -> words.get("or").add(rawWord.substring(1));
                case '-' -> words.get("not").add(rawWord.substring(1));
                default -> words.get("and").add(rawWord);
            }
        }
        return words;
    }
}