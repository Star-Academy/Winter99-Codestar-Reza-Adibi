import org.junit.jupiter.api.Test;

import java.io.File;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class TextFileReaderTest {
    @Test
    public void readAllFilesInFolderTest() {
        HashMap<String, String> correctFiles = new HashMap<>();
        correctFiles.put("simple.txt", "this is simple file\n");
        correctFiles.put("simple2.txt", "this is second document\n");
        HashMap<String, String> testResult = (HashMap<String, String>) TextFileReader.readAllFileInFolder("data");
        assertEquals(correctFiles, testResult, "reader returns wrong result");
    }

    @Test
    public void listOfFileInFolderTest() {
        File[] correctFiles = {new File("data\\simple.txt"), new File("data\\simple2.txt")};
        File[] testResult = TextFileReader.listOfFileInFolder("data");
        assertEquals(correctFiles[0].getAbsolutePath(), testResult[0].getAbsolutePath());
        assertEquals(correctFiles[1].getAbsolutePath(), testResult[1].getAbsolutePath());
    }

    @Test
    public void readTextFileTest() {
        File correctFile = new File("data\\simple.txt");
        String correctFileText = "this is simple file\n";
        String testTextCF = TextFileReader.readTextFile(correctFile);
        assertEquals(testTextCF, correctFileText);

        File incorrectFile = new File("data\\nothing");
        String incorrectFileText = "";
        String testTextIcF = TextFileReader.readTextFile(incorrectFile);
        assertEquals(incorrectFileText, testTextIcF);
    }
}
