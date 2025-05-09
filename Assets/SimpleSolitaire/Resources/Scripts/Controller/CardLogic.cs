using SimpleSolitaire.Model.Config;
using SimpleSolitaire.Model.Enum;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleSolitaire.Controller
{
	public class CardLogic : MonoBehaviour
	{
		public DeckRule CurrentRule;

		public DeckRule TempRule;
		public HandOrientation Orientation;

		public List<HandOrientationElement> DeckElements;

		public Card[] CardsArray;
		public int[] CardNumberArray = new int[Public.CARD_NUMS];
		public Deck[] BottomDeckArray = new Deck[7];
		public Deck[] AceDeckArray = new Deck[4];
		public Deck[] AllDeckArray = new Deck[13];
		public Deck WasteDeck;
		public Deck PackDeck;
		public GameManager GameManagerComponent;
		public HintManager HintManagerComponent;
		public AutoCompleteManager AutoCompleteComponent;
		public UndoPerformer UndoPerformerComponent;

		public ParticleSystem ParticleStars;

		[SerializeField]
		private GameObject CardHolderPrefab;

		private readonly string _packNone = "pack_deck_none";
		private readonly string _packRotate = "pack_deck_rotate";
		private bool _isUserMadeFirstMove;

		public bool IsNeedResetPack;

		private void Awake()
		{
			CardNumberArray = new int[Public.CARD_NUMS];
		}

        /// <summary>
        /// Initialize logic structure of game session.
        /// </summary>
		public void InitCardLogic()
		{
			InitCardNodes();
			InitAllDeckArray();
			SetHandOrientation();
			UndoPerformerComponent.ResetUndoStates();
			ParticleStars.Stop();
		}

		private void OnDisable()
		{
			if (HintManagerComponent != null)
			{
				HintManagerComponent.ResetHint();
			}
		}

		/// <summary>
		/// Randomize cards.
		/// </summary>
		private void GenerateRandomCardNums()
		{
			int[] tagArray = new int[Public.CARD_NUMS];
			int i = 0;
			for (i = 0; i < Public.CARD_NUMS; i++)
			{
				tagArray[i] = 0;
			}
			for (i = 0; i < Public.CARD_NUMS; i++)
			{
				int rand = Random.Range(0, Public.CARD_NUMS);
				while (rand < Public.CARD_NUMS && tagArray[rand] == 1)
				{
					rand = Random.Range(0, Public.CARD_NUMS);
				}
				tagArray[rand] = 1;
				CardNumberArray[i] = rand;
			}
		}

        /// <summary>
		/// Randomize cards.
		/// </summary>
		public void InitSpecificCardNums(int[] numsArray)
        {
            CardNumberArray = numsArray;
        }

		public void SetHandOrientation()
		{
			DeckElements.ForEach(x =>
			{
				switch (Orientation)
				{
					case HandOrientation.LEFT:
						x.RectRoot.anchorMin = x.LeftTransformRef.anchorMin;
						x.RectRoot.anchorMax = x.LeftTransformRef.anchorMax;
						x.RectRoot.pivot = x.LeftTransformRef.pivot;
						x.RectRoot.localScale = x.LeftTransformRef.localScale;
						x.RectRoot.anchoredPosition = x.LeftTransformRef.anchoredPosition;
						x.RectRoot.sizeDelta = x.LeftTransformRef.sizeDelta;
						break;
					case HandOrientation.RIGHT:
						x.RectRoot.anchorMin = x.RightTransformRef.anchorMin;
						x.RectRoot.anchorMax = x.RightTransformRef.anchorMax;
						x.RectRoot.pivot = x.RightTransformRef.pivot;
						x.RectRoot.localScale = x.RightTransformRef.localScale;
						x.RectRoot.anchoredPosition = x.RightTransformRef.anchoredPosition;
						x.RectRoot.sizeDelta = x.RightTransformRef.sizeDelta;
						break;
				}
			});

			for (int i = 0; i < AllDeckArray.Length; i++)
			{
				Deck targetDeck = AllDeckArray[i];
				targetDeck.UpdateCardsPosition(false);
			}
		}

		/// <summary>
		/// Initilize cards in the game.
		/// </summary>
			private void InitCardNodes()
		{
			for (int i = 0; i < Public.CARD_NUMS; i++)
			{
				CardsArray[i].transform.SetParent(transform);
				CardsArray[i].InitWithNumber(i);
				CardsArray[i].CardLogicComponent = this;
			}
        }

        /// <summary>
        /// Initialize deck of cards.
        /// </summary>
        public void InitDeckCards()
		{
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < i + 1; j++)
				{
					BottomDeckArray[i].PushCard(PackDeck.Pop());
				}
				BottomDeckArray[i].UpdateCardsPosition(true);
			}
			PackDeck.UpdateCardsPosition(true);
			WasteDeck.UpdateCardsPosition(true);
		}

		/// <summary>
		/// Initialize deck array.
		/// </summary>
		private void InitAllDeckArray()
		{
			int j = 0;
			for (int i = 0; i < 4; i++)
			{
				AceDeckArray[i].Type = DeckType.DECK_TYPE_ACE;
				AllDeckArray[j++] = AceDeckArray[i];
			}
			for (int i = 0; i < 7; i++)
			{
				BottomDeckArray[i].Type = DeckType.DECK_TYPE_BOTTOM;
				AllDeckArray[j++] = BottomDeckArray[i];
			}
			WasteDeck.Type = DeckType.DECK_TYPE_WASTE;
			AllDeckArray[j++] = WasteDeck;
			PackDeck.Type = DeckType.DECK_TYPE_PACK;
			AllDeckArray[j++] = PackDeck;
		}

		/// <summary>
		/// Call when we drop card.
		/// </summary>
		/// <param name="card">Dropped card</param>
		public void OnDragEnd(Card card)
		{

			bool isPackWasteNotFound = false;
			bool isHasTarget = false;
			for (int i = 0; i < AllDeckArray.Length; i++)
			{
				Deck targetDeck = AllDeckArray[i];
				if (targetDeck.Type == DeckType.DECK_TYPE_BOTTOM || targetDeck.Type == DeckType.DECK_TYPE_ACE)
				{
					if (targetDeck.OverlapWithCard(card))
					{
						isHasTarget = true;
						Deck srcDeck = card.Deck;

						if (targetDeck.AcceptCard(card))
						{
							WriteUndoState();
							Card[] popCards = srcDeck.PopFromCard(card);
							targetDeck.PushCardArray(popCards);
							targetDeck.UpdateCardsPosition(false);
							srcDeck.UpdateCardsPosition(false);
							ActionAfterEachStep();

							if (targetDeck.Type == DeckType.DECK_TYPE_ACE)
							{
								GameManagerComponent.AddScoreValue(Public.SCORE_MOVE_TO_ACE);
								GameManagerComponent.PlayGameAudio(SoundType.AUDIO_TYPE_CYCLE);
							}
							else
							{
								GameManagerComponent.PlayGameAudio(SoundType.AUDIO_TYPE_ROTATE);
							}
							return;
						}
					}
				}
				else
				{
					isPackWasteNotFound = true;
				}
			}
			if (isPackWasteNotFound && (card.Deck.Type != DeckType.DECK_TYPE_PACK && card.Deck.Type != DeckType.DECK_TYPE_WASTE) || isHasTarget)
			{
				GameManagerComponent.PlayGameAudio(SoundType.AUDIO_TYPE_CANCEL);
			}
		}

		/// <summary>
		/// Call when we click on pack with cards.
		/// </summary>
		public void OnClickPack()
		{
			switch (CurrentRule)
			{
				case DeckRule.ONE_RULE:
					IsNeedResetPack = PackDeck.GetCardNums() == 1;

					if (PackDeck.GetCardNums() > 0)
					{
						WasteDeck.PushCard(PackDeck.Pop());
						PackDeck.UpdateCardsPosition(false);
						WasteDeck.UpdateCardsPosition(false);
					}
					else
					{
						if (WasteDeck.GetCardNums() > 0)
						{
							MoveWasteToPack();
						}
					}
					break;
				case DeckRule.THREE_RULE:
					for (int i = 0; i < 3; i++)
					{
						if (IsNeedResetPack)
						{
							MoveWasteToPack();
							IsNeedResetPack = false;
							break;
						}

						if (PackDeck.GetCardNums() > 0)
						{
							WasteDeck.PushCard(PackDeck.Pop());
							PackDeck.UpdateCardsPosition(false);
							WasteDeck.UpdateCardsPosition(false);
							IsNeedResetPack = PackDeck.GetCardNums() == 0;
							if (IsNeedResetPack) break;
						}
						else
						{
							break;
						}
					}
					break;
			}
			ActionAfterEachStep();
			GameManagerComponent.PlayGameAudio(SoundType.AUDIO_TYPE_ROTATE);
		}

		/// <summary>
		/// Hide all cards from waste to pack.
		/// </summary>
		public void MoveWasteToPack()
		{
			int cardNums = WasteDeck.GetCardNums();
			for (int i = 0; i < cardNums; i++)
			{
				PackDeck.PushCard(WasteDeck.Pop());
			}
			PackDeck.UpdateCardsPosition(false);
			WasteDeck.UpdateCardsPosition(false);
		}

		/// <summary>
		/// Check for player win or no.
		/// </summary>
		public void CheckWinGame()
		{
			bool hasWin = true;
			for (int i = 0; i < 4; i++)
			{
				if (AceDeckArray[i].GetCardNums() != 13)
				{
					hasWin = false;
					break;
				}
			}
			if (hasWin)
			{
				GameManagerComponent.HasWinGame();
			}
		}

		/// <summary>
		/// Call after each step.
		/// </summary>
		public void ActionAfterEachStep()
		{
			if (!_isUserMadeFirstMove)
			{
				_isUserMadeFirstMove = true;
			}

			SetPackDeckBg();
			GameManagerComponent.CardMove();

			if (AutoCompleteComponent.IsAutoCompleteActive)
			{
				HintManagerComponent.UpdateAvailableForAutoCompleteCards();
			}
			else
			{
				HintManagerComponent.UpdateAvailableForDragCards();
			}

			CheckWinGame();
		}

		/// <summary>
		/// Shuffle cards by state type.
		/// </summary>
		/// <param name="bReplay">Replay game or start new</param>
		public void Shuffle(bool bReplay)
		{
			HintManagerComponent.IsHintWasUsed = false;
			IsNeedResetPack = false;
			GameManagerComponent.RestoreInitialState();
			RestoreInitialState();

			if (!bReplay)
			{
				GenerateRandomCardNums();
			}

			for (int i = 0; i < Public.CARD_NUMS; i++)
			{
				Card card = CardsArray[i];
				card.InitWithNumber(CardNumberArray[i]);
				card.CardRect.gameObject.name = "Card_" + card.GetTypeName() + "_" + card.Number;
				PackDeck.PushCard(card);
			}
			InitDeckCards();
			SetPackDeckBg();
			HintManagerComponent.UpdateAvailableForDragCards();
		}

		/// <summary>
		/// Initialize default state of game.
		/// </summary>
		public void RestoreInitialState()
		{
			for (int i = 0; i < 13; i++)
			{
				AllDeckArray[i].RestoreInitialState();
			}
		}

		/// <summary>
		/// Set up background of pack.
		/// </summary>
		public void SetPackDeckBg()
		{
			string name = _packNone;
			if (PackDeck.GetCardNums() > 0 || WasteDeck.GetCardNums() > 0)
			{
				name = _packRotate;
			}
			PackDeck.SetBackgroundImg(name);
		}

		/// <summary>
		/// Write state of current decks and cards.
		/// </summary>
		public void WriteUndoState()
		{
			UndoPerformerComponent.AddUndoState(AllDeckArray, CardsArray);
			UndoPerformerComponent.ActivateUndoButton();
		}

		/// <summary>
		/// If user made any changes with cards.
		/// </summary>
		public bool IsMoveWasMadeByUser()
		{
			return _isUserMadeFirstMove;
		}
    }
}