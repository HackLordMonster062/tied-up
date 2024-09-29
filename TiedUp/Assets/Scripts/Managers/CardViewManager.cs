using UnityEngine;

public class CardViewManager : Singleton<CardViewManager> {
	[SerializeField] SO_CardSprites cardSprites;

	[SerializeField] Card cardPrefab;
	[SerializeField] Transform middleCardsPlace;
	[SerializeField] float middleCardSpacing;


	Card[] _middleCardObjects;
	public Card[] MiddleCards { get { return _middleCardObjects; } }

	protected override void Awake() {
		base.Awake();

		_middleCardObjects = new Card[2];

		UpdateMiddleCards();
	}

	private void Start() {
		CardManager.instance.OnSuccessfulMove += UpdateMiddleCards;
	}

	void UpdateMiddleCards() {
		var middleCards = CardManager.instance.MiddleCards;

		float start = -middleCardSpacing * middleCards.Length / 2;

		for (int i = 0; i < middleCards.Length; i++) {
			if (_middleCardObjects[i] != null) Destroy(_middleCardObjects[i].gameObject);

			_middleCardObjects[i] = GenerateCardObject(middleCards[i]);
			_middleCardObjects[i].transform.position = middleCardsPlace.position + new Vector3(start + i * middleCardSpacing, 0, 0);
		}
	}

	public Card GenerateCardObject(CardData card) {
		Card obj = Instantiate(cardPrefab);
		obj.SetData(card);

		return obj;
	}

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
