using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UTAD
{
    [RequireComponent(typeof(Collider))]
	public class Enemy : Killable
	{
		#region UNITY METHODS
        private void OnTriggerEnter(Collider col)
        {
            IKillable killable = col.GetComponent<IKillable>();
            if (killable!=null) {
                killable.ReceiveDamage(damage);
                Die();
            }
        }

        private void FixedUpdate()
		{
            if (!isAlive) return;
            updateTimer -= Time.fixedDeltaTime;
            if (updateTimer <= 0f)
            {
                updateTimer = updateTime;
                navAgent.SetDestination(GameManager.Instance.Player.position);
            }
		}
        #endregion

        #region VARIABLES
        [SerializeField]
        private NavMeshAgent navAgent;
        public int damage { get; protected set; }
        private float updateTime = 0f;
        private float updateTimer = 0f;
        private float speed;
        #endregion

        #region PUBLIC METHODS
        public void PauseAI() {
            navAgent.speed = 0;
        }
        public void ResumeAI()
        {
            navAgent.speed = speed;
        }
        public void SetData(SO_Enemy data)
		{
            damage = data.damage;
            navAgent.speed = data.MovementSpeed;
            speed = data.MovementSpeed;

            updateTime = data.UpdateTime;
        }
        public void InstantiateOn(Transform point)
        {
            transform.position = point.position;
            transform.rotation = point.rotation;
        }
        #endregion

        #region PROTECTED METHODS
        protected override void OnDie()
        {
            gameObject.SetActive(false);
        }

        protected override void OnRestart()
        {
            gameObject.SetActive(true);
            updateTimer = updateTime;
            navAgent.SetDestination(GameManager.Instance.Player.position);
        }
        #endregion
    }
}