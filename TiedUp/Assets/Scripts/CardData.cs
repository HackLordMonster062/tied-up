using UnityEngine;

public struct CardData {
	public int Value { get; private set; }
	public Suit Suit { get; private set; }

	public CardData(int value, Suit suit) {
		Value = value;
		Suit = suit;
	}
}

public enum Suit {
	Spades,
	Hearts,
	Clubs,
	Diamonds,
	Joker
}