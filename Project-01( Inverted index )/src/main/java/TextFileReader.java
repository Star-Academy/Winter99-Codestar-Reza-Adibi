import java.io.*;
import java.util.HashMap;
import java.util.Map;

public class TextFileReader {

    /**
     * Reads all the files in the folder as text.
     *
     * @param folderPath
     * @return Map(file name = > file content).
     */
    public static Map<String, String> readAllFileInFolder(String folderPath) {
        Map<String, String> result = new HashMap<>();
        try {
            for (File f : listOfFileInFolder(folderPath)) {
                result.put(f.getName(), readTextFile(f));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    /**
     * get Array of files in given directory.
     *
     * @param folderPath path to directory.
     * @return Array of all files stored in the folder
     */
    protected static File[] listOfFileInFolder(String folderPath) {
        File folder = new File(folderPath);
        return folder.listFiles();
    }

    /**
     * Read the file given as parameter
     *
     * @param file input file
     * @return content of the file
     */
    public static String readTextFile(File file) throws IOException {
        FileReader fileReader = new FileReader(file);
        BufferedReader reader = new BufferedReader(fileReader);
        StringBuilder stringBuilder = new StringBuilder();
        String line;
        while ((line = reader.readLine()) != null) {
            stringBuilder.append(line);
            stringBuilder.append("\n");
        }
        stringBuilder.deleteCharAt(stringBuilder.length() - 1);
        return stringBuilder.toString();
    }
}
