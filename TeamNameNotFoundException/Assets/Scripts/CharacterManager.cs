using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {
	private string characterName = "";

	public string GetCharacterName()
	{
		Debug.Log ("Inside: " + characterName);
		return characterName;
	}

	public void CharacterSelected(int id) {
		characterName = GetCharacter (id);
		Debug.Log ("Character Name: " + GameManager.instance);
		GameManager.instance.characterName = characterName;
	}

	public string GetCharacter(int id) {
		switch (id) {
		case 0:
			return "Player";

		case 1:
			return "Juan";

		case 2:
			return "Stormi";

		case 3:
			return "Andrew";

		default:
			return null;
		}
	}
}
