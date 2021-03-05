import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

public class TextFileReaderTest {
    @Test
    public void readAllFilesInFolderTest() {
        HashMap<String, String> correctResult = new HashMap<>();
        correctResult.put("simple.txt", "this is simple file\n");
        correctResult.put("simple2.txt", "this is second document\n");
        HashMap<String, String> testResult = null;
        testResult = (HashMap<String, String>) TextFileReader.readAllFileInFolder("data");
        assertEquals(correctResult, testResult, "reader returns wrong result");

        correctResult = new HashMap<>();
        testResult = (HashMap<String, String>) TextFileReader.readAllFileInFolder("wrongPath");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void listOfFileInFolderTest() {
        File[] correctResult = {new File("data\\simple.txt"), new File("data\\simple2.txt")};
        File[] testResult = TextFileReader.listOfFileInFolder("data");
        assertEquals(correctResult[0].getAbsolutePath(), testResult[0].getAbsolutePath());
        assertEquals(correctResult[1].getAbsolutePath(), testResult[1].getAbsolutePath());
    }

    @Test
    public void readTextFileTest() {
        File correctFile = new File("data\\simple.txt");
        String correctFileText = "this is simple file\n";
        String testTextCF = null;
        try {
            testTextCF = TextFileReader.readTextFile(correctFile);
        } catch (IOException e) {
            e.printStackTrace();
            fail();
        }
        assertEquals(testTextCF, correctFileText);

        File incorrectFile = new File("data\\nothing");
        assertThrows(IOException.class, () -> TextFileReader.readTextFile(incorrectFile));
    }
}
