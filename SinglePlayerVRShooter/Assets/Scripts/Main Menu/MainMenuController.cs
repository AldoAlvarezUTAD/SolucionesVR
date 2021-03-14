using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class MainMenuController : MonoBehaviour
	{
        #region UNITY METHODS
        private void Awake()
        {
        }

        private void Start()
		{
            DisableButtons();
		}
        #endregion

        #region VARIABLES
        [SerializeField] private UIElement[] buttons;
        #endregion

        #region PUBLIC METHODS
        public void JoinGame()
        {
            DisableButtons();
        }
        public void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
#endregion

        #region PRIVATE METHODS


        private void ShowButtons()
        {
            for (int i = 0; i < buttons.Length; ++i)
                buttons[i].Show();
        }
        private void DisableButtons()
        {
            for (int i = 0; i < buttons.Length; ++i)
                buttons[i].Deactivate();
        }
        #endregion
    }
}