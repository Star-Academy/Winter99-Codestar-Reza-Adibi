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
        for (File f : listOfFileInFolder(folderPath)) {
            result.put(f.getName(), readTextFile(f));
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
        // todo: filter files
        return folder.listFiles();
    }

    /**
     * Read the file given as parameter
     *
     * @param file input file
     * @return content of the file
     */
    public static String readTextFile(File file) {
        FileReader fileReader = null;
        BufferedReader reader = null;
        try {
            fileReader = new FileReader(file);
            reader = new BufferedReader(fileReader);
            String line;
            StringBuilder sb = new StringBuilder();
            while ((line = reader.readLine()) != null) {
                sb.append(line);
                sb.append("\n");
            }
            return sb.toString();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (reader != null) {
                try {
                    reader.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        return "";
    }
}
