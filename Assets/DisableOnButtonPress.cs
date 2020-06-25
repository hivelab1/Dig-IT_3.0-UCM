using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnButtonPress : MonoBehaviour {
	GameObject menu;
	MenuAct menuAct;

	void Awake()
	{
		menu = GameObject.Find ("Menu"); //main menu
		menuAct = GameObject.Find ("Models").GetComponent<MenuAct> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MiddleVR.VRDeviceMgr != null)
		{
			if( MiddleVR.VRDeviceMgr.IsWandButtonToggled(0))
			{
				print("user toggled button 0, re-enabling main menu, and disabling ourself!");
				menuAct.enable(); //allow main menu to be toggled again
				this.gameObject.SetActive(false); //turn ourselves off
			}
		}
	}
}
