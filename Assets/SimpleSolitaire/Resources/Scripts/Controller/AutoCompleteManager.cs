using System.Collections;
using UnityEngine;

namespace SimpleSolitaire.Controller
{
    public class AutoCompleteManager : MonoBehaviour
    {
        [Tooltip("The state of auto complete actions.")]
        public bool IsAutoCompleteActive = false;

        [Tooltip("Time between cards sets on correct place. (Transition)")]
        public float HintSetTransitionTime = 0.2f;

        [Header("Components")]
        public HintManager HintComponent;
        public GameObject AutoCompleteHintButtonObj;

        private IEnumerator _doubleClickAutoCompleteCoroutine;
        private IEnumerator _autoCompleteCoroutine;
        private bool _isCanComplete = true;

        private bool _autoCompleteFeatureEnable = true;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CompleteGame();
            }
        }

        public void SetEnableAutoCompleteFeature(bool state)
        {
            _autoCompleteFeatureEnable = state;
            AutoCompleteHintButtonObj.SetActive(_isCanComplete && state);
        }

        /// <summary>
        /// Activate autocomplete availability with button.
        /// </summary>
        public void ActivateAutoCompleteAvailability()
        {
            _isCanComplete = true;

            if (!_autoCompleteFeatureEnable)
            {
                return;
            }
            AutoCompleteHintButtonObj.SetActive(true);
        }

        /// <summary>
        /// Deactivate autocomplete availability with button.
        /// </summary>
        public void DectivateAutoCompleteAvailability()
        {
            _isCanComplete = false;

            if (!_autoCompleteFeatureEnable)
            {
                return;
            }
            AutoCompleteHintButtonObj.SetActive(false);
        }

        /// <summary>
        /// Call auto complete action.
        /// </summary>
        public void CompleteGame()
        {
            if (_isCanComplete)
            {
                _isCanComplete = false;
                StopAutoComplete();
                _autoCompleteCoroutine = CompleteCoroutine();
                StartCoroutine(_autoCompleteCoroutine);
            }
        }

        /// <summary>
        /// Auto complete actions in coroutine.
        /// </summary>
        private IEnumerator CompleteCoroutine()
        {
            IsAutoCompleteActive = true;
            HintComponent.UpdateAvailableForAutoCompleteCards();

            while (HintComponent.IsHasHint())
            {
                HintComponent.HintAndSet(HintSetTransitionTime);

                yield return new WaitWhile(() => HintComponent.IsHintProcess);
            }

            IsAutoCompleteActive = false;
            HintComponent.UpdateAvailableForDragCards();
        }

        /// <summary>
        /// Deactivate auto complete coroutine.
        /// </summary>
        private void StopAutoComplete()
        {
            if (_autoCompleteCoroutine != null)
            {
                StopCoroutine(_autoCompleteCoroutine);
            }
        }

        /// <summary>
		/// Deactivate double click auto complete coroutine.
		/// </summary>
		private void StopDoubleClickAutoComplete()
        {
            if (_doubleClickAutoCompleteCoroutine != null)
            {
                StopCoroutine(_autoCompleteCoroutine);
            }
        }

        /// <summary>
        /// Auto complete actions in coroutine.
        /// </summary>
        private IEnumerator DoubleClickAutoCompleteCoroutine(Card card)
        {
            IsAutoCompleteActive = true;
            _isCanComplete = false;
            HintComponent.UpdateAvailableForAutoCompleteCards();
            AutoCompleteHintButtonObj.SetActive(false);

            HintComponent.HintAndSet(HintSetTransitionTime);
            yield return new WaitWhile(() => HintComponent.IsHintProcess);

            _isCanComplete = true;
            IsAutoCompleteActive = false;
            HintComponent.UpdateAvailableForDragCards();
        }
    }
}