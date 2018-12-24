using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver {

	public static void InsertPatternSet (PatternSet ps) {
		var dbm = new DatabaseManager ("test.db");
        dbm.OpenConnection ();
		ArrayList data = new ArrayList();
		data.Add (ps.name);
		data.Add (Parse.PatternSetToString(ps));
        dbm.Insert ("vectors", data);
        dbm.CloseConnection ();
	}

	public static List<PatternSet> ReadPatternSets () {
		var dbm = new DatabaseManager ("test.db");
		dbm.OpenConnection ();
        var result = new List<PatternSet>();
		var a = dbm.Read ("vectors", DatabaseManager.ALL_COLLUMNS);
        foreach (var item in a) {
			var arr = item as ArrayList;
			result.Add (Parse.StringToPatternSet (arr[0] as string, arr[1] as string));
		}
        dbm.CloseConnection ();
		return result;
	}
}