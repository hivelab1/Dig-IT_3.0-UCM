using UnityEngine;
using System.Collections;

public class MVRSelectShader : MonoBehaviour {

	public string regularShader;
	public string selectedShader;
	public bool isCompound;

	private MeshRenderer[] myRenders;
	private ArrayList myCompoundRenders;

	

	public void setShaders(string regular, string selected){
		this.regularShader = regular;
		this.selectedShader = selected;
	}

	void OnMVRWandEnter(){
		//print("We entered object: " + this.name);
		setShader (selectedShader);

	}

	void OnMVRWandExit() {
		setShader (regularShader);
	}

	void OnDisable(){
		setShader (regularShader);
	}

	void setShader(string shader){
		Shader sh = Shader.Find (shader);
		if(myRenders != null){
			foreach(MeshRenderer rend in myRenders){
				if(rend.material.shader != null) rend.material.shader = sh;
			}
		}
		if(isCompound) {
			foreach(MeshRenderer rend in myCompoundRenders) {
				if(rend.material.shader != null) rend.material.shader = sh;
			}
		}
	}

	// Use this for initialization
	void Awake () {

	}

	public void Initialize(string regular, string selected){
		if(gameObject.tag == "CompoundMesh"){
			isCompound = true;
		}
		myCompoundRenders = new ArrayList ();
		setShaders (regular, selected);

		myRenders = gameObject.GetComponentsInChildren<MeshRenderer> ();
		if(isCompound){
			Transform parent = gameObject.transform.parent;
			foreach(Transform sibling in parent) {
				myCompoundRenders.Add(sibling.gameObject.GetComponent<MeshRenderer>());
			}
		}
	}

	// Update is called once per frame
	//void Update () {
	//
	//}
}
