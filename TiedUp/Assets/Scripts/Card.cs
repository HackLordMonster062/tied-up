using UnityEngine;

public class Card : MonoBehaviour {
    CardData _data;

    SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetData(CardData card, bool isFlipped = false) {
        _data = card;

        _spriteRenderer.sprite = isFlipped ? CardViewManager.instance.GetBackSprite() : CardViewManager.instance.GetSpriteFor(_data);
    }
}
