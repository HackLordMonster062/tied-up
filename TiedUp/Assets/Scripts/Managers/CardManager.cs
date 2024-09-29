using UnityEngine;

public class CardManager : Singleton<CardManager> {
    public static readonly int DeckSize = 54;

    [SerializeField] Card cardPrefab;

    public CardData[] MiddleCards {  get; private set; }

    void Start() {
        InitiateGame();
    }

    public void InitiateGame() {
        MiddleCards = new CardData[2] { GenerateCard(), GenerateCard() };
    }

    public Card GenerateCardObject(CardData card) {
        Card obj = Instantiate(cardPrefab);
        obj.SetData(card);

        return obj;
    }

    public CardData GenerateCard() {
        if (Random.value < (2 / DeckSize)) {
            return new CardData(0, Suit.Joker);
        }

        Suit suit = (Suit)(Random.Range(1, 5));
        int value = Random.Range(1, 14);

        return new CardData(value, suit);
    }
}
