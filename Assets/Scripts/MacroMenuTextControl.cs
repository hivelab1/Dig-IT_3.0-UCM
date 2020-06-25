//Source: TextVRControl
//Modified by Cheng Ma
//Manages the buttons on the Micro View menu block

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
//using UnityEditor;

public class MacroMenuTextControl : MonoBehaviour {
	
	//buttons
	public bool isMainMenuButton = false;
	public bool isResetButton = false;
	public bool isMeasureButton = false;
	public bool isTimelineMenuButton = false;
	public bool isImportMenuButton = false;
	public bool isHelpButton = false;
	public bool isExitButton = false;
	public bool isLandscapeButton = false;
	
	bool isGrabbed=false;
	


	private GameObject ImportMenu;
	private GameObject TimelineMenu;
	private GameObject Slider;
	private GameObject wand;
	private GameObject Models;
	private MenuAct menuAct;
	private GameObject Menu;
	private bool measureOnToSend;
	private ButtonManage measureManage;
	private ResetManager resetManager;
	private GameObject landscape;
	private bool landscapeToggle;


	//private MenuAct menuAct;

	// Use this for initialization
	void Awake () {
		Models = GameObject.Find ("Models");
		wand = GameObject.Find("VRWand");
		menuAct = GameObject.Find ("Models").GetComponent<MenuAct> ();
		Menu = GameObject.Find ("Menu");
		resetManager = GameObject.Find ("Center").GetComponent<ResetManager> ();
		measureOnToSend = true;
		measureManage = Menu.GetComponent<ButtonManage>();
		landscape = GameObject.Find ("CatalHoyuk_DSM");
		landscapeToggle = true;
		//menuAct = GameObject.Find ("Models").GetComponent<MenuAct> ();

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
	
	void VRAction ()
	{
		if (MiddleVR.VRDeviceMgr == null) //if middle vr is not setup yet, don't do any VR related actions
			return;
		
		isGrabbed = true;


		GameObject ourHead=GameObject.Find ("HeadNode"); 

		Vector3 pos =  this.transform.position;    //get the position of the cube
		Vector3 fvec = ourHead.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = ourHead.transform.right;
		
		if (isMainMenuButton) {
			//Go to scene with Main Menu
			Debug.Log ("No Main Menu yet!");
			//Application.LoadLevel (0);
		} else if (isMeasureButton) {
			print("toggle measure here");
			//Open menu with models
			measureManage.setMeasure (gameObject.transform.position, measureOnToSend);
			measureOnToSend = !measureOnToSend;
			Menu.SetActive (false); 
			menuAct.disable();
			menuAct.enable(); //allow back on in 1 frame
		} else if (isImportMenuButton) {

			//StartUpMacro inter = Models.GetComponent<StartUpMacro> ();
			//ImportMenu = inter.importmenu;
			//ImportMenu.SetActive (true);
			//Menu.SetActive (false);
			//menuAct.setMON();
	
		} else if (isTimelineMenuButton) {
			print("show timeline here");
			menuAct.slider.SetActive (true);
			Menu.SetActive (false);
			menuAct.disable();
		} else if (isHelpButton) {
			print("show help here");
			menuAct.Legend.SetActive (true);
			Menu.SetActive (false);
			menuAct.disable();
		} else if (isExitButton) {
			Menu.SetActive (false);
		} else if (isResetButton) {
			print("do reset here");
			resetManager.reset ();
			Menu.SetActive (false); 
			menuAct.disable();
			menuAct.enable(); //allow back on in 1 frame
		} else if (isLandscapeButton) {
			print("toggle landscape here");
			landscape.SetActive (landscapeToggle);
			landscapeToggle = !landscapeToggle;
			Menu.SetActive (false); 
			menuAct.disable();
			menuAct.enable(); //allow back on in 1 frame
			//Menu.SetActive (false);
		}

	}
} 	

