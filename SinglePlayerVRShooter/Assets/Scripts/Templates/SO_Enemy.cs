using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Game Elements/Enemy", order = 1)]
    public class SO_Enemy : ScriptableObject
	{
        #region VARIABLES
        [Range(0.1f, 5f)]
        public float MovementSpeed = 1f;
        [Range(0f, 5f)]
        [Tooltip("The time between the updates for the player position.")]
        public float UpdateTime = 1f;
        [Range(1, 100)]
        public int damage = 10;
        public GameObject Prefab;
        #endregion
    }
}