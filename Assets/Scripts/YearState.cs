using UnityEngine;
using System.Collections;
using System.Linq;
public class YearState : MonoBehaviour {

	public bool practiceMode;

	private int currYear;
	
	//parent of gameObjects that have db info
	private GameObject dbObjectsParent;

	//parent of all models
	private GameObject ModelParent;

	//list of gameObjects that have db info
	private ArrayList unitsList;

	//list of bigger models
	private ArrayList extraBuildingsList;
	private ArrayList extraB89List;

	//reconstruction models
	private ArrayList reconstructionList;

	private GameObject displayLabel; //for timeline menu


	//button for current year
	//private UIButton currButton;

	private Hashtable buttonsTable;
	private Hashtable timeButtonsHash;

	// Use this for initialization
	void Awake () {
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		currYear = 2011;
		dbObjectsParent = GameObject.Find ("B89");
		ModelParent = GameObject.Find ("Models");
		displayLabel=GameObject.Find ("DisplayingNowText");
		
		timeButtonsHash = new Hashtable ();
		timeButtonsHash.Add("0Button",   GameObject.Find("0ButtonText"));
		timeButtonsHash.Add("2011Button",GameObject.Find("2011ButtonText"));
		timeButtonsHash.Add("2012Button",GameObject.Find("2012ButtonText"));
		timeButtonsHash.Add("2013Button",GameObject.Find("2013ButtonText"));
		timeButtonsHash.Add("2014Button",GameObject.Find("2014ButtonText"));
		timeButtonsHash.Add("2015Button",GameObject.Find("2015ButtonText"));
		
		unitsList = new ArrayList ();
		extraBuildingsList = new ArrayList ();
		extraB89List = new ArrayList ();
		reconstructionList = new ArrayList ();
		buttonsTable = new Hashtable ();
		if (practiceMode) {
			prepareCubesList();
		} else {
			prepareUnitsList ();
		}
		if(!practiceMode){
			prepareExtraList ();
			prepareReconstructionsList();
		}
		buildButtonsTable ();
		stopWatch.Stop();
		System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;

		UnityEngine.Debug.Log("YearState setup time: " + elapsedTime.ToString() +" ms");
	}


	void Start()
	{
		setYearAndUpdate(2011);   
	}

	void buildButtonsTable(){
		foreach(int yr in Enumerable.Range (2011, 5).ToArray()){
			GameObject currButt = GameObject.Find (yr.ToString () + "Button");
			if(currButt != null){
				buttonsTable.Add(yr, currButt);
			}
		}
	}

	void Update() {
		//if(currButton != null){
		//	currButton.SetState (UIButtonColor.State.Pressed, true);
		//	currButton.GetComponent<FeedBack> ().setDont (true);
		//}
	}
	void prepareUnitsList(){
		//build b89 units list
		foreach(Transform child in dbObjectsParent.transform) {
			Transform trueChild = child.GetChild(0);
			unitsList.Add(trueChild.gameObject);
		}
	}

	void prepareCubesList(){
		foreach(Transform child in dbObjectsParent.transform) {
			//if(child.gameObject.GetComponentInChildren<PracticeData>() != null){
			//	unitsList.Add(child.gameObject);
			//}
		}
	}

	void prepareExtraList() {
		foreach(Transform child in ModelParent.transform) {
			Transform trueChild = child.GetChild (0);
			if(trueChild.gameObject.GetComponent<ExtraBuilding>() != null){
				extraBuildingsList.Add(trueChild.gameObject);
			}
		}

		GameObject B89wall = GameObject.Find ("B89_2011_PreEx_ExtWall_01");
		GameObject B89ref = GameObject.Find ("B89_56_L_Reference");

		extraB89List.Add (B89wall);
		extraB89List.Add (B89ref);
	}

	void prepareReconstructionsList(){
		GameObject reconst = GameObject.Find ("Final_B89_Matthiesen_text_embedded");
		if(reconst!=null) //bug - we can't use find to get inactive objects
		{
		   reconstructionList.Add (reconst);
		   reconst.SetActive (false);
		}
	}

