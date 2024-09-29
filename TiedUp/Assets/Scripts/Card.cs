using UnityEngine;

public class Card : MonoBehaviour {
    CardData _data;

    SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        
    }

    public void SetData(CardData card) {
        _data = card;

        _spriteRenderer.sprite = CardViewManager.instance.GetSpriteFor(_data);
    }
}
