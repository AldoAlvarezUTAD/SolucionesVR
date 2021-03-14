using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
    [CreateAssetMenu(fileName = "New FireArm", menuName = "Game Elements/FireArm", order = 1)]
    public class SO_Firearm : ScriptableObject
    {
        #region VARIABLES
        [Range(0.1f, 5f)]
        public float reloadTime = 1.5f;
        [Range(1, 25)]
        public int magazineSize = 10;
        [Tooltip("Time between shots"),
         Range(0.01f, 2.0f)]
        public float rateOfFire = 0.2f;

        public SO_Bullet bulletType;
        public GameObject Prefab;
        #endregion

    }
}