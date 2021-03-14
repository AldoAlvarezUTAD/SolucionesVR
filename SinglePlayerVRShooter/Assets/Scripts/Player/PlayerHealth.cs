using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UTAD
{
	public class PlayerHealth : Killable
	{
        #region VARIABLES
        [SerializeField] private PlayerUI UI;

        [SerializeField] private GameObject Collider;
		#endregion

        #region PROTECTED METHODS
        protected override void OnDie()
        {
            Collider.SetActive(false);
            GameManager.Instance.EndGame();
        }
        protected override void OnRestart()
        {
            Collider.SetActive(true);
            UI.UpdateHealth(health / maxHealth);
        }
        protected override void OnDamageReceived(float damage)
        {
            UI.UpdateHealth(health / maxHealth);
        }
        protected override void OnHeal(float amount)
        {
            UI.UpdateHealth(health / maxHealth);
        }
        #endregion
    }
}