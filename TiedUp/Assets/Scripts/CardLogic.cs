using System.Linq;
using UnityEngine;

public static class CardLogic {
    public static readonly int SameNumberMinSize = 2;
    public static readonly int OrderedMinSize = 3;
    public static readonly int SameSuitMinSize = 4;

    public static bool IsLegal(CardData[] cardsPlaced, CardData[] middleCards) {
        bool isValidSet = IsSameNumber(cardsPlaced, middleCards)
                       || IsSameSuit(cardsPlaced, middleCards)
                       || IsOrdered(cardsPlaced, middleCards);

        bool isValidTop = cardsPlaced[cardsPlaced.Length - 1].Suit != Suit.Joker;

        return cardsPlaced.Length > 0 && isValidSet && isValidTop;
    }

    static bool IsSameNumber(CardData[] cards, CardData[] middle) {
        int? value = null;

        foreach (CardData card in cards) {
            if (card.Suit != Suit.Joker) {
                if (value == null) {
                    value = card.Value;
                } else if (card.Value != value) {
                    return false;
                }
            }
        }

        if (value == null) {
            return true;
        }

        int middleCount = 0;

        for (int i = 0; i < middle.Length; i++) {
            if (middle[i].Value == value) {
				middleCount++;
            }
        }

        return middleCount > 0 && middleCount + cards.Length >= SameNumberMinSize;
    }

    static bool IsSameSuit(CardData[] cards, CardData[] middle) {
		Suit? suit = null;

		foreach (CardData card in cards) {
			if (card.Suit != Suit.Joker) {
				if (suit == null) {
					suit = card.Suit;
				} else if (card.Suit != suit) {
					return false;
				}
			}
		}

		if (suit == null) {
			return true;
		}

		int middleCount = 0;

		for (int i = 0; i < middle.Length; i++) {
			if (middle[i].Suit == suit) {
				middleCount++;
			}
		}

		return middleCount > 0 && middleCount + cards.Length >= SameSuitMinSize;
	}

    static bool IsOrdered(CardData[] cards, CardData[] middle) {
        var ordered = cards
            .Where(card => card.Suit != Suit.Joker)
            .OrderBy(card => card.Value).ToArray();

        int jokers = cards.Count(card => card.Suit == Suit.Joker);

        int currValue = (ordered[0].Value + 11) % 13 + 1;  // One number down and around

        int middleCount = 0;

		foreach (CardData card in middle) {
			if (card.Value == currValue) {
                middleCount++;
                break;
			}
		}

        currValue++;

		int i = 0;

        while (i < ordered.Length) {
            if (currValue == ordered[i].Value) {
                i++;
            } else {
                bool found = false;

                foreach (CardData card in middle) {
                    if (card.Value == currValue) {
                        found = true;
                        break;
                    }
                }

                middleCount++;

                if (!found && jokers-- <= 0) {
                    return false;
                }
            }

            currValue = currValue % 13 + 1;
        }

		foreach (CardData card in middle) {
			if (card.Value == currValue) {
				middleCount++;
				break;
			}
		}

        return middleCount + cards.Length >= OrderedMinSize;
    }
}
