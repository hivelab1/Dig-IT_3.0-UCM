/* HUD Position Script
 * Emmanuel Shiferaw
 * 
 * Controls position of unit-sheet
 * data displayed as HUD. Attaches
 * items to head at different relative
 * locations
 *
*/
using UnityEngine;
using System.Collections;

public class HUDPosition : MonoBehaviour {
	GameObject head;
	GameObject hudText;
	GameObject Buttonmenu;
	bool once = false;
	public bool isCategory;
	public bool isName;
	public bool isYear;
	public bool isDistance;
	
	// Use this for initialization
	void Start () {
		head = GameObject.Find ("HeadNode");
		hudText = transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos =  head.transform.position;    //get the position of the wand
		Vector3 fvec = head.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = head.transform.right;
		Vector3 uvec = head.transform.up;
		
		Vector3 finalPos = pos + (fvec * 2) + (uvec) - (rvec); //calculate the position of the menu utilizing position of wand and vectors of head
		/*if (GameObject.Find ("Layer Menu") != null || GameObject.Find ("Model Menu") != null || GameObject.Find ("House Menu") != null)  {
			if (menu == GameObject.Find ("Layer Menu") || menu == GameObject.Find ("Model Menu")|| menu == GameObject.Find ("House Menu") ){
				finalPos = pos + fvec * 1 + rvec * 1;
			}
		}*/
		Vector3 catPos = pos + (fvec * 2) - (rvec) + (uvec * 0.5f);
		Vector3 yearPos = pos + (fvec * 2) - (rvec);
		Vector3 distancePos = pos + (fvec * 2) + (rvec * 0.5f) + (uvec);
		if (!once && isName) {
			hudText.transform.position = finalPos;
			hudText.transform.rotation = head.transform.rotation;
			//once = !once;
		} else if (!once && isCategory){
			hudText.transform.position = catPos;
			hudText.transform.rotation = head.transform.rotation;
		} else if (!once && isYear) {
			hudText.transform.position = yearPos;
			hudText.transform.rotation = head.transform.rotation;
		} else if (isDistance){
			hudText.transform.position = distancePos;
			hudText.transform.rotation = head.transform.rotation;

		}
		
		
	}
}
