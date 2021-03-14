using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace UTAD
{
    [RequireComponent(typeof(Collider), typeof(Image))]
	public class UIElement : MonoBehaviour
	{
		#region UNITY METHODS
		private void Awake()
		{
            interactible = Interactible;
            background = GetComponent<Image>();
            GetComponent<Collider>().isTrigger = true; ;
		}

        private void OnTriggerEnter(Collider other)
        {
            Hand h = other.GetComponent<Hand>();
            if (h == null) return;
            hand = h;
            if (!InteractionExists()) return;

            ChangeAppeareance(selected);
        }
        private void OnTriggerStay(Collider other)
        {
            if (!InteractionExists()) { checkingInteraction = false; return; }
            if (!isSameHand(other)) return;
            checkingInteraction = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if (!InteractionExists()) { checkingInteraction = false; return; }
            if (!isSameHand(other)) return;
            checkingInteraction = false;
            ChangeAppeareance(normal);
        }

        private void Update()
        {
            if (!checkingInteraction) return;
            if (!PlayerInteracted()) return;
            Click();
        }
        #endregion

        #region VARIABLES
        [SerializeField] protected Color normal = Color.white;
        [SerializeField] protected Color selected = Color.white;
        [SerializeField] protected Color deativated = Color.white;

        [SerializeField] protected bool Interactible = true;
        [SerializeField] private GrabTypes interactionButton;
        [SerializeField] protected UnityEvent OnClick;

        private bool interactible = true;
        private Image background;
        private Collider collision;
        private Hand hand;
        private bool checkingInteraction = false;
        #endregion

        #region PUBLIC METHODS
        public void Deactivate()
        {
            DisableInteraction();
            ChangeAppeareance(deativated);
        }
        public void Hide()
        {
            Deactivate();
            gameObject.SetActive(false);
        }
        public void Show()
        {
            SetInteraction(Interactible);
            ChangeAppeareance(normal);
            gameObject.SetActive(true);
        }

        public void EnableInteraction() => SetInteraction(true);
        public void DisableInteraction() => SetInteraction(false);

        public void Click()
        {
            if (!interactible) return;
            OnClick?.Invoke();
        }
		#endregion

		protected virtual void ChangeAppeareance(Color appeareance)
		{
            background.color = appeareance;
		}

		#region PRIVATE METHODS
        private bool InteractionExists()
        {
            return hand != null && gameObject.activeSelf;
        }
        private bool isSameHand(Collider c)
        {
            Hand h = c.GetComponent<Hand>();
            if (h == null) return false;
            if (h != hand) return false;
            return true;
        }
        private void SetInteraction(bool state) => interactible = state;
        private bool PlayerInteracted()
        {
            GrabTypes grab = hand.GetGrabStarting();
            return grab == interactionButton;
        }
		#endregion
	}
}