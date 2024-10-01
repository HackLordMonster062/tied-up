using System;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public static event Action<GameState> OnBeforeStateChange;
	public static event Action<GameState> OnAfterStateChange;

	[SerializeField] HandManager[] handManagers;

	int _currHandIndex = 0;
	bool _hasPlayed = false;

    public HandManager CurrHand => handManagers[_currHandIndex];
	public GameState State { get; private set; }

	void Start() {
		CardManager.instance.OnSuccessfulMove += UpdateHasPlayed;

		ChangeState(GameState.Initiating);
		ChangeState(GameState.Dealing);
		ChangeState(GameState.Playing);
    }

    void Update() {
        
    }

    public void FinishTurn(HandManager hand) {
		if (State != GameState.Playing || hand != CurrHand) return;

		ChangeState(GameState.TurnEnd);

		int cardsToDeal = 1;

		if (_hasPlayed) {
			cardsToDeal = 2;
		}

		CurrHand.DrawCards(cardsToDeal);

		ChangeState(GameState.Playing);
	}

	void ChangeState(GameState newState) {
		OnBeforeStateChange?.Invoke(newState);

		State = newState;
		switch (newState) {
			case GameState.Initiating:
				break;
			case GameState.Dealing:
				break;
			case GameState.Playing:
				CurrHand.Enable();

				_hasPlayed = false;
				break;
			case GameState.TurnEnd:
				CurrHand.Disable();

				_currHandIndex = (_currHandIndex + 1) % handManagers.Length;
				break;
			case GameState.GameEnd:
				break;
		}

		OnAfterStateChange?.Invoke(newState);
	}

	void UpdateHasPlayed(HandManager hand) {
		if (hand == CurrHand)
			_hasPlayed = true;
	}
}

[Serializable]
public enum GameState {
	Initiating = 0,
	Dealing = 1,
	Playing = 2,
	TurnEnd = 3,
	GameEnd = 4,
}