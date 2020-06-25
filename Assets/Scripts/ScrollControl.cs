using UnityEngine;
using System.Collections;

public class ScrollControl : MonoBehaviour {

	bool isGrabbed=false;
	public bool isUp;
	public bool isDown;
	public GameObject otherScroll;

	private FileTexts fileTexts;



	// Use this for initialization
	void Awake () {
		fileTexts = (FileTexts) GameObject.Find ("ImportMenu").GetComponent<FileTexts> ();
		updateColors ();

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

	void updateColors() {
		if (isUp) {
			setActive (fileTexts.hasUp ());
		}
		
		if(isDown) {
			setActive (fileTexts.hasDown());
		}

	}

	void setActive(bool isOn) {
		gameObject.GetComponent<MVRSelectColor>().setRegular((isOn) ? (Color.white):(Color.black));
		gameObject.GetComponent<MeshRenderer>().material.color = (isOn) ? (Color.white):(Color.black);
		gameObject.GetComponent<VRActor> ().enabled = isOn;
	}

	void VRAction ()
	{
		if (MiddleVR.VRDeviceMgr == null) //if middle vr is not setup yet, don't do any VR related actions
			return;
		
		isGrabbed = true;

		if (isDown) {
			fileTexts.down ();

		} else if (isUp){
			fileTexts.up ();
		}

		updateColors ();
		otherScroll.GetComponent<ScrollControl> ().updateColors ();

	}
}
