using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameStates { InGame, Paused }
namespace UTAD
{
	public sealed class GameManager : SingletonObj<GameManager>, IRestartable
	{
		#region UNITY METHODS
		protected override void Awake()
		{
            base.Awake();
            SetInstancePoints();
        }


		private void FixedUpdate()
		{
            if (gameState == GameStates.Paused) return;
            CheckRound();
            CheckPerks();
        }
        #endregion

        #region VARIABLES
        public GameStates gameState { get; private set; } = GameStates.Paused;

        public Transform Player => player;
        [SerializeField] private Transform player;
        [SerializeField] private Transform playerInitialPosition;
        [SerializeField] private PlayerController playerCrtl;
        [SerializeField] private EnemiesManager enemies;
        [SerializeField] private WeaponsManager weapons;

        [SerializeField]
        private Transform InstancePoints;
        private Transform []instancePoints;

        [SerializeField, Range(15, 60)] private float perkTime = 25;
        [SerializeField, Range(15, 60)] private float roundTime = 25;

        [SerializeField] private UnityEvent OnPause, OnGameOver;
        private int round = 1;
        private float perkTimer = 0f;
        private float roundTimer = 0f;
        #endregion

        #region PUBLIC METHODS
        public void StartGame() { Restart(); }
        public void PauseGame() {
            gameState = GameStates.Paused;
            OnPause.Invoke();
            enemies.Pause();
            playerCrtl.Pause();
        }
        public void ResumeGame() {
            gameState = GameStates.InGame;
            enemies.Resume();
            playerCrtl.Resume();
        }
        public void EndGame()
        {
            PauseGame();
            OnGameOver?.Invoke();
        }
        public void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Restart() {
            player.position = playerInitialPosition.position;
            player.rotation = playerInitialPosition.rotation;

            weapons.Restart();
            playerCrtl.Restart();
            enemies.Restart();

            round = 1;
            perkTimer = perkTime;
            roundTimer = roundTime;
            ResumeGame();
        }

		public Transform GetRandomPoint()
		{
            int index = Random.Range(0,instancePoints.Length);
            return instancePoints[index];
		}
#endregion

#region PRIVATE METHODS
		private void SetInstancePoints()
		{
            if (instancePoints != null) return;
            int totalPoints = InstancePoints.childCount;
            instancePoints = new Transform[totalPoints];
            for (int point = 0; point < totalPoints; ++point)
            {
                instancePoints[point] = InstancePoints.GetChild(point);
            }
		}
        private void CheckPerks()
        {
            perkTimer -= Time.fixedDeltaTime;
            if (perkTimer <= 0f) {
                perkTimer = perkTime;
                ApplyRandomPerk();
            }
        }
        private void ApplyRandomPerk()
        {
            int prob = Random.Range(0, 100);
            if (prob > 80)
            {
                //change weapon
            }
            else if (prob > 30)
            {
                //add ammo
                playerCrtl.firearm.AddBullets(1.5f);
            }
            else {
                //heal
                playerCrtl.Health.Heal(35);
            }
        }
        private void CheckRound() {
            if (round >= 10) return;
            roundTimer -= Time.fixedDeltaTime;
            if (roundTimer <= 0f)
            {
                roundTimer = roundTime;
                ChangeRound();
            }
        }
        private void ChangeRound()
        {
            round = Mathf.Clamp(++round, 1, 10);
            enemies.ModifyInstanceTime(1 - (0.05f * round));
        }
#endregion
    }
}