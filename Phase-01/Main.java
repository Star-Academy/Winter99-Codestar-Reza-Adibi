import java.util.*;

public class Main {
    Scanner scanner = new Scanner(System.in);

    public static void main(String[] args) throws Exception {
        Main This = new Main();
        TextFileReader reader = new TextFileReader();
        Map<String, String> result = reader.readAllFileInFolder("EnglishData");
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.AddDocuments(result);
        Map<String, ArrayList<String>> filters = This.GetFiltersFromUser();
        System.out.println(This.GetResult(invertedIndex, filters));
    }

    /**
     * get filters and create result( set of docIDs ).
     *
     * @param invertedIndex our data base of word-docIDs.
     * @param filters       "and", "or" & "not" filters.
     * @return set of docIDs that satisfy filters.
     */
    private HashSet<String> GetResult(InvertedIndex invertedIndex, Map<String, ArrayList<String>> filters) {
        HashSet<String> andResult = null;
        HashSet<String> orResult = null;
        try {
            if (!filters.get("or").isEmpty()) {
                orResult = Filterer.OrWords(invertedIndex, new HashSet<>(), filters.get("or"));
            }
            if (!filters.get("and").isEmpty()) {
                andResult = Filterer.AndWords(invertedIndex, orResult, filters.get("and"));
            }
            if (filters.get("not").isEmpty()) {
                return filters.get("and").isEmpty() ? orResult : andResult;
            }
            if (!filters.get("or").isEmpty() && filters.get("and").isEmpty()) {
                return Filterer.NotWords(invertedIndex, orResult, filters.get("not"));
            } else if (!filters.get("and").isEmpty()) {
                return Filterer.NotWords(invertedIndex, andResult, filters.get("not"));
            } else {
                return Filterer.NotWords(invertedIndex, invertedIndex.GetAllDocIDs(), filters.get("not"));
            }
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
            return new HashSet<>();
        }
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