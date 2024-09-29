using UnityEngine;

public class CardManager : MonoBehaviour {
    public static readonly int DeckSize = 54;

    [SerializeField] Card cardPrefab;

    public CardData[] MiddleCards {  get; private set; }

    void Start() {
        InitiateeGame();
    }

    public void InitiateeGame() {
        MiddleCards = new CardData[2] { GenerateCard(), GenerateCard() };
    }

    public Transform GenerateCardObject(CardData card) {
        

        Card obj = Instantiate(cardPrefab);
        obj.SetData(card, sprite);

        return obj.transform;
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