	void updateRender(){
		//Debug.Log ("button on!");
		//Debug.Log ("Setting year to: " + currYear.ToString ());

		ConnectSingleton data = ModelParent.GetComponent<ConnectSingleton>();
		
		
		foreach (GameObject go in extraBuildingsList) {
			ExtraBuilding yearInfo = go.GetComponent<ExtraBuilding>();
			
			if(int.Parse(yearInfo.year) >= currYear || ((currYear != 0) && (currYear < 2013))) {
				changeRender (go, true);
			} else {
				changeRender (go, false);
			}
		}
		
		foreach (GameObject go in unitsList) {
			if(currYear < 2000) {
				go.SetActive(false);
				continue;
			} else {
				go.SetActive(true);
			}
			Select layerData = go.GetComponentInChildren<Select>();
			if(layerData != null) {
				if(layerData.yearExc != null) {
					if(int.Parse(layerData.yearExc) >= currYear) {
						changeRender (go, true);
					} else {
						changeRender (go, false);
					}
				}
			}
		}

		foreach (GameObject go in reconstructionList) {
			if(currYear == 0) {
				go.SetActive(true);
			} else {
				go.SetActive(false);
			}
		}

		foreach(GameObject go in extraB89List) {
			if(currYear == 0) {
				go.SetActive(false);
			} else {
				go.SetActive(true);
			}
		}
		
	}

	void updateRenderCubes(){
		foreach (GameObject go in unitsList) {
			//PracticeData layerData = go.GetComponentInChildren<PracticeData>();
			//if(layerData != null) {
			//	if(layerData.year != null) {
			//		if(int.Parse(layerData.year) >= currYear) {
			//			changeRender (go, true);
			//		} else {
			//			changeRender (go, false);
			//		}
			//	}
			//}
		}
	}

	void changeRender(GameObject model, bool flip){
		MeshRenderer[] rends = (MeshRenderer[]) model.GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer rend in rends){
			rend.enabled = flip;
		}
	}

	void updateDisplay(){ //we don't really need to update timeline button with current menu setup -DJZ
		/*if(currButton != null) {
			currButton.SetState (UIButtonColor.State.Normal, false);
			((FeedBack)currButton.gameObject.GetComponent<FeedBack>()).setDont (false);
		}
		currButton = GameObject.Find (currYear.ToString () + "Button").GetComponent<UIButton> ();
		if(currButton != null){
			((FeedBack)currButton.gameObject.GetComponent<FeedBack>()).setDont (true);
			currButton.SetState (UIButtonColor.State.Pressed, false);

		}
		*/
	}

	//public void setYearAndUpdate(int yearToSet, bool timelineActive) {
//		print("in YearState setting to year: " + yearToSet);
//		if (timelineActive) {
//			setYearAndUpdate(yearToSet);
///		} else {
//			currYear = yearToSet;
//			if(!practiceMode) {
//				updateRender ();
//			} else {
//				updateRenderCubes ();
//			}
//		}
//	}

	public void setYearAndUpdate(int yearToSet)
	{
		UndoStack undoStack = GameObject.Find ("Models").GetComponent<UndoStack> ();
		undoStack.reset();

		print("in YearState setting to year: " + yearToSet);
		currYear = yearToSet;
		//if(!practiceMode) {
			updateRender ();
		//} else {
		//	updateRenderCubes ();
		//}
		updateDisplay ();


		GameObject dateButton=(GameObject)timeButtonsHash[currYear+"Button"];
		displayLabel.transform.parent=null;

		displayLabel.transform.position=dateButton.transform.position; //goal is to "Displaying Now" label next to correct date button -DJZ
		displayLabel.transform.rotation=dateButton.transform.rotation;
		displayLabel.transform.parent=dateButton.transform;
		displayLabel.transform.localPosition=new Vector3(30f,0f,0f);

	}

}
