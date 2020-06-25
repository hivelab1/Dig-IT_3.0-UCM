using UnityEngine;
using System.Collections;

public class FeedBack : MonoBehaviour {

	private Renderer[] currRends;
	private Material PlainButtonOnMat;
	private Material PlainButtonOffMat;
	private bool dont;
	private bool isHighlighted;

	// Use this for initialization
	void Start () {
		currRends = gameObject.GetComponentsInChildren<Renderer>();
		PlainButtonOnMat = (Material)Resources.Load("ScrollButtonsOn", typeof(Material));
		PlainButtonOffMat = (Material)Resources.Load("ScrollButtonsOff", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDont(bool dontBool){
		dont = dontBool;
	}
	public void setHighlighted(){
		if(dont) return;
		isHighlighted = true;
		handleAll ();
	}

	public void unHighlight(){
		if(dont) return;
		isHighlighted = false;
		handleAll ();
	}

	void handleAll(){
		if (gameObject.tag == "Feedback" && currRends != null) {
			handleFeedback (isHighlighted);
		} else if (gameObject.tag == "Button" ) {
			handleButton(isHighlighted);
		} else if (gameObject.tag == "CompoundButton") {
			handleScroll (isHighlighted);
		} else if (gameObject.tag == "UIButton"){
			handleUIButton (isHighlighted);
		} else if (gameObject.tag == "PlainButton"){
			handlePlainButton(isHighlighted);
		}
	}

	void handlePlainButton(bool highlight){
		GetComponent<Renderer>().material.color = highlight ? (new Color (0.1f, 0.4f, 0.1f)) : (Color.gray);
	}
	void handleButton(bool highlight){
		//if(gameObject.GetComponentInChildren<UILabel>() != null){
		//	UILabel label = gameObject.GetComponentInChildren<UILabel>();
		//	label.color = highlight ? (new Color(0.1f, 0.4f, 0.1f)) : (Color.black);
		//}
	}

	void handleFeedback(bool highlight){
		foreach(Renderer rend in currRends){
			rend.material.shader = highlight ? Shader.Find ("Self-Illum") : Shader.Find ("Diffuse");
		}
	}

	void handleScroll(bool highlight) {
		foreach(Transform child in transform) {
			child.GetComponent<Renderer>().material = highlight ? PlainButtonOnMat: PlainButtonOffMat;
		}
	}

	void handleUIButton(bool highlight) {
		//UIButton button = gameObject.GetComponent<UIButton> ();
		//if(button != null){
		//	button.SetState(highlight ? UIButtonColor.State.Hover: UIButtonColor.State.Normal, false);
		//}
	}
}
