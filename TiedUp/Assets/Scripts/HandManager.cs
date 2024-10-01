using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandManager : MonoBehaviour {
    [Header("Visuals")]
    [SerializeField] float minSpacing;

    List<Card> _cards;

    HashSet<Card> _selectedCards;

    bool _isActive;

	private void Awake() {
        _cards = new();
		_selectedCards = new();
	}

	void Start() {
        DrawCards(4);
        AdjustCardObjects();
    }

    void Update() {
        if (!_isActive) return;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (Input.GetMouseButtonDown(0) && hit.collider != null) {
            if (hit.collider.TryGetComponent(out Card card)) {
                if (_cards.Contains(card)) {
                    _selectedCards.Add(card);
                }

                if (CardViewManager.instance.MiddleCards.Contains(card) && _selectedCards.Count > 0) {
                    MakeMove(Array.IndexOf(CardViewManager.instance.MiddleCards, card));
                }
            } 
            
            if (hit.collider.CompareTag("Pile")) {
                GameManager.instance.FinishTurn(this);
            }
        }
    }

    public void MakeMove(int middleCard) {
        bool isValid = CardManager.instance.MakeMove(CardSetToDataArr(_selectedCards), middleCard, this);

        if (isValid) {
            foreach (Card card in _selectedCards) {
                _cards.Remove(card);
                Destroy(card.gameObject);
            }
        }

        _selectedCards.Clear();
        AdjustCardObjects();
    }

    public void DrawCards(int amount = 1) {
        CardData data;

        for (int i = 0; i < amount; i++) {
            data = CardManager.instance.GenerateCard();

			_cards.Add(CardViewManager.instance.GenerateCardObject(data));
		}

		AdjustCardObjects();
	}

    public void Enable() {
        _isActive = true;
    }

    public void Disable() { 
        _isActive = false; 
    }

    void AdjustCardObjects() {
        float start = -minSpacing * _cards.Count / 2;

        for (int i = 0; i < _cards.Count; i++) {
            _cards[i].transform.position = transform.position + new Vector3(start + i * minSpacing, 0, _cards.Count - i);
        }
    }

    CardData[] CardSetToDataArr(HashSet<Card> cards) {
        return cards.Select(card => card.Data).ToArray();
    }
}
