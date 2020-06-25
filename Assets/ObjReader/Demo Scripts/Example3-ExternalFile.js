// This example loads an external OBJ file from a path (relative to Application.dataPath/ObjReader/Sample Files),
// so it won't work in the web player.

var objFileName = "U18878_X3.txt";
var standardMaterial : Material;
var transparentMaterial : Material;

function Start () {
	var loadingText = GameObject.Find("LoadingText").GetComponent(GUIText);
	loadingText.enabled = true;
	loadingText.text = "Loading...";
	yield;
	
	objFileName = Application.dataPath + "/ObjReader/Sample Files/" + objFileName;
	
	ObjReader.use.ConvertFile (objFileName, true, standardMaterial, transparentMaterial);
	
	loadingText.enabled = false;
}