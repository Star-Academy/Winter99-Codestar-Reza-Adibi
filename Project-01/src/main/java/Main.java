import java.util.*;

public class Main {

    public static void main(String[] args) throws Exception {
        final Map<String, String> result = TextFileReader.readAllFileInFolder("EnglishData");
        final InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addDocuments(result);
        final UserInterface ui = new Console(new Scanner(System.in));
        final Map<String, ArrayList<String>> filters = ui.getUserInputFilters();
        final Filterer searchEngine = new Filterer(invertedIndex);
        System.out.println(searchEngine.filterDocIDs(filters));
    }

}