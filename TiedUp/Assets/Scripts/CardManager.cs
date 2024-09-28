using UnityEngine;

public class CardManager : MonoBehaviour {
    public static readonly int DeckSize = 54;

    [SerializeField] SO_CardSprites cardSprites;

    void Start() {
        
    }

    public Sprite GetCardSprite(CardData card) {
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

    public CardData GenerateCard() {
        if (Random.value < (2 / DeckSize)) {
            return new CardData()
        }
    }
}
