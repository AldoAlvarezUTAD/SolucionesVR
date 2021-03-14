using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class EnemiesManager : MonoBehaviour, IRestartable
	{
		#region UNITY METHODS
		private void Start()
		{
            Enemies = new List<Enemy>[enemyTypes.Length];
            for (int i = 0; i < enemyTypes.Length; ++i)
            {
                Enemies[i] = CreateEnemies(enemyTypes[i], 10);
            }
		}


		private void FixedUpdate()
		{
            if (GameManager.Instance.gameState == GameStates.Paused) return;

            timer -= Time.fixedDeltaTime;
            if (timer <= 0f) {
                timer = instanceTime * timeModifier;
                ActivateNewEnemy();
            }
		}
        #endregion

        #region VARIABLES
        [SerializeField, Range(1, 10f)]
        private float instanceTime = 4f;
        [SerializeField]
        private SO_Enemy []enemyTypes;

        private List<Enemy>[] Enemies;

        private float timer = 0f;
        private float timeModifier = 1f;
        #endregion

        #region PUBLIC METHODS
        public void Pause()
        {
            for (int type = 0; type < enemyTypes.Length; ++type)
            {
                for (int i = 0; i < Enemies[type].Count; ++i)
                {
                    Enemy enemy = Enemies[type][i];
                    if (enemy.isAlive)
                        enemy.PauseAI();
                }
            }
        }
        public void Resume()
        {
            for (int type = 0; type < enemyTypes.Length; ++type)
            {
                for (int i = 0; i < Enemies[type].Count; ++i)
                {
                    Enemy enemy = Enemies[type][i];
                    if (enemy.isAlive)
                        enemy.ResumeAI();
                }
            }
        }
        public void Restart()
        {
            timer = instanceTime;
            timeModifier = 1f;
            for (int type = 0; type < enemyTypes.Length; ++type)
            {
                for (int i = 0; i < Enemies[type].Count; ++i) {
                Enemy enemy = Enemies[type][i];
                    if (enemy.isAlive)
                        DisableEnemy(enemy);
                }
            }

            ActivateNewEnemy();
        }

        public void ModifyInstanceTime(float value)
        {
            timeModifier = value;
        }
        #endregion

        #region PRIVATE METHODS
        private void ActivateNewEnemy()
        {
            int type = Random.Range(0, enemyTypes.Length);
            Transform point = GameManager.Instance.GetRandomPoint();

            Enemy enemy = GetDisabledEnemy(type);
            if (enemy != null)
                ActivateEnemy(enemy, point);
            else
                CreateNewEnemy(type, point);
        }

        private Enemy GetDisabledEnemy(int type)
        {
            for (int i = 0; i < Enemies[type].Count; ++i)
            {
                Enemy enemy = Enemies[type][i];
                if (!Enemies[type][i].isAlive)
                    return enemy;
            }
            return null;
        }

        private void ActivateEnemy(Enemy enemy, Transform point)
        {
            enemy.InstantiateOn(point);
            enemy.Restart();
        }

        private void CreateNewEnemy(int type, Transform point)
        {
            Enemy enemy = InstantiateNewEnemy(enemyTypes[type]);
            ActivateEnemy(enemy, point);
        }

        private List<Enemy> CreateEnemies(SO_Enemy type, int count)
		{
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < count; ++i)
            {
                Enemy enemy = InstantiateNewEnemy(type);
                DisableEnemy(enemy);
                enemy.SetData(type);
                enemies.Add(enemy);
            }
            return enemies;
		}

        private Enemy InstantiateNewEnemy(SO_Enemy type)
        {
            GameObject enemyObj = Instantiate(type.Prefab, transform);
            Enemy enemyBehaviour = enemyObj.GetComponent<Enemy>();
            if (enemyBehaviour == null)
                enemyBehaviour = enemyObj.AddComponent<Enemy>();
            return enemyBehaviour;
        }

        private void DisableEnemy(Enemy enemy)
        {
            enemy.Die();
        }
        #endregion
    }
}