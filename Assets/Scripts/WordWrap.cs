using UnityEngine;
using System.Collections;

public class WordWrap : MonoBehaviour {

	//private bool once = true;
	public TextMesh desc;
	private string textDisplayed="";

	// Use this for initialization
	void Start () {
		desc = this.gameObject.GetComponent<TextMesh> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(desc.text!=textDisplayed) //only do when neccisary -DJZ
		{
		   desc.text = ResolveTextSize(desc.text, 25);
	       textDisplayed=desc.text;
		}
	}

	// Wrap text by line height
	private string ResolveTextSize(string input, int lineLength){
		
		// Split string by char " "         
		string[] words = input.Split(" "[0]);
		
		// Prepare result
		string result = "";
		
		// Temp line string
		string line = "";

		int lineCount = 0;
		// for each all words        
		foreach(string s in words){
			// Append current word into line
			if (lineCount > 8) { 
				break;
			}
			string temp = line + " " + s;
			
			// If line length is bigger than lineLength

			if(temp.Length > lineLength){
				//int extraSpace=lineLength-line.Length;
				//result += line + " " + s.Substring(0,extraSpace)+"-";
				
				// Append current line into result
				result += line + "\n";
				lineCount++;
				// Remain word append into new line
				line = s; // .Substring(extraSpace);
			}
			// Append current word into current line
			else {
				line = temp;
			}
		}
		
		// Append last line into result        
		result += line;
		
		// Remove first " " char
		return result.Substring(1,result.Length-1);
	}
}
