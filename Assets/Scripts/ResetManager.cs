using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {
	

	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private UndoStack undoStack;
	private YearState centralYearState;


	// Use this for initialization
	void Awake () {
		undoStack = GameObject.Find ("Models").GetComponent<UndoStack> ();
		centralYearState = (YearState)GameObject.Find ("Models").GetComponent<YearState> ();
		originalPosition = transform.position;
		originalRotation = transform.rotation;
	}



	// Update is called once per frame
	void Update () {
	
	}

	public void reset(){
		transform.position = originalPosition;
		transform.rotation = originalRotation;
		undoStack.reset ();
		centralYearState.setYearAndUpdate(2011);
	}


}
