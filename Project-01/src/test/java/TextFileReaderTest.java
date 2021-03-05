import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

public class TextFileReaderTest {
    @Test
    public void readAllFilesInFolderTestValidPath() {
        HashMap<String, String> correctResult = new HashMap<>();
        correctResult.put("simple.txt", "this is simple file");
        correctResult.put("simple2.txt", "this is second document");
        HashMap<String, String> testResult = null;
        testResult = (HashMap<String, String>) TextFileReader.readAllFileInFolder("data");
        assertEquals(correctResult, testResult, "reader returns wrong result");
    }

    @Test
    public void readAllFilesInFolderTestInvalidPath() {
        HashMap<String, String> correctResult = new HashMap<>();
        HashMap<String, String> testResult = (HashMap<String, String>) TextFileReader.readAllFileInFolder("wrongPath");
        assertEquals(correctResult, testResult);
    }

    @Test
    public void listOfFileInFolderTest() {
        File[] correctResult = {new File("data\\simple.txt"), new File("data\\simple2.txt")};
        File[] testResult = TextFileReader.listOfFileInFolder("data");
        assertEquals(
                Arrays.asList(correctResult[0].getAbsolutePath(), correctResult[1].getAbsolutePath()),
                Arrays.asList(testResult[0].getAbsolutePath(), testResult[1].getAbsolutePath())
        );
    }

    @Test
    public void readTextFileTestValidFilePath() {
        File correctFile = new File("data\\simple.txt");
        String correctFileText = "this is simple file";
        try {
            String testText = TextFileReader.readTextFile(correctFile);
            assertEquals(testText, correctFileText);
        } catch (IOException e) {
            e.printStackTrace();
            fail();
        }
    }

    @Test
    public void readTextFileTestInvalidFilePath() {
        File incorrectFile = new File("data\\nothing");
        assertThrows(IOException.class, () -> TextFileReader.readTextFile(incorrectFile));
    }
}
