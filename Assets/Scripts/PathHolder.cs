using UnityEngine;
using System.Collections;

public class PathHolder : MonoBehaviour {

	private string fullPath;
	private string fileName;
	private GameObject myText;

	// Use this for initialization
	void Awake () {
		myText = GameObject.Find (gameObject.name + " Text");
	}

	public string getFullPath(){
		return fullPath;
	}

	public void setText(string text){
		if(myText.GetComponent<TextMesh>() != null){
			myText.GetComponent<TextMesh> ().text = text;
		}
	}

	public void setFullPath(string path){
		fullPath = path;
	}

	public string getFileName(){
		return fileName;
	}

	public void setFileName(string name){
		fileName = name;
		setText (name);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
