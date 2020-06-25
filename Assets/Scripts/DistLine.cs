using UnityEngine;
using System.Collections;

public class DistLine : MonoBehaviour {

	LineRenderer lineRenderer;
	GameObject objectOne;
	GameObject objectTwo;
	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find ("Diffuse"));
		lineRenderer.SetColors (Color.yellow, Color.red);
		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.SetVertexCount (2);
		objectOne = gameObject;
		objectTwo = GameObject.Find ("Point2");
	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, new Vector3(objectOne.transform.position.x,objectOne.transform.position.y,objectOne.transform.position.z));
		lineRenderer.SetPosition(1, new Vector3(objectTwo.transform.position.x,objectTwo.transform.position.y,objectTwo.transform.position.z));
	}
}
