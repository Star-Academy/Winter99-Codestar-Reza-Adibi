import java.util.ArrayList;
import java.util.HashMap;

public class GeneralFunctions {
    private static final String[] filterTypes = new String[]{"and", "or", "not"};

    protected static HashMap<String, ArrayList<String>> initialFiltersMap() {
        HashMap<String, ArrayList<String>> map = new HashMap<>();
        for (String filterType : filterTypes)
            map.put(filterType, new ArrayList<>());
        return map;
    }

    protected static void insertDataToFiltersMap(HashMap<String, ArrayList<String>> answerMap, String[] filterTypes, String[] filterValues) {
        for (int i = 0; i < filterTypes.length; i++) {
            answerMap.get(filterTypes[i]).add(filterValues[i]);
        }
    }

    /**
     * get array of arrays of strings and return HashMap of HashMaps of ArrayLists.
     *
     * @param map                     ex: [["hello", "docID1", "0"], ["hell", "docID2", "0"]].
     * @param arrayOfWord_DocID_Index ex:{"hello"=>{"docID1"=>[0]}, "hell"=>{"docID2"=>[0]}}.
     */
    public static void addDataDirectlyToInvertedIndexMap(HashMap<String, HashMap<String, ArrayList<Integer>>> map, String[][] arrayOfWord_DocID_Index) {
        for (String[] word_DocID_Index : arrayOfWord_DocID_Index) {
            if (!map.containsKey(word_DocID_Index[0]))
                map.put(word_DocID_Index[0], new HashMap<>());
            if (!map.get(word_DocID_Index[0]).containsKey(word_DocID_Index[1]))
                map.get(word_DocID_Index[0]).put(word_DocID_Index[1], new ArrayList<>());
            map.get(word_DocID_Index[0]).get(word_DocID_Index[1]).add(Integer.parseInt(word_DocID_Index[2]));
        }
    }
}
