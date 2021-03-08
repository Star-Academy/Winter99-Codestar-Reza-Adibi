import java.util.*;

public class Main {
    final static String dataSourcePath = "EnglishData";

    public static void main(String[] args) throws Exception {
        final InvertedIndex invertedIndex = initialDataSource();
        final UserInterface ui = new Console(new Scanner(System.in));
        final Map<String, ArrayList<String>> filters = ui.getUserInputFilters();
        final Filterer searchEngine = new Filterer(invertedIndex);
        System.out.println(searchEngine.filterDocIDs(filters));
    }

    private static InvertedIndex initialDataSource() {
        final Map<String, String> result = TextFileReader.readAllFileInFolder(dataSourcePath);
        final InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addDocuments(result);
        return invertedIndex;
    }

}