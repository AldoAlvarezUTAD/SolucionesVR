using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace UTAD
{
	public class PlayerController : MonoBehaviour, IRestartable
	{
		#region UNITY METHODS
		private void Start()
		{
		}


        private void Update()
        {
            if (GameManager.Instance.gameState == GameStates.Paused) return;

            GrabTypes grab = right.GetGrabStarting();
            switch (grab)
            {
                case GrabTypes.Pinch: firearm.Shoot(); break;
                default:break;
            }

            grab = left.GetGrabStarting();
            switch (grab)
            {
                case GrabTypes.Pinch:
                    GameManager.Instance.PauseGame();
                    break;
                default:break;
            }
        }
        #endregion

        #region VARIABLES
        [SerializeField] private Hand right, left;
        [SerializeField]
        private PlayerHealth healthCtrl;
        public PlayerHealth Health => healthCtrl;
        [SerializeField]
        private Firearm firearmCtrl;
        public Firearm firearm => firearmCtrl;
        [SerializeField]
        private GameObject teleport;
        #endregion

        #region PUBLIC METHODS
        public void Pause() {
            teleport.SetActive(false);
        }
        public void Resume() {
            teleport.SetActive(true);
        }
        public void Restart()
		{
            healthCtrl.Restart();
		}
		#endregion

		#region PRIVATE METHODS
		private void CheckInputs()
		{
            if (Input.GetKey(KeyCode.W)) {

            }
		}
        #endregion
    }
}