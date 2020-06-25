//Source: TextVRControl
//Modified by Cheng Ma
//Manages the buttons on the Micro View menu block

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
//using UnityEditor;

public class TimelineTextControl : MonoBehaviour {
	
	//buttons
	public bool practiceMode;

	public bool isAllButton = false;
	public bool is2011Button = false;
	public bool is2012Button = false;
	public bool is2013Button = false;
	public bool is2014Button = false; 
	public bool is2015Button = false;
	public bool isExitButton = false;
	public bool isReconstructionButton = false;
	public bool isMainMenuButton = false;

	
	bool isGrabbed=false;
	private GameObject wand;
	private YearState centralYearState;
	private GameObject timeline;
	MenuAct menuAct;

	// Use this for initialization
	void Awake () {
		centralYearState = (YearState)GameObject.Find ("Models").GetComponent<YearState> ();
		menuAct = GameObject.Find ("Models").GetComponent<MenuAct> ();
		timeline = GameObject.Find ("Slider");
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

		if (isAllButton) {
			centralYearState.setYearAndUpdate (2011);
		} else if (isMainMenuButton) {
			//VRWandInteraction inter = wand.GetComponent<VRWandInteraction>();
			//inter.Menu.SetActive(true);
		} else if (is2011Button) {
			centralYearState.setYearAndUpdate (2011);
		} else if (is2012Button) {
			centralYearState.setYearAndUpdate (2012);
		} else if (is2013Button) {
			//Open menu with models
			centralYearState.setYearAndUpdate (2013);
		} else if (is2014Button) {
			centralYearState.setYearAndUpdate (2014);
		} else if (is2015Button) {
			centralYearState.setYearAndUpdate (2015);
		} else if (isReconstructionButton){
			centralYearState.setYearAndUpdate(0);
		} 

		print("should be setting timeline off here");
		menuAct.enable(); //allow menu to be turned on
		timeline.SetActive (false);

	}
	
} 	
