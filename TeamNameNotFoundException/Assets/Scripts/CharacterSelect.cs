using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCharacter(string name)
    {
        ApplicationData.characterName = name;
    }
}
