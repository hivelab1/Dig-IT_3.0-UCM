using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class LogManager : MonoBehaviour {

	public string folderForLogFiles;
	public string folderForSummaryFiles;

	private  List<Vector3> headPositions;
	private List<Vector3> headRotations;

	private List<Vector3> wandPositions;
	private List<Vector3> wandRotations;

	private GameObject headPositionObject;
	private GameObject headRotationObject;

	private GameObject wandPositionObject;
	private GameObject wandRotationObject;

	private float distanceTraveled;
	private Vector3 lastPosition;



	// Use this for initialization
	void Start () {

		/*headPositions = new List<Vector3> ();
		headRotations = new List<Vector3> ();

		wandPositions = new List<Vector3> ();
		wandRotations = new List<Vector3> ();

		headPositionObject = gameObject;
		headRotationObject = GameObject.Find ("HeadNode");
		lastPosition = headPositionObject.transform.position;

		wandPositionObject = wandRotationObject = GameObject.Find ("HandNode");
		*/
	}
	
	// Update is called once per frame
	void Update () {

		/*headPositions.Add (headPositionObject.transform.position);
		headRotations.Add (headRotationObject.transform.rotation.eulerAngles);

		wandPositions.Add (wandPositionObject.transform.position);
		wandRotations.Add (wandRotationObject.transform.rotation.eulerAngles);

		distanceTraveled += Vector3.Distance (headPositionObject.transform.position, lastPosition);
		lastPosition = headPositionObject.transform.position;
		*/
	}

	void OnApplicationQuit(){
		/*String currDateTime = DateTime.Now.ToString ();
		String fileName = currDateTime + ".txt";

		writeDumpFiles ();
		writeSummaryFile ();
		*/
	}
	
	void writeDumpFiles(){

		/*String headPositionFilePath = "C:\\Users\\eas66\\Desktop\\DigItLogs\\HeadPositionLog.txt";
		String headRotationFilePath = "C:\\Users\\eas66\\Desktop\\HeadRotationLog.txt";

		String wandPositionFilePath = "C:\\Users\\eas66\\Desktop\\DigItLogs\\WandPositionLog.txt";
		String wandRotationFilePath = "C:\\Users\\eas66\\Desktop\\DigItLogs\\WandRotationLog.txt";

		writeFileFromList (headPositionFilePath, "FULL HEAD POSITION DUMP FILE FOR DIG@IT", headPositions);
		writeFileFromList (headRotationFilePath, "FULL HEAD ROTATION DUMP FILE FOR DIG@IT", headRotations);

		writeFileFromList (wandPositionFilePath, "FULL WAND POSITION DUMP FILE FOR DIG@IT", wandPositions);
		writeFileFromList (wandRotationFilePath, "FULL WAND ROTATION DUMP FILE FOR DIG@IT", wandRotations);
		*/

	}

	void writeSummaryFile() {
		/*String dummyName = "C:\\Users\\eas66\\Desktop\\DigItLogs\\SummaryLog.txt";
		StreamWriter writer = File.CreateText (dummyName);
		writer.WriteLine ("SUMMARY FILE FOR DIG@IT");
		
		writer.WriteLine ("Total distance traveled: " + distanceTraveled + " m" );
		writer.WriteLine ("Time since startup: " + Time.timeSinceLevelLoad + " s");
		writer.Close ();
		*/
	}

	void writeFileFromList(string path, string title, List<Vector3> list) {

		/*StreamWriter writer = File.CreateText (path);
		writer.WriteLine (title);

		foreach (Vector3 vec in list) {
			writer.WriteLine(vec.ToString());
		}
		writer.Close ();
		*/
	}


}
