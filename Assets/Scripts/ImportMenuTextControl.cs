//Source: TextVRControl
//Modified by Cheng Ma
//Manages the buttons on the Micro View menu block

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System.Linq;
using System.Collections.Generic;

//using UnityEditor;

public class ImportMenuTextControl : MonoBehaviour {
	
	//buttons
	public bool isMainMenuButton = false;
	public bool isModel0Button = false;
	public bool isModel1Button = false;
	public bool isModel2Button = false;
	public bool isModel3Button = false;
	public bool isExitButton = false;

	
	bool isGrabbed=false;
	private GameObject wand;
	private GameObject ModelMenu;
	private GameObject ImportMenu;
	private GameObject B89;
	private GameObject ObjectManager;
	private GameObject wandCube;

	// Use this for initialization
	void Awake () {
		wand = GameObject.Find("VRWand");
		wandCube = GameObject.Find ("WandCube");
		ModelMenu = GameObject.Find ("Menu");
		B89 = GameObject.Find ("B89");
		ObjectManager = GameObject.Find ("ObjectManager");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (MiddleVR.VRDeviceMgr == null) //if middle vr is not setup yet, don't do any VR related actions
			return;
		
		if (isGrabbed && MiddleVR.VRDeviceMgr.IsWandButtonPressed (0)==false) //watch for a button release, if we've previously grabbed object
		{
			isGrabbed=false;
			
		}
	}

	void importFile(){
		ObjectManager.AddComponent<importObj>();
		//ExtFile.objFileName = "Car_obj.txt";
		importObj reader = ObjectManager.GetComponent<importObj>();
		PathHolder currHolder = gameObject.GetComponent<PathHolder> ();
		//UILabel currText = gameObject.GetComponentInChildren<UILabel>();
		reader.import (currHolder.getFullPath());
	}

	List<GameObject> currObjs(){
		List<GameObject> returnList = new List<GameObject>();
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		foreach (GameObject go in allObjects) {
			returnList.Add(go);
		}
		return returnList;
	}

	void placeNewObject(List<GameObject> oldList){
		List<GameObject> newList = currObjs ();

		List<GameObject> diff = 
			oldList.Where (a => !newList.Any (a.Equals)).Union (
				newList.Where (b => !oldList.Any (b.Equals))).ToList ();

		foreach(GameObject dif in diff){
			Vector3 offset = new Vector3(0.1f,0.0f,0.1f);
			dif.transform.position = wandCube.transform.position + offset;
			dif.AddComponent<VRActor>();
			VRActor act = dif.GetComponent<VRActor>();
			act.Grabable = true;
			Debug.Log (dif);
		}

	}

	void addObject(){
		List<GameObject> old = currObjs ();
		importFile ();
		placeNewObject(old);
	}
 
	void VRAction ()
	{
		if (MiddleVR.VRDeviceMgr == null) //if middle vr is not setup yet, don't do any VR related actions
			return;
		
		isGrabbed = true;
		
		//print (transform.name + " was selected!!!");
		//audio.Play (); //play the click sound
		
		GameObject ourHead=GameObject.Find ("HeadNode"); //get the other objects in the scene that we need to utilize
		
		Vector3 pos =  this.transform.position;    //get the position of the cube
		Vector3 fvec = ourHead.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = ourHead.transform.right;
	
		importObj importScript = ObjectManager.GetComponent<importObj>();

		if (isMainMenuButton) {
			ModelMenu.SetActive(true);
			transform.parent.parent.gameObject.SetActive(false);
			//Application.LoadLevel (0);
		} else if (isModel0Button) {

			addObject();
	
		} else if (isModel1Button) {
			addObject ();
			//Revert all changes by reloading the level

		} else if (isModel2Button) {
			addObject();
			//Open menu with models

		} else if (isModel3Button) {

			//Open menu with deactived layers
			//DeactivatedLayersMenu.SetActive (true);
		} else if (isExitButton){
			transform.parent.gameObject.SetActive(false);;
		}
	}
} 	

