using UnityEngine;
using System.Collections;

public class importObj : MonoBehaviour {

	public string objFileName;
	public Material standardMaterial;
	public Material transparentMaterial;

	IEnumerator Start () {
		//var loadingText = GameObject.Find("LoadingText").GetComponent<GUIText>();
		//loadingText.enabled = true;
		//loadingText.text = "Loading...";
		yield return null;

		//loadingText.enabled = false;

	}



	public void import(string path){
		objFileName = path;
		ObjReader.use.ConvertFile (objFileName, true, standardMaterial, transparentMaterial);
	}
}