using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {

	public string    suit;
	public int       rank;
	public Color     color = Color.black;
	public string    colS = "Black";  // or "Red"
	
	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();
	
	public GameObject back;    
	public CardDefinition def; 	
	public SpriteRenderer[]		spriteRenderers;

	void Start() {
		SetSortOrder (0);	//ensures that the card starts properly depth sorted
	}
	
	// property
	public bool faceUP {
		get {
			return (!back.activeSelf);
		}		
		set {
			back.SetActive(!value);
		}
	}

	//If spriteRenderers is not yet defined, this function defines it
	public void PopulateSpriteRenderers() {
		//if spriterenderers is null or empty
		if (spriteRenderers == null || spriteRenderers.Length == 0) {
			//get spriterenderer components of this gameobject and its children
			spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
		}
	}

	//Sets the sortingLayerName on all SpriteRenderer Componenets
	public void SetSortingLayerName(string tSLN) {
		PopulateSpriteRenderers ();

		foreach (SpriteRenderer tSR in spriteRenderers) {
			tSR.sortingLayerName = tSLN;
		}
	}

	//Sets the sortingorder of all spriterenderer components
	public void SetSortOrder(int sOrd) {
		PopulateSpriteRenderers ();

		//the white background of the card is on bottom (sOrd)
		//on top of that are all the pips, decorators, face, etc. (sord+ 1)
		//The back is on top so that when visible, it covers the rest (sord + 2)

		//Iterate through all the spriterenderers as tSR
		foreach (SpriteRenderer tSR in spriteRenderers) {
			if (tSR.gameObject == this.gameObject) {
				//if the gameobject is this.gameobject, its the background
				tSR.sortingOrder = sOrd;	//set its order to sord
				continue; //and continue to the next iteration of the loop
			}
			//Each of the children of this gameobject are named
			//switch based on the names
			switch (tSR.gameObject.name) {
			case "back":
				tSR.sortingOrder = sOrd + 2;
				//Set it to the highest layer to cover everything else
				break;
			case "face":
			default:
				tSR.sortingOrder = sOrd + 1;
				//Set it to the middle layer to be above the background
				break;
			}
		}
	}

	//Virtual methods can be overridden bu subclass methods with the same name
	virtual public void OnMouseUpAsButton() {
		//print (name);	//when clicked, this outputs the card name
	}

} // class Card

[System.Serializable]
public class Decorator{
	public string	type;			// For card pips, tyhpe = "pip"
	public Vector3	loc;			// location of sprite on the card
	public bool		flip = false;	//whether to flip vertically
	public float 	scale = 1.0f;
}

[System.Serializable]
public class CardDefinition{
	public string	face;	//sprite to use for face cart
	public int		rank;	// value from 1-13 (Ace-King)
	public List<Decorator>	
					pips = new List<Decorator>();  // Pips Used					
} // Class CardDefinition
