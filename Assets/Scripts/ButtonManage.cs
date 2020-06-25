using UnityEngine;
using System.Collections;

public class ButtonManage : MonoBehaviour {
	public bool measureOn;
	private GameObject MeasureOn;
	MeshRenderer rend;
	private GameObject distanceObjs;
	private DistanceDisplay distDisplay;
	Color prev;
	// Use this for initialization

	void Awake(){
		MeasureOn = GameObject.Find ("MeasureOn");
		MeasureOn.SetActive (false);
		rend = MeasureOn.GetComponentInChildren<MeshRenderer>();
		//prev = rend.material.color;

	}

	void Start () {

		distanceObjs = GameObject.Find ("DistancePoints");
		distDisplay = (DistanceDisplay) distanceObjs.GetComponent<DistanceDisplay> ();
		measureOn = false;
		setMeasureOn (measureOn);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setMeasure(Vector3 userPos, bool on) {
		distanceObjs.transform.position = userPos;
		this.setMeasureOn (on);
	}

	public void setMeasureOn(bool on){
		measureOn = on;
		if (on) {
			//rend.material.color = Color.green;
		} else {
			//rend.material.color = prev;
		}
		MeasureOn.SetActive (on);
		distanceObjs.SetActive (on);

		distDisplay.setOn (on);
	}
	
}
