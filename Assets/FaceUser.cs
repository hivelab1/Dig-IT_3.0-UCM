using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour {
	GameObject head;

	// Use this for initialization
	void Start () {
		head = GameObject.Find ("HeadNode");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 headPos =  head.transform.position;   
		Vector3 ourPos = this.transform.position;
		Vector3 vecToHead = ourPos-headPos;
		vecToHead.Normalize ();

		this.transform.rotation=Quaternion.LookRotation(vecToHead,head.transform.up);
	}
}
