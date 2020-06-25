using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class Select : MonoBehaviour {
	
	/*static string connectString = 
		"Server=server.catalhoyuk.com,1434;" +
			"Database=Catalhoyuk;" +
			"User ID=catalhoyuk;" +
			"Password=catalhoyuk;";
    */
	//static SqlConnection cn = new SqlConnection(connectString);
	//public string d = ConnectSingleton.Instance.d;
	public int unitIDnum;
	public int featureIDnum;
	public string yearExc = " ";
	public string category = " ";
	public string area = " ";
	public string desc = " ";
	public string mound = " ";
	public string type = " ";
	public GameObject dataHolder;
	public ConnectSingleton data;
	public string featureID;
	
	// Use this for initialization
	void Awake () {
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		//Trim U from object name to obtain unit ID
		//Then convert that to int to use if necessary
		FillData ();
		//Debug.Log (unitIDnum);

		stopWatch.Stop();
		System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;

		//UnityEngine.Debug.Log("Select setup time: " + elapsedTime.ToString() +" ms");
	}

	void FillData(){
		string unitID = transform.name.TrimStart('U').TrimEnd ('a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','N','E','W','S','_','C', 'Q');
		if (IsDigitsOnly (unitID)) {
			unitIDnum = int.Parse (unitID);
		} else {
			unitIDnum = 0;
		}
		/*if (transform.name.StartsWith ("F")) {
			featureID = transform.name.TrimStart ('F').TrimEnd ('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'N', 'E', 'W', 'S', 'Q', '_', 'C');
		} else {
			featureID = "0";
		}*/
		dataHolder = GameObject.Find ("B89");
		data = dataHolder.GetComponent<ConnectSingleton>();
		//Debug.Log (data);
		//Debug.Log (string.Concat ("Unit ID Select: ", unitID));
		if(data != null){
			data.childrenYear.TryGetValue (unitID, out yearExc);
			//Debug.Log (yearExc);
			data.childrenCategory.TryGetValue (unitID, out category);
			data.childrenArea.TryGetValue (unitID, out area);
			data.childrenDesc.TryGetValue (unitID, out desc);
			//data.childrenMound.TryGetValue (featureID, out mound);
			//data.childrenType.TryGetValue (featureID, out type);
		}

	}

	// Update is called once per frame
	//void Update () {
	//	
	//}
	bool IsDigitsOnly(string str)
		
	{
		foreach (char c in str)
		{
			if (c < '0' || c > '9')
				return false;
		}
		
		return true;
	}
}