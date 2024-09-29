using System.Linq;

public static class CardLogic {
    public static readonly int SameSuitMinSize = 4;

    public static bool IsLegal(CardData[] cardsPlaced, CardData[] middleCards) {
        bool isValidSet = IsSameNumber(cardsPlaced, middleCards)
                       || IsSameSuit(cardsPlaced, middleCards)
                       || IsOrdered(cardsPlaced, middleCards);

        bool isValidTop = cardsPlaced[cardsPlaced.Length - 1].Suit == Suit.Joker;

        return isValidSet && isValidTop;
    }

    static bool IsSameNumber(CardData[] cards, CardData[] middle) {
        int? value = null;

        for (int i = 0; i < middle.Length; i++) {
            if (middle[i].Value == cards[0].Value) {
                value = middle[i].Value;
                break;
            }
        }

        if (value == null)
            return false;

        foreach (CardData card in cards) {
            if (card.Value != value) {
                return false;
            }
        }

        return true;
    }

    static bool IsSameSuit(CardData[] cards, CardData[] middle) {
		Suit? suit = null;
        int count = 0;

		for (int i = 0; i < middle.Length; i++) {
			if (middle[i].Suit == cards[0].Suit) {
				suit = middle[i].Suit;
                count++;
			}
		}

		if (suit == null || count + cards.Length < SameSuitMinSize)
			return false;

		foreach (CardData card in cards) {
			if (card.Suit != suit) {
				return false;
			}
		}

		return true;
	}

    static bool IsOrdered(CardData[] cards, CardData[] middle) {
        var ordered = cards.OrderBy(card => card.Value).ToArray();

        int currValue = ordered[0].Value - 1;

        int middleCount = 0;

        int i = 1;

        while (i < cards.Length) {
            currValue = (currValue - 1) % 13 + 1;

            if (currValue == cards[i].Value) {
                i++;
            } else {
                bool found = false;

                foreach (CardData card in middle) {
                    if (card.Value == currValue) {
                        found = true;
                    }
                }

                if (!found) {
                    return false;
                }

                middleCount++;
            }
        }

        return true;
    }
}
