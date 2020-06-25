/*ConnectSingleton
 *ONLY HAVE ONE INSTANCE OF THIS SCRIPT
 *Queries from database for each object
 *that is a child of 'parent model',
 *dumps data into Dictionary data structures
 *for each relevant field. The children
 *will then pull their individual data
 *in the 'Select' script attached to each model
 */
using System; //to get Exception defined - DJZ
using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics; //to get stopwatch

public class ConnectSingleton : MonoBehaviour {
	public bool offline;

	static string connectString = 
		"Server=server.catalhoyuk.com,1434;" +
			"Database=Catalhoyuk;" +
			"User ID=catalhoyuk;" +
			"Password=catalhoyuk;";
	public SqlConnection cn;
	public Dictionary<string, string> childrenYear;
	public Dictionary<string, string> childrenCategory;
	public Dictionary<string, string> childrenArea;
	public Dictionary<string, string> childrenDesc;
	public Dictionary<string, string> childrenMound;
	public Dictionary<string, string> childrenType;

	string cat;
	string currID;
	bool first;
	GameObject currChild;
	Select currSel;

	GameObject B89;
	//Static singleton property
	//public static ConnectSingleton Instance { get; private set; }

	void Awake()
	{
		Stopwatch stopWatch = new Stopwatch();
		stopWatch.Start();

		try
		{ //lets be careful with SQL, catch any issues -DJZ
			cn= new SqlConnection(connectString);
		


		//Dictionary<GameObject, string> childrenYear = new Dictionary<GameObject, string>();
		//FeatureData ();
		first = true;
		childrenYear = new Dictionary<string, string> ();
		childrenCategory = new Dictionary<string, string> ();
		childrenArea = new Dictionary<string, string> ();
		childrenDesc = new Dictionary<string, string> ();
		childrenMound = new Dictionary<string, string> ();
		childrenType = new Dictionary<string, string> ();
		string sCommand = "Select \"Unit Number\", Year, Category, Area, Discussion from dbo.view_UnitSheetForWeb WHERE \"Unit Number\" = ";
		B89 = GameObject.Find ("B89");

		foreach(Transform child in B89.transform){
			currChild = child.transform.GetChild(0).gameObject;
			currSel = currChild.GetComponent<Select>();
			//Debug.Log (currChild.name);
			//childrenYear.Add(child.gameObject, "1922");
			if(first){
				first = false;
				sCommand = string.Concat (sCommand, currChild.name.TrimStart('U').TrimEnd ('a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','N','E','W','S','_','C', 'Q'), " ");
			}
			else if(currChild.name.StartsWith("U") && !currChild.name.EndsWith("wall")){
				sCommand = string.Concat (sCommand, "OR \"Unit Number\" = ", currChild.name.TrimStart('U').TrimEnd ('a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','N','E','W','S','_','C', 'Q'), " ");
			}
		}
		//Debug.Log (sCommand);
		sCommand = string.Concat (sCommand, ";");
		//Debug.Log (sCommand);


		SqlDataAdapter da  = new SqlDataAdapter(sCommand, cn);
		cn.Open ();
		
		DataTable dataTable = new DataTable();
		int rec = da.Fill (dataTable);
		foreach (DataRow row in dataTable.Rows) {
			//Debug.Log (string.Concat("--- Row --- : ", row.ItemArray[0]));
			currID = row.ItemArray[0].ToString();
		    foreach(DataColumn column in dataTable.Columns){
				//print("Column name" + column.ColumnName);
				//Debug.Log ("Row name" + row[column].ToString());

				switch(column.ColumnName) {
					case "Category":
						if(!childrenCategory.ContainsKey(currID)){
							childrenCategory.Add(currID, row[column].ToString ());
							childrenCategory.TryGetValue(column.ColumnName, out cat);
							//print (row[column].ToString ());
						}
						break;
					case "Year":
						if(!childrenYear.ContainsKey(currID)){
							childrenYear.Add(currID, row[column].ToString ());
							childrenYear.TryGetValue(column.ColumnName, out cat);
							//Debug.Log (cat);
						}
						break;
					case "Area":
						if(!childrenArea.ContainsKey(currID)){
							childrenArea.Add(currID, row[column].ToString ());
							childrenArea.TryGetValue(column.ColumnName, out cat);
							//Debug.Log (cat);
						}
						break;	
					case "Discussion":
						if(!childrenDesc.ContainsKey(currID)){
							childrenDesc.Add(currID, row[column].ToString ());
							childrenDesc.TryGetValue(column.ColumnName, out cat);
							//Debug.Log (cat);
						}
						break;
					default:
						break;
				}


			}
		}
		}
		catch(Exception e)
		{
			UnityEngine.Debug.Log("We got an exception!!");
			UnityEngine.Debug.LogException(e, this);
		}

		stopWatch.Stop();
		TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;
		
		UnityEngine.Debug.Log("SQL setup time: " + elapsedTime.ToString() +" ms");
	}




}
