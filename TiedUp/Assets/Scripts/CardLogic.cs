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

        var orderedMiddle = middle.OrderBy(card => card.Value);

        int jokers = cards.Count(card => card.Suit == Suit.Joker);

        int currValue = CardModulu(ordered[0].Value - middle.Length);

        int middleCount = 0;

        for (int j = 0; j < middle.Length; j++) {
            foreach (CardData card in orderedMiddle) {
                if (card.Value == currValue) {
                    middleCount++;
                    break;
                }
            }

            currValue = CardModulu(currValue + 1);
        }

        currValue = ordered[0].Value;

		int i = 0;

        bool skipped = false;
        bool restarted = false;

        while (i < ordered.Length) {
            if (currValue == ordered[i].Value) {
                i++;
            } else {
                bool found = false;

                foreach (CardData card in orderedMiddle) {
                    if (card.Value == currValue) {
                        found = true;
                        break;
                    }
                }

                middleCount++;

                if (!found) {
                    if (!skipped)
                        skipped = true;
                    else if (restarted && jokers-- <= 0) {
                        return false;
                    }
                }
            }

            if (skipped && !restarted) restarted = true;

			currValue = CardModulu(currValue + 1);
        }

		for (int j = 0; j < middle.Length; j++) {
			foreach (CardData card in orderedMiddle) {
				if (card.Value == currValue) {
					middleCount++;
					break;
				}
			}

			currValue = CardModulu(currValue + 1);
		}

		return middleCount + cards.Length >= OrderedMinSize;
    }

    static int CardModulu(int value) {
        return (value + 12) % 13 + 1;
    }
}
