using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class MenuAct : MonoBehaviour {

	private bool enabled;
	//public bool menuOn;
	private bool tabletOn;

	private GameObject menu;
	private GameObject tablet;
	public GameObject slider;
	private GameObject importMenu;
	private GameObject B89;
	//private StartUpMacro holder;

	private GameObject point1;
	private GameObject point2;
	private GameObject wandTip;
	private DistanceDisplay distDisplay;

	public TextMesh TabletUnitText;
	public TextMesh TabletYearText;
	public TextMesh TabletDescriptionText;
	private UndoStack undoStack;
	public GameObject Legend;

	private bool prePoint1;
	private bool prePoint2;

	private ButtonManage buttonManage;
	private VRWand         m_Wand      = null;
	private GameObject m_CurrentSelectedObject    = null;
	public GameObject wandMesh;

	private Shader normal;
	private Shader top;
	private GameObject landscape;


	void Awake(){
		enabled = true;
		//menuOn = false;
		tabletOn = false;
		landscape = GameObject.Find ("CatalHoyuk_DSM");
		//menuAct = GameObject.Find ("Models").GetComponent<MenuAct> ();
		buttonManage = GameObject.Find ("Menu").GetComponent<ButtonManage> ();
		//wandMesh = GameObject.Find ("RayMesh");
		//measuring points
		point1 = GameObject.Find ("Point1");
		point2 = GameObject.Find ("Point2");
		wandTip = GameObject.Find ("WandTip");
		prePoint1 = true;
		prePoint2 = false;
		distDisplay = GameObject.Find ("DistancePoints").GetComponent<DistanceDisplay> ();
		normal = Shader.Find ("Standard");
		top = Shader.Find("Custom/TopT");

		menu = GameObject.Find ("Menu"); //main menu
		tablet = GameObject.Find ("Tablet"); // tablet
		B89 = GameObject.Find ("B89"); //building w/interactions
		//holder = gameObject.GetComponent<StartUpMacro> (); // script containing more info from b89
		importMenu = GameObject.Find ("ImportMenu"); //import obj menu
		Legend = GameObject.Find ("Legend");

		//Tablet texts
		TabletUnitText = GameObject.Find ("TabletUnit").GetComponent<TextMesh> ();
		TabletYearText = GameObject.Find ("TabletYear").GetComponent<TextMesh> ();
		TabletDescriptionText = GameObject.Find ("TabletDiscussion").GetComponent<TextMesh> ();


		slider = GameObject.Find ("Slider"); //slider
		if(slider==null) print("CANT FIND SLIDER!!");
		undoStack = GameObject.Find ("Models").GetComponent<UndoStack> (); //stack of deactivated items



		m_Wand = GameObject.Find ("VRWand").GetComponent<VRWand>();

	}

	void Start(){
		//turn things on/off
		landscape.SetActive (false);
		slider.SetActive (false);
		menu.SetActive (false);
		tablet.SetActive (tabletOn);
		//menuOn = true;
		tabletOn = true;
		Legend.SetActive (false);
	}
	public void enable() {
		//enabled = true;
		StartCoroutine(enableMenuNextFrame());
	}

	IEnumerator enableMenuNextFrame()
	{
		yield return null;
		enabled=true;
	}


	public void disable() {
		enabled = false;
	}

	//public void setMON() {
	//	menuOn = true;
	//}
	void Update() {
		Reaction ();
	}
	void Reaction()
	{
		if (MiddleVR.VRDeviceMgr != null)
		{
			// Getting wand horizontal axis
			//float x = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
			// Getting wand vertical axis
			//float y = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
			
			// Getting state of wand button
			//vrButtons buttons = MiddleVR.VRDeviceMgr.GetWandButtons();
			//if (buttons == null)
			//	return;
			VRSelection selection = m_Wand.GetSelection();

			// Getting toggled state of primary wand button
			// bool t0 = MiddleVR.VRDeviceMgr.IsWandButtonToggled(0);

			if ((enabled) && (MiddleVR.VRDeviceMgr.IsWandButtonToggled(0)) ) //&& selection == null    now allow menu to pop up anytime -DJZ
			{
				print ("button 0 toggled - may toggle menu");
				// If primary button is pressed, display wand horizontal axis value
				//MVRTools.Log("WandButton 0 pressed! HAxis value: " + x + ", VAxis value: " + y );
				if(menu.activeSelf)
				{
					print("  turning menu off");
					menu.SetActive(false);
				
					//holder.importmenu.SetActive(false);
					slider.SetActive(false);
					Legend.SetActive(false);
					wandMesh.GetComponent<MeshRenderer>().material.shader = top;
					//Debug.Log ("Should be changing to"); Debug.Log (top);
				}
				else
				{
					print("  turning menu on");
					menu.SetActive(true);
					wandMesh.GetComponent<MeshRenderer>().material.shader = normal;
				}
				//menuOn = !menuOn;

				//slider.SetActive(false);
			}
			if(MiddleVR.VRDeviceMgr.IsWandButtonToggled(2) && selection !=null) //new - impliment digging (hiding objects) feature
			{               													//TODO - be careful about not deleting UI objects!!!
				if(selection.SelectedObject.tag!="Button")
				{
				   print("digging (hiding object): " + selection.SelectedObject.name);
				   selection.SelectedObject.SetActive(false); //hide it
				   undoStack.add(selection.SelectedObject);
				}
				else
					print("No digging menu item. Skipping!");
			}
			if(MiddleVR.VRDeviceMgr.IsWandButtonToggled(3)) {
				print ("button 2 toggled - undo");
				GameObject undo = undoStack.pop ();

				if(undo != null) {
					print ("found an object to turn on?");
					undo.SetActive(true);
				}
				Legend.SetActive(false);
			}
			if(MiddleVR.VRDeviceMgr.IsWandButtonToggled(1)){
				print ("button 3 toggled - should toggle tablet");
				tablet.SetActive (tabletOn);

				if(tabletOn) tablet.GetComponent<TabletPosition> ().ParentTablet ();
				else         tablet.GetComponent<TabletPosition> ().UnParentTablet ();

				tabletOn = !tabletOn;
				Legend.SetActive(false);
			}
			if(MiddleVR.VRDeviceMgr.IsWandButtonToggled(5) && buttonManage.measureOn){
				print ("button 4 toggled - measure");
				if(prePoint1){
					point1.transform.position = wandTip.transform.position;
					prePoint1 = false;
					prePoint2 = true;
				} else if(prePoint2){
					point2.transform.position = wandTip.transform.position;
					prePoint2 = false;
					prePoint1 = true;
				}
			}
		}
	}
	

}
