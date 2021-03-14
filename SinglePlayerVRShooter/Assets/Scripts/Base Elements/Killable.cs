using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UTAD
{
	public abstract class Killable : MonoBehaviour, IKillable, IRestartable
	{

        #region VARIABLES
        [SerializeField, Range(50,500)]
        protected float maxHealth = 100;

        public float Health { get; }
        public bool isAlive => health > 0;
        protected float health;
		#endregion

		#region PUBLIC METHODS
		public void Die()
		{
            health = 0;
            OnDie();
		}
        public void Heal(float amount)
        {
            ModifyHealth(Mathf.Abs(amount));
            OnHeal(amount);
        }
        public void ReceiveDamage(float damage)
        {
            ModifyHealth(-Mathf.Abs(damage));
            OnDamageReceived(damage);
        }
        public void Restart()
        {
            health = maxHealth;
            OnRestart();
        }
        #endregion

        #region PROTECTED METHODS
        protected virtual void OnDie() { }
        protected virtual void OnHeal(float amount) { }
        protected virtual void OnDamageReceived(float damage) { }
        protected virtual void OnRestart() { }
        #endregion

        #region PRIVATE METHODS
        private void ModifyHealth(float amount)
		{
            health += amount;
            if (health > maxHealth)
                health = maxHealth;
            else if (health <= 0)
                Die();
        }
        #endregion
    }
}