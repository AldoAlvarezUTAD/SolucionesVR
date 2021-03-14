using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTAD
{
	public class PlayerUI : MonoBehaviour
	{
		#region UNITY METHODS
		private void Start()
		{
			
		}


		private void Update()
		{
			
		}
        #endregion

        #region VARIABLES
        [SerializeField] private Image healthBar;
        [SerializeField] private Text bullets;
		#endregion

		#region PUBLIC METHODS
		public void UpdateHealth(float percent)
		{
            percent = Mathf.Clamp01(percent);
            healthBar.fillAmount = percent;
		}
        public void UpdateBullets(int amount)
        {
            bullets.text = amount.ToString();
        }
        #endregion

    }
}