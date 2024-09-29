using UnityEngine;

public class CardViewManager : Singleton<CardViewManager> {
	[SerializeField] SO_CardSprites cardSprites;

    public Sprite GetSpriteFor(CardData card) {
		switch (card.Suit) {
			case Suit.Spades:
				return cardSprites.spades[card.Value - 1];
			case Suit.Diamonds:
				return cardSprites.diamonds[card.Value - 1];
			case Suit.Hearts:
				return cardSprites.hearts[card.Value - 1];
			case Suit.Clubs:
				return cardSprites.clubs[card.Value - 1];
			case Suit.Joker:
				return cardSprites.joker;
		}

		return null;
	}

	public Sprite GetBackSprite() {
		return cardSprites.back;
	}
}
