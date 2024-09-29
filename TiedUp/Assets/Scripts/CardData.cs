using System;

[Serializable]
public struct CardData {
	public int Value { get; private set; }
	public Suit Suit { get; private set; }

	public CardData(int value, Suit suit) {
		Value = value;
		Suit = suit;
	}
}

public enum Suit {
	Spades = 1,
	Hearts = 2,
	Clubs = 3,
	Diamonds = 4,
	Joker
}