import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Console implements UserInterface {
    public static HashMap<Character, String> filters;

    static {
        filters = new HashMap<>();
        filters.put('+', "or");
        filters.put('-', "not");
        filters.put(' ', "and");
    }

    /**
     * get filters from user.
     *
     * @return map(filter name = > array of words).
     */
    @Override
    public HashMap<String, ArrayList<String>> getUserInputFilters() {
        final Scanner scanner = new Scanner(System.in);
        System.out.println("write your words:");
        do {
            String userInput = scanner.nextLine();
            try {
                HashMap<String, ArrayList<String>> userFilters = stringToFilter(userInput);
                scanner.close();
                return userFilters;
            } catch (Exception e) {
                System.out.println("Please input your filters!");
            }
        } while (true);
    }

    /**
     * get a text and pars it into filters.
     *
     * @param userInput
     * @return map(filter name = > array of words).
     * @throws Exception
     */
    private HashMap<String, ArrayList<String>> stringToFilter(String userInput) throws Exception {
        if (userInput == null || userInput.isEmpty())
            throw new Exception("UserInterface.stringToFilter(): empty string received.");
        HashMap<String, ArrayList<String>> userFilters = new HashMap<>();
        for (String filterName : filters.values())
            userFilters.put(filterName, new ArrayList<>());
        String[] rawWords = userInput.toLowerCase().split("[, .]");
        for (String rawWord : rawWords) {
            if (filters.containsKey(rawWord.charAt(0)))
                userFilters.get(filters.get(rawWord.charAt(0))).add(rawWord.substring(1));
            else
                userFilters.get(filters.get(' ')).add(rawWord);
        }
        return userFilters;
    }
}
