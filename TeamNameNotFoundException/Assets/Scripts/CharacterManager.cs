using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public void CharacterSelected(int id) {
		Debug.Log ("Character selected: " + CharacterName(id));
		Debug.Log ("Number selected: " + id);
	}

	string CharacterName(int id) {
		switch(id) {
		case 0:
			return "Ashley";

		case 1:
			return "Juan";

		case 2:
			return "Stormi";

		case 3:
			return "Andrew";

		default:
			return "Who is that?";
		}
	}
}
