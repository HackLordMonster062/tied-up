using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    [SerializeField] CardData[] cardsData;

    [Header("Visuals")]
    [SerializeField] float minSpacing;

    List<Card> _cards;

    void Start() {
        GenerateCards();
        AdjustCardObjects();
    }

    void Update() {
        
    }

    void GenerateCards() {
        _cards = new();

        foreach (var data in cardsData) {
            _cards.Add(CardManager.instance.GenerateCardObject(data));
        }
    }

    void AdjustCardObjects() {
        float start = -minSpacing * _cards.Count / 2;

        for (int i = 0; i < _cards.Count; i++) {
            _cards[i].transform.position = transform.position + new Vector3(start + i * minSpacing, 0, -i);
        }
    }
}
