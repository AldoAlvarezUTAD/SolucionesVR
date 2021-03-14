using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
    [CreateAssetMenu(fileName = "New Bullet", menuName = "Game Elements/Bullet", order = 1)]
    public class SO_Bullet : ScriptableObject
	{
        #region VARIABLES
        [Tooltip("The drop of the bullet. Make 0 for it to not be affected by gravity."),
         Range(0.0f, 2.0f)]
        public float dropfall = 0f;
        [Tooltip("The speed at which the bullet will travel."),
        Range(2.0f, 20.0f)]
        public float Speed = 0f;
        [Range(1,100)]
        public int Damage = 1;
        [Range(10, 100)]
        public float effectiveDistance = 20;
        #endregion
    }
}