using UnityEngine;
using System.Collections;

public class HUDTextControl : MonoBehaviour {
	bool distanceOn;
	TextMesh distanceText;
	string currDistance;
	// Use this for initialization
	void Start () {
		distanceText = (TextMesh) gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(distanceOn){
			distanceText.text = "distance: " + currDistance + " m";
		} else {
			distanceText.text = " ";
		}
	}

	public void setDistanceDisplay(string distance){
		currDistance = distance;
	}

	public void setOn(bool on){
		distanceOn = on;
	}
}
