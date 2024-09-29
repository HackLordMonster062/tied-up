using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandManager : MonoBehaviour {
    [SerializeField] CardData[] cardsData;

    [Header("Visuals")]
    [SerializeField] float minSpacing;

    List<Card> _cards;

    List<Card> _selectedCards;

	private void Awake() {
		_selectedCards = new List<Card>();
	}

	void Start() {
        GenerateCards();
        AdjustCardObjects();
    }

    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Card card;


		if (Input.GetMouseButtonDown(0) && hit.collider != null && (card = hit.collider.GetComponent<Card>()) != null) {
            if (_cards.Contains(card)) {
                _selectedCards.Add(card);
            }

            if (CardViewManager.instance.MiddleCards.Contains(card) && _selectedCards.Count > 0) {
                MakeMove(Array.IndexOf(CardViewManager.instance.MiddleCards, card));
			}
		    print(_selectedCards.Count);
        }
    }

    public void MakeMove(int middleCard) {
        CardManager.instance.MakeMove(CardListToDataArr(_selectedCards), middleCard);

        foreach (Card card in _selectedCards) {
            _cards.Remove(card);
        }

        _selectedCards.Clear();
    }

    void GenerateCards() {
        _cards = new();

        foreach (var data in cardsData) {
            _cards.Add(CardViewManager.instance.GenerateCardObject(data));
        }
    }

    void AdjustCardObjects() {
        float start = -minSpacing * _cards.Count / 2;

        for (int i = 0; i < _cards.Count; i++) {
            _cards[i].transform.position = transform.position + new Vector3(start + i * minSpacing, 0, -i);
        }
    }

    CardData[] CardListToDataArr(List<Card> cards) {
        return cards.Select(card => card.Data).ToArray();
    }
}
