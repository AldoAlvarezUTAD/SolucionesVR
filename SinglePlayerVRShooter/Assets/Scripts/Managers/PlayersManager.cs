using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class PlayersManager : SingletonObj<PlayersManager>
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
        [SerializeField, Range(10, 200)]
        private int maxHealth = 100;
        public int MaxHealth => maxHealth;

        [SerializeField, Range(0.0f, 1f)]
        private float healthRegenRate = 0.1f;
        [SerializeField, Range(1, 10)]
        private float healthRegenTime = 100;

        public float HealthRegenRate => healthRegenRate;
        public float HealthRegenTime => healthRegenTime;
        #endregion

        #region PUBLIC METHODS
        public void Method()
		{
			
		}
		#endregion

		#region PRIVATE METHODS
		private void method()
		{
			
		}
		#endregion
	}
}