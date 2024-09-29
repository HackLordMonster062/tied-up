using System;
using UnityEngine;

public class CardManager : Singleton<CardManager> {
    public static readonly int DeckSize = 54;

    public event Action OnSuccessfulMove;

    public CardData[] MiddleCards { get; private set; }

    protected override void Awake() {
        base.Awake();

        InitiateGame();
    }

    public void InitiateGame() {
        MiddleCards = new CardData[2] { GenerateCard(), GenerateCard() };
    }

    public void MakeMove(CardData[] cards, int middleCardIndex) {
        bool isLegal = CardLogic.IsLegal(cards, MiddleCards);

        if (isLegal) {
            MiddleCards[middleCardIndex] = cards[cards.Length - 1];

            OnSuccessfulMove?.Invoke();
        }
    }

    public CardData GenerateCard() {
        if (UnityEngine.Random.value < (2 / DeckSize)) {
            return new CardData(0, Suit.Joker);
        }

        Suit suit = (Suit)(UnityEngine.Random.Range(1, 5));
        int value = UnityEngine.Random.Range(1, 14);

        return new CardData(value, suit);
    }
}
