using UnityEngine;
using System.Collections;

public class AddInteraction : MonoBehaviour {
	GameObject Models;
	// Use this for initialization
	void Start () {
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		//Debug.Log ("Doing interaction");
		Models = GameObject.Find ("B89");
		GameObject curr;
		Transform curr_child;
		GameObject curr_child_child;
		foreach(Transform child in Models.transform){
			curr = child.gameObject;
		    //curr.AddComponent<VRActor>();
		
			curr_child = child.GetChild (0);
			string unitID = curr.name.TrimEnd ('a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','N','E','W','S','_','C', 'Q');
			if(curr_child.GetChild(0).childCount > 0){
				curr_child_child = curr_child.GetChild(0).gameObject;
				dealWithComplexMesh(curr_child_child, unitID);
				/*foreach(Transform child_child in curr_child.GetChild (0).transform) {

					addInteractionToMeshObj(child_child.gameObject, unitID);

				}*/
			} else {

				curr_child_child = curr_child.GetChild(0).gameObject;
				addInteractionToMeshObj(curr_child.name, curr_child_child, unitID);
			}
			MVRSelectShader mss = curr.GetComponent<MVRSelectShader> ();
			if(mss==null)
				curr.AddComponent<MVRSelectShader>();
			curr.GetComponent<MVRSelectShader>().setShaders("Standard", "Self-Illum");
		}
		Models = GameObject.Find ("Models");
		Models.transform.rotation = Quaternion.Euler (-90f, 70.4f, 0f);

		stopWatch.Stop();
		System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;

		UnityEngine.Debug.Log("Interaction setup time: " + elapsedTime.ToString() +" ms");
	}

	void addInteractionToMeshObj(string unit_name, GameObject obj, string unit) {
		obj.tag = "Feedback";
		obj.name = unit;
		//Collider check = obj.GetComponent<Collider>();
		//if(check != null){
		//	DestroyImmediate(obj.GetComponent<Collider>());
		//}
		if(obj.GetComponent<Collider>() == null) {
			obj.AddComponent<MeshCollider>();
			obj.GetComponent<MeshCollider>().convex = true; 
		}
		setupVRActor(obj,false);

		MVRSelectShader mss = obj.GetComponent<MVRSelectShader> ();
		if(mss==null)
			obj.AddComponent<MVRSelectShader>();

		if (unit_name == "U30927") {
			obj.GetComponent<MVRSelectShader> ().Initialize ("AlphaSelfIllum", "AlphaSelfIllum");
		} else {
			obj.GetComponent<MVRSelectShader> ().Initialize ("Standard", "Self-Illum");
		}
		
		obj.AddComponent<Select>();
	}

	void setupVRActor(GameObject obj, bool grab)
	{
		if(obj.GetComponent<VRActor>()==null)
		{
		   obj.AddComponent<VRActor>();
		}

		obj.GetComponent<VRActor>().Grabable = grab;
	}

	void dealWithComplexMesh(GameObject parent, string unit){
		parent.tag = "CompoundMeshParent";
		parent.name = unit;
		parent.AddComponent<Select>();
		setupVRActor(parent,false);
		foreach (Transform trans in parent.transform) {
			//Collider check = trans.gameObject.GetComponent<Collider>();
			//if(check != null){
			//	DestroyImmediate(trans.gameObject.GetComponent<Collider>());
			//}
			if(trans.gameObject.GetComponent<Collider>() == null) {
				trans.gameObject.AddComponent<MeshCollider>();
				trans.gameObject.GetComponent<MeshCollider>().convex = true;
			}

			trans.gameObject.tag = "CompoundMesh";


			setupVRActor(trans.gameObject,false); //should be grabable or not? hmmm. -DJZ

			MVRSelectShader mss = trans.gameObject.GetComponent<MVRSelectShader> (); //don't double add -DJZ
			if(mss==null)
				trans.gameObject.AddComponent<MVRSelectShader>();
			trans.gameObject.GetComponent<MVRSelectShader>().Initialize("Standard", "Self-Illum");
		
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
