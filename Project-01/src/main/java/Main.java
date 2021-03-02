import java.util.*;

public class Main {

    public static void main(String[] args) throws Exception {
        Map<String, String> result = TextFileReader.readAllFileInFolder("EnglishData");
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addDocuments(result);
        UserInterface ui = new Console(new Scanner(System.in));
        Map<String, ArrayList<String>> filters = ui.getUserInputFilters();
        Filterer searchEngine = new Filterer(invertedIndex);
        System.out.println(searchEngine.filterDocIDs(filters));
    }

}