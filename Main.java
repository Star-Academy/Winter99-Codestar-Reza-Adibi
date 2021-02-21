import java.util.Map;

public class Main {
    public static void main(String[] args) {
        TextFileReader reader = new TextFileReader();
        Map<String, String> result = reader.readAllFileInFolder("data");
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.AddDocumets(result);
        System.out.println(result);
        System.out.println(invertedIndex.invertedIndexMap);
    }
}