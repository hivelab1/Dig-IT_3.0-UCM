using UnityEngine;
using System.Collections;
using System.IO;
using MiddleVR_Unity3D;
public class FileTexts : MonoBehaviour {

	public string OBJFolder; // = "C:\\Users\\eas66\\Desktop\\import_obj"; //this is dangerous - shouldn't be specific to user -DJZ

	private ArrayList currListFiles;
	public int topInd;
	private string[] filePaths;
	private ArrayList fileArray;
	private float initY;
	private float endY;
	private GameObject scrollTick;
	private GameObject Model0; private GameObject Model1;

	void Awake() {
		Model0 = GameObject.Find ("Model 0");
		Model1 = GameObject.Find ("Model 1");
	}
	// Use this for initialization
	void Start () {
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		topInd = 0;
		scrollTick = GameObject.Find ("ScrollTick");
		initY = GameObject.Find ("Model 0").transform.position.y;
		endY = GameObject.Find ("Model 2").transform.position.y;
		fileArray = new ArrayList ();
		updateFolder ();
		updateMenu (topInd);

		stopWatch.Stop();
		System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;

		//UnityEngine.Debug.Log("FileTexts setup time: " + elapsedTime.ToString() +" ms");
	}

	void updateFolder(){
		filePaths = Directory.GetFiles(OBJFolder, "*obj*",
		                               SearchOption.AllDirectories);
		fileArray.AddRange (filePaths);
	}

	void updateMenu(int topFileIndex){
		//updateFolder ();
		int buttonInd = 0;
		for(int ind = topFileIndex; ind < fileArray.Count; ind++){
			string s = (string) fileArray[ind];
			GameObject importButton = GameObject.Find ("Model " + buttonInd.ToString());
			if(importButton != null){
				PathHolder holder = importButton.GetComponent<PathHolder>();
				holder.setFullPath(s);
				holder.setFileName(Path.GetFileName(s));
			}
			buttonInd++;
		}

		updateTick ();
	}

	void updateTick(){
		float newx;
		if(topInd == 0){
			newx = (float) Model0.transform.localPosition.x + (float)0.2;
			scrollTick.transform.localPosition = new Vector3(newx, Model0.transform.localPosition.y, Model0.transform.localPosition.z);
		} else if (topInd == 1){
			newx = (float) Model1.transform.localPosition.x + (float)0.2;
			scrollTick.transform.localPosition = new Vector3(newx, Model1.transform.localPosition.y, Model1.transform.localPosition.z);

		} else if (topInd == 2){
		} else if (topInd == 3){
		}
	}

	public void down(){
		if (hasDown ()) {
			topInd++;
			updateMenu (topInd);
		}
	}

	public bool hasDown() {
		return ((topInd + 1 < (fileArray.Count - 2)));
	}

	public void up() {
		if(hasUp ()) {
			topInd--;
			updateMenu (topInd);
		}
	}

	public bool hasUp() {
		return (topInd - 1 >= 0);
	}
}
