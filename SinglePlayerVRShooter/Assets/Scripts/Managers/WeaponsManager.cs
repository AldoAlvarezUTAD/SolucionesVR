using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class WeaponsManager : SingletonObj<WeaponsManager>
	{
		#region UNITY METHODS
		protected override void Awake()
		{
            base.Awake();
            firearmsObj = new GameObject[firearms.Length];
            for (int i = 0; i < firearms.Length; ++i)
                firearmsObj[i] = Instantiate(firearms[i].Prefab, gunHolder);
            HideFirearms();
		}
        #endregion

        #region VARIABLES
        [SerializeField]
        private Transform gunHolder;
        [SerializeField]
        private SO_Firearm[] firearms;
        private GameObject[] firearmsObj;
		#endregion

		#region PUBLIC METHODS
        public SO_Firearm GetRandomFirearm()
        {
            int index = Random.Range(0, firearms.Length);
            HideFirearms();
            firearmsObj[index].SetActive(true);
            return firearms[index];
        }
        public void Restart()
        {
            GameManager.instance.Player.GetComponent<PlayerController>().firearm.SwitchFirearm(firearms[0]);
            firearmsObj[0].SetActive(true);
        }
        #endregion

        #region PRIVATE METHODS
        private void HideFirearms()
		{
            for (int i = 0; i < firearms.Length; ++i)
                firearmsObj[i].SetActive(false);
        }
        #endregion
    }
}