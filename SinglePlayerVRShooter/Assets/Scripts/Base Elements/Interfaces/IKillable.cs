using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public interface IKillable
	{

		#region VARIABLES
        float Health { get; }
        #endregion

        #region PUBLIC METHODS
        void ReceiveDamage(float damage);
        void Heal(float amout);
        void Die();
		#endregion

	}
}