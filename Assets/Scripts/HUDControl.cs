using UnityEngine;
using System.Collections;


public class HUDControl : MonoBehaviour {
	
	// END NETWORK STUFF

	public bool offline;


	private bool On;
	//private Server remoteHUD;

	private GameObject YearHUD;
	private GameObject CategoryHUD;
	private GameObject UnitHUD;
	private GameObject DistanceHUD;
	private ArrayList HUDObjs;

	private GameObject NormalHUDParent;
	private GameObject TabletParent;

	private GameObject TabletUnitText;
	private GameObject TabletYearText;
	private GameObject TabletDescText;
	//private GameObject DistanceDisplayText;
	private ArrayList TabletObjs;

	private ArrayList UnitObjs;
	private ArrayList YearObjs;
	private ArrayList CategoryObjs;
	private ArrayList DescObjs;
	private ArrayList DistanceObjs;

	private string currYear;
	private string currDesc;
	private string currUnit;
	private string currDistance;
	private string currCategory;

	private VRWand         m_Wand=null;
	GameObject objectInfoDisplayed=null;


	// Use this for initialization
	void Awake () {
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		//offline = true;
		On = false;
		//GameObject remoteHUDObj=GameObject.Find ("AppRoot");
		//if(remoteHUDObj!=null)
		//	remoteHUD = remoteHUDObj.GetComponent<Server> ();
		//else 
		//	remoteHUD=null;
		
		findOBJs ();
		fillCollections ();
		m_Wand = GameObject.Find ("VRWand").GetComponent<VRWand>();

		//stopWatch.Stop();
		//System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		//int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;
		//UnityEngine.Debug.Log("HUDControl setup time: " + elapsedTime.ToString() +" ms");
	}

	private void findOBJs(){
		YearHUD = GameObject.Find ("HUDYear");
		CategoryHUD = GameObject.Find ("HUDCat");
		DistanceHUD = GameObject.Find ("Distance");
		UnitHUD = GameObject.Find ("PopUpText");
		//DistanceDisplayText = GameObject.Find ("DistanceDisplay");
		TabletUnitText = GameObject.Find ("TabletUnit");
		TabletYearText = GameObject.Find ("TabletYear");
		TabletDescText = GameObject.Find ("TabletDiscussion");

		NormalHUDParent = GameObject.Find ("NormalHUD");
		TabletParent = GameObject.Find ("TabletTextParent");
	}

	private void fillCollections(){
		HUDObjs = new ArrayList ();
		HUDObjs.Add (YearHUD); HUDObjs.Add (CategoryHUD); HUDObjs.Add (DistanceHUD); HUDObjs.Add (UnitHUD);

		TabletObjs = new ArrayList ();
		TabletObjs.Add (TabletUnitText); TabletObjs.Add (TabletYearText); TabletObjs.Add (TabletDescText);

		UnitObjs = new ArrayList ();
		UnitObjs.Add (UnitHUD);
		UnitObjs.Add (TabletUnitText);

		YearObjs = new ArrayList ();
		YearObjs.Add (YearHUD);
		YearObjs.Add (TabletYearText);

		DescObjs = new ArrayList ();
		DescObjs.Add (TabletDescText);

		CategoryObjs = new ArrayList ();
		CategoryObjs.Add (CategoryHUD);

		DistanceObjs = new ArrayList ();
		DistanceObjs.Add (DistanceHUD);
		//DistanceObjs.Add (DistanceDisplayText);
	}

	public void setOn(bool what) {
		On = what;
	}

	public void setYearDisplay(string year){
		currYear = year;
		updateDisplays ();
	}

	public void setDistanceDisplay(string distance){
		currDistance = distance;
		updateDisplays ();
	}

	public void setCategoryDisplay(string category) {
		currCategory = category;
		updateDisplays ();
	}

	public void setUnitDisplay(string unit){
		currUnit = unit;
		updateDisplays ();
	}

	public void setDescriptionDisplay(string desc){
		currDesc = desc;
		updateDisplays ();
	}

	void Update() //new DJZ to get MSSQL data to tablet
	{
		VRSelection selection = m_Wand.GetSelection();
		if(selection != null)
		{
			GameObject obj=selection.SelectedObject;
			while(obj.GetComponent<Select>()==null) 
			{
				if(obj.transform.parent==null) //never found any MSSQL data
				{
					showBlankInfo();
				    return;
				}
				obj=obj.transform.parent.gameObject; //climb up through the hierarchy until we find something
			}
			showInfo(obj);
		}
		else
			showBlankInfo();
	}

	void showInfo(GameObject obj)
	{
		if(obj!=objectInfoDisplayed) //don't keep triggering an update, if we are already displaying the content
		{
			Select s=obj.GetComponent<Select>();
			setYearDisplay(s.yearExc);
			setUnitDisplay(s.unitIDnum.ToString());
			setDescriptionDisplay(s.desc);

			objectInfoDisplayed=obj;
		}
	}

	void showBlankInfo()
	{
		if(objectInfoDisplayed!=null)
		{
		   setYearDisplay("");
		   setUnitDisplay("");
		   setDescriptionDisplay("");

		   objectInfoDisplayed=null;
		}
	}

	private void updateDisplays(){
		if(offline) {
			updateYear ();
			updateCategory ();
			updateUnit ();
			updateDescription ();
			updateDistance ();
		} else {
			//HUDPack toSend = new HUDPack(currUnit, currYear, currCategory, currDesc);
			if(currYear != null) updateYear ();
			if(currCategory != null) updateCategory ();
			if(currUnit != null) updateUnit ();
			if(currDesc != null) updateDescription ();
			if(currDistance != null)updateDistance ();

			if(currUnit != null && On){
				//if(remoteHUD!=null) remoteHUD.setUnit(currUnit);
			} else { 
				//remoteHUD.setUnit (Constants.noneSelected);
			}
			if(currYear != null && On) {
				//if(remoteHUD!=null) remoteHUD.setYear(currYear);
			} else {
				//remoteHUD.setYear (Constants.blank);
			}
			if(currCategory != null && On) {
				//if(remoteHUD!=null) remoteHUD.setCategory(currCategory);
			} else { 
				//remoteHUD.setCategory (Constants.blank);
			}
			if(currDesc != null && On) {
				//if(remoteHUD!=null) remoteHUD.setDescription(currDesc);
			} else {
				//remoteHUD.setDescription (Constants.blank);
			}
		}

	}

	private void updateNetHUD(){
	}
	
	private void updateYear(){
		foreach(GameObject go in YearObjs){
			go.GetComponent<TextMesh>().text = currYear;
		}
	}
	private void updateUnit(){
		foreach(GameObject go in UnitObjs){
			go.GetComponent<TextMesh>().text = currUnit;
		}
	}

	private void updateCategory(){
		foreach(GameObject go in CategoryObjs){
			go.GetComponent<TextMesh>().text = currCategory;
		}
	}

	private void updateDescription(){
		foreach(GameObject go in DescObjs){
			go.GetComponent<TextMesh>().text = currDesc;
		}
	}

	private void updateDistance(){
		foreach(GameObject go in DistanceObjs) {
			go.GetComponent<TextMesh>().text = currDistance;
		}
	}

	public void toggleNormalHUDText(bool tog){
		NormalHUDParent.SetActive (tog);
	}

	public void toggleTabletText(bool tog){
		TabletParent.SetActive (tog);
	}

	public void toggleDistance(bool tog){
		foreach(GameObject go in DistanceObjs){
			go.SetActive(false);
		}
	}
}
