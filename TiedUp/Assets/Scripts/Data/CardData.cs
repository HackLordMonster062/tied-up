using System;
using UnityEngine;

[Serializable]
public struct CardData {
	[SerializeField] int value;
	public int Value { get { return value; } }

	[SerializeField] Suit suit;
	public Suit Suit { get { return suit; } }

	public CardData(int value, Suit suit) {
		this.value = value;
		this.suit = suit;
	}
}

public enum Suit {
	Spades = 1,
	Hearts = 2,
	Clubs = 3,
	Diamonds = 4,
	Joker
}