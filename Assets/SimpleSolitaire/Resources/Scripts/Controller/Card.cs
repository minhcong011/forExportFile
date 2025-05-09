using SimpleSolitaire.Model.Enum;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleSolitaire.Controller
{
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public int CardType = 0;
        public int CardNumber = 0;
        public int Number = 0;
        public int CardStatus = 0;
        public int CardColor = 0;
        public bool IsDraggable = false;

        public int IndexZ;

        public CardLogic CardLogicComponent;
        public Image BackgroundImage;

        private Vector3 _lastMousePosition = Vector3.zero;
        private Vector3 _offset;
        private IEnumerator _coroutine;
        private IEnumerator _doubleClickTimer;

        public RectTransform CardRect;
        private Vector3 _newPosition;
        private int _tapCount;
        private float _newTime;
        private float _maxDubbleTapTime = 0.5f;
        private bool IsDoubleCLickActionStarted = false;

        private Deck _deck;
        public Deck Deck
        {
            get
            {
                return _deck;
            }
            set
            {
                _deck = value;
            }
        }

        private readonly string _spadeTextureName = "spade";
        private readonly string _diamondTextureName = "diamond";
        private readonly string _clubTextureName = "club";
        private readonly string _heartTextureName = "heart";

        private void Start()
        {
            IndexZ = transform.GetSiblingIndex();
        }

        private void Update()
        {
            OnDoubleClickCheck();
        }

        /// <summary>
        /// Set new background image for card.
        /// </summary>
        /// <param name="str"></param>
        public void SetBackgroundImg(string str)
        {
            Sprite tempType = Resources.Load("Sprites/cards/" + str, typeof(Sprite)) as Sprite;
            BackgroundImage.overrideSprite = tempType;
        }

        /// <summary>
        /// Show star particles.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ActivateParticle()
        {
            yield return new WaitForSeconds(0.1f);
            CardLogicComponent.ParticleStars.Play();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CardLogicComponent.AutoCompleteComponent.IsAutoCompleteActive || !IsDraggable)
            {
                return;
            }

            IndexZ = transform.GetSiblingIndex();
            _deck.SetCardsToTop(this);
            CardLogicComponent.ParticleStars.transform.SetParent(gameObject.transform);
            CardLogicComponent.ParticleStars.transform.SetAsFirstSibling();

            _coroutine = ActivateParticle();
            StartCoroutine(_coroutine);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CardLogicComponent.AutoCompleteComponent.IsAutoCompleteActive || !IsDraggable)
            {
                return;
            }

            RectTransformUtility.ScreenPointToWorldPointInRectangle(CardRect, Input.mousePosition, eventData.enterEventCamera, out _newPosition);
            if (_lastMousePosition != Vector3.zero)
            {
                Vector3 offset = _newPosition - _lastMousePosition;
                transform.position += offset;
                CardLogicComponent.ParticleStars.transform.position = new Vector3(transform.position.x, transform.position.y - 20f, transform.position.z);
                _deck.SetPositionFromCard(this, transform.position.x, transform.position.y);
            }
            _lastMousePosition = _newPosition;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            if (CardLogicComponent.AutoCompleteComponent.IsAutoCompleteActive || !IsDraggable)
            {
                return;
            }

            transform.SetSiblingIndex(IndexZ);
            _lastMousePosition = Vector3.zero;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            CardLogicComponent.ParticleStars.Stop();

            CardLogicComponent.OnDragEnd(this);
            _deck.UpdateCardsPosition(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Deck.Type == DeckType.DECK_TYPE_PACK)
            {
                if (CardLogicComponent.AutoCompleteComponent.IsAutoCompleteActive)
                {
                    return;
                }
                Deck.OnPointerClick(eventData);
            }

            _tapCount++;
        }

        /// <summary>
        /// Get card texture by type.
        /// </summary>
        /// <param name="backTexture"></param>
        /// <returns> Texture string type</returns>
        public string GetTexture(string backTexture)
        {
            string texture = backTexture;
            if (CardStatus != 0)
            {
                texture = GetTypeName() + Number;
            }
            return texture;
        }

        /// <summary>
        /// Set default card status and background image <see cref="SetBackgroundImg"/>.
        /// </summary>
        public void RestoreBackView()
        {
            CardStatus = 0;
            SetBackgroundImg(CardShirtManager.Instance.ShirtName);
        }

        /// <summary>
        /// Set card position.
        /// </summary>
        /// <param name="position">New card position.</param>
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        /// <summary>
        /// Initialize card by number.
        /// </summary>
        /// <param name="cardNum">Card number.</param>
        public void InitWithNumber(int cardNum)
        {
            CardNumber = cardNum;
            CardType = Mathf.FloorToInt(cardNum / 13);
            if (CardType == 1 || CardType == 3)
            {
                CardColor = 1;
            }
            else
            {
                CardColor = 0;
            }
            Number = (cardNum % 13) + 1;
            CardStatus = 0;

            SetBackgroundImg(GetTexture(CardShirtManager.Instance.ShirtName));
        }

        /// <summary>
        /// Update card background <see cref="SetBackgroundImg"/>.
        /// </summary>
        public void UpdateCardImg()
        {
            SetBackgroundImg(GetTexture(CardShirtManager.Instance.ShirtName));
        }

        public string GetTypeName()
        {
            switch (CardType)
            {
                case 0:
                    return _spadeTextureName;
                case 1:
                    return _heartTextureName;
                case 2:
                    return _clubTextureName;
                case 3:
                    return _diamondTextureName;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        ///Called when user click on card double times in specific interval
        /// </summary>
        public void OnDoubleClickCheck()
        {
            if (_tapCount == 1 && !IsDoubleCLickActionStarted)
            {
                IsDoubleCLickActionStarted = true;
                _newTime = Time.time + _maxDubbleTapTime;
            }
            else if (_tapCount == 2 && Time.time <= _newTime)
            {
                _tapCount = 0;
                IsDoubleCLickActionStarted = false;
                CardLogicComponent.HintManagerComponent.HintAndSetByDoubleClick(this);
            }

            if (Time.time > _newTime)
            {
                _tapCount = 0;
                IsDoubleCLickActionStarted = false;
            }
        }
    }
}