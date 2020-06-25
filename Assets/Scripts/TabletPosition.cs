/*Source: ObjectSelected
 * Modified by Cheng Ma, Emmanuel Shiferaw
 * Places the object directly in front of the user
 */
using UnityEngine;
using System.Collections;

public class TabletPosition : MonoBehaviour {
	
	GameObject wand;
	GameObject tablet;
	//GameObject Buttontablet;
	GameObject head;
	GameObject center;
	bool once = false;
	
	// Use this for initialization
	void Start () {
		wand = GameObject.Find ("HeadNode");
		head = GameObject.Find ("HeadNode");
		center = GameObject.Find ("Center");
		tablet = transform.gameObject;

		//UpdateTablet ();
	}
	
	// Update is called once per frame
	//void Update () {

	public void UnParentTablet()
	{
		tablet.transform.parent = null;

	}

	public void ParentTablet()
	{
		print ("Setting tablet position and orientation now");
		Vector3 pos =  wand.transform.position;    //get the position of the wand
		Vector3 fvec = wand.transform.forward;  //this is the direction straight ahead based on the head orientation
		Vector3 rvec = wand.transform.right;
		Vector3 uvec = wand.transform.up;

		//Debug.Log ("FVEC: " + fvec.ToString ());
		Vector3 down = uvec * 0.3f; //was 0.6

		Vector3 finalPos = pos + fvec - down; //calculate the position of the tablet utilizing position of wand and vectors of head
	
		//Vector3 newRot = -1 * wand.transform.forward;
		//Vector3 leftRot = -1 * wand.transform.right;
		tablet.transform.position = finalPos;
		tablet.transform.rotation = wand.transform.rotation;

		Vector3 newRot = -1 * wand.transform.forward;
		//Vector3 leftRot = -1 * wand.transform.right;;
		tablet.transform.rotation = Quaternion.LookRotation (newRot, transform.up);
		transform.RotateAround (transform.position, transform.right, -35);
		tablet.transform.parent = center.transform;
		//if(!once){
		//tablet.transform.RotateAround (finalPos, Vector3.down, 90);
		//tablet.transform.RotateAround (finalPos, Vector3.up, 90);
		//once = true;
		//}
		//once = !once;
		
		
	}
}
