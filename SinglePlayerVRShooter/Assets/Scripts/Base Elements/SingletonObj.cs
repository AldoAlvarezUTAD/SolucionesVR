using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class SingletonObj<T> : MonoBehaviour where T : Behaviour
	{
		#region UNITY METHODS
		protected virtual void Awake()
		{
            SetInstance();
		}
		#endregion

		#region VARIABLES
        public static T Instance { get { return GetInstance();} }
        protected static T instance = null;
        private static bool searchingInstance = false;
		#endregion

		#region PRIVATE METHODS
		private static T GetInstance()
		{
            return instance;
		}

        private static void SetInstance()
        {
            if (instance != null) return;
            if (searchingInstance) return;
            searchingInstance = true;

            T[]instances = GameObject.FindObjectsOfType<T>();
            if (instances.Length >= 1)
            {
                instance = instances[0];
                for (int i = 1; i < instances.Length; ++i)
                    DisableInstance(instances[i]);
            }
            else
            {
                instance = new GameObject("New " + typeof(T).ToString() + " Instance").AddComponent<T>();
            }
        }

        private static void DisableInstance(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.enabled = false;
        }
        #endregion
    }
}