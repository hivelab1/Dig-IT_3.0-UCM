using UnityEngine;
using System.Collections;

public class MVRSelectColor : MonoBehaviour {

	public Color regularColor;
	public Color selectedColor;
	public HierarchyType hierarchyType;

	private MeshRenderer[] myRenders;
	private MeshRenderer myRender;
	 


	public enum HierarchyType
		{
			Solo,
			Parent,
			WithText
		}

	public MVRSelectColor(Color regular, Color selected){
		setShaders (regular, selected);
	}
	
	public void setShaders(Color regular, Color selected){
		this.regularColor = regular;
		this.selectedColor = selected;
	}
	
	void OnMVRWandEnter(){
		if (hierarchyType == HierarchyType.Parent) {
			setChildColors (selectedColor);
		} else if (hierarchyType == HierarchyType.Solo) {
			setColor (selectedColor);
		} else if (hierarchyType == HierarchyType.WithText) {
		}
	}

	public void setRegular(Color regular) {
		this.regularColor = regular;
	}

	void OnMVRWandExit() {
		if (hierarchyType == HierarchyType.Parent) {
			setChildColors (regularColor);
		} else if (hierarchyType == HierarchyType.Solo) {
			setColor (regularColor);
		} else if (hierarchyType == HierarchyType.WithText) {
		}
	}

	void OnDisable(){
		if (hierarchyType == HierarchyType.Parent) {
			setChildColors (regularColor);
		} else if (hierarchyType == HierarchyType.Solo) {
			setColor (regularColor);
		} else if (hierarchyType == HierarchyType.WithText) {
		}
	}

	void setChildColors(Color color){
		foreach(MeshRenderer rend in myRenders){
			rend.material.color = color;
		}
	}

	void setColor(Color color){
		myRender.material.color = color;
	}
	// Use this for initialization
	void Awake () {
		myRender = gameObject.GetComponent<MeshRenderer> ();
		myRenders = gameObject.GetComponentsInChildren<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
