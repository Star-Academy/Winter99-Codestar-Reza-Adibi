import java.util.ArrayList;
import java.util.HashMap;

public class Functions {

    protected static HashMap initialFiltersMap() {
        HashMap<String, ArrayList<String>> map = new HashMap<>();
        map.put("and", new ArrayList<>());
        map.put("or", new ArrayList<>());
        map.put("not", new ArrayList<>());
        return map;
    }

    protected static void insertDataToAnswerMap(HashMap<String, ArrayList<String>> answerMap, String[] filterTypes, String[] filterValues) {
        for (int i = 0; i < filterTypes.length; i++) {
            answerMap.get(filterTypes[i]).add(filterValues[i]);
        }
    }
}
