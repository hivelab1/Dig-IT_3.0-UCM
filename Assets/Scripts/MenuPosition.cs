/*Source: ObjectSelected
 * Modified by Cheng Ma, Emmanuel Shiferaw
 * Places the object directly in front of the user
 */
using UnityEngine;
using System.Collections;

public class MenuPosition : MonoBehaviour {

	GameObject wand;
	GameObject menu;
	GameObject Buttonmenu;
	GameObject head;
	bool set = false;

	public bool followHead;

	// Use this for initialization
	void Start () {

		//wand = GameObject.Find ("HeadNode");
		head = GameObject.Find ("HeadNode");
		menu = transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!set) {
			set = true;
			updatePosition ();
		}
		if (followHead) {
			updatePosition ();
		}
	

	}

	void OnDisable(){
		set = false;
	}

	void updatePosition() {
		Vector3 pos =  head.transform.position;    //get the position of the wand
		Vector3 fvec = head.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = head.transform.right;
		Vector3 uvec = head.transform.up;
		
		Vector3 finalPos = pos + (fvec * 2.5f); //calculate the position of the menu utilizing position of wand and vectors of head
		

		menu.transform.position = finalPos;
		menu.transform.rotation=Quaternion.LookRotation (fvec, uvec); //proper menu orienting -DJZ

		/*Vector3 newRot = -1 * wand.transform.forward;
		Vector3 leftRot = -1 * wand.transform.right;
		Vector3 leftRotLegend = -1 * wand.transform.up;
		
		if(gameObject.name != "Legend"){
			menu.transform.rotation = Quaternion.LookRotation (leftRot, Vector3.back);
		} else {
			menu.transform.rotation = Quaternion.LookRotation (leftRotLegend, newRot);
		}*/
		
	
	}
}
