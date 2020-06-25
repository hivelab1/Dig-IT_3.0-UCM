using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class UndoStack : MonoBehaviour {

	public Stack deactivated;
	// Use this for initialization
	void Start () {
		deactivated = new Stack ();
	}

	public void add(GameObject go){
		deactivated.Push (go);
	}

	public GameObject pop(){
		if (deactivated.Count > 0) {
			return (GameObject)deactivated.Pop ();
		} else {
			return null;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void log(){
		foreach (Object o in deactivated) {
			MiddleVRTools.Log (o.ToString());
		}
	}

	public void reset() {
		while(deactivated.Count > 0){
			GameObject undo = (GameObject) deactivated.Pop ();
			if(undo != null) {
				undo.SetActive(true);
			}
		}


	}
}
