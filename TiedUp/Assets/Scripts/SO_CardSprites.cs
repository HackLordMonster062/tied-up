using UnityEngine;

[CreateAssetMenu(fileName = "CardSprites", menuName = "data/CardSprites")]
public class SO_CardSprites : ScriptableObject {
	public Sprite back;

	public Sprite joker;

	public Sprite[] hearts = new Sprite[13];
	public Sprite[] spades = new Sprite[13];
	public Sprite[] clubs = new Sprite[13];
	public Sprite[] diamonds = new Sprite[13];
}
