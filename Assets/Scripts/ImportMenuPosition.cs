/*Source: ObjectSelected
 * Modified by Cheng Ma, Emmanuel Shiferaw
 * Places the object directly in front of the user
 */
using UnityEngine;
using System.Collections;

public class ImportMenuPosition : MonoBehaviour {
	
	GameObject wand;
	GameObject menu;
	GameObject Buttonmenu;
	bool once = false;
	bool set = false;
	public bool followHead;
	// Use this for initialization
	void Start () {
		wand = GameObject.Find ("HeadNode");
		menu = transform.gameObject;
	}

	void Update() {
		if (!set) {
			set = true;
			updatePosition ();
		}
		if (followHead) {
			updatePosition ();
		}
	}
	// Update is called once per frame
/*	void Update () {
		Vector3 pos =  wand.transform.position;    //get the position of the wand
		Vector3 fvec = wand.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = wand.transform.right;
		Vector3 uvec = wand.transform.up;
		
		Vector3 finalPos = pos + fvec * 3; //calculate the position of the menu utilizing position of wand and vectors of head
		/*if (GameObject.Find ("Layer Menu") != null || GameObject.Find ("Model Menu") != null || GameObject.Find ("House Menu") != null)  {
			if (menu == GameObject.Find ("Layer Menu") || menu == GameObject.Find ("Model Menu")|| menu == GameObject.Find ("House Menu") ){
				finalPos = pos + fvec * 1 + rvec * 1;
			}
		}*/
		/*
		if (!once) {
			menu.transform.position = finalPos;
			menu.transform.rotation = wand.transform.rotation;
			//once = !once;
		}
		
		
	}*/

	void updatePosition() {
		Vector3 pos =  wand.transform.position;    //get the position of the wand
		Vector3 fvec = wand.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = wand.transform.right;
		Vector3 uvec = wand.transform.up;
		
		Vector3 finalPos = pos + (fvec * 2.5f); //calculate the position of the menu utilizing position of wand and vectors of head
		
		
		menu.transform.position = finalPos;
		
		Vector3 newRot = -1 * wand.transform.forward;
		Vector3 leftRot = -1 * wand.transform.right;
		menu.transform.rotation = Quaternion.LookRotation (leftRot, newRot);

		
	}
}

