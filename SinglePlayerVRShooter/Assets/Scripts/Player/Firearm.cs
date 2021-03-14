using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTAD
{
	public class Firearm : MonoBehaviour, IRestartable
	{
		#region UNITY METHODS
		private void Start()
		{
            reload = ReloadFirearm();
            shoot = ShootBullet();
		}

        #endregion

        #region VARIABLES
        [SerializeField] private PlayerUI UI;
        [SerializeField] private AudioSource shotSound;
        private int remainingBullets = 0;
        private int totalBullets = 0;
        private int maxBullets = 0;
        private bool canShoot = true;
        private bool reloading = false;

        private SO_Firearm currentFirearm;

        private IEnumerator reload;
        private IEnumerator shoot;
		#endregion

		#region PUBLIC METHODS
        public void Restart()
        {
            CancelReload();
            CancelShot();
        }
		public void Reload()
        {
            if (reloading) return;
            reload = ReloadFirearm();
            StartCoroutine(reload);
        }
        public void CancelReload() { StopCoroutine(reload); }

        public void AddBullets(float magazine) {

            totalBullets += Mathf.FloorToInt(currentFirearm.magazineSize*magazine);
            if (totalBullets > maxBullets)
                totalBullets = maxBullets;
        }

        public void SwitchFirearm(SO_Firearm firearm)
        {
            UpdateFirearm(firearm);
        }
        public void Shoot()
        {
            if (remainingBullets <= 0)
                Reload();
            if (!canShoot) return;
            shoot = ShootBullet();
            StartCoroutine(shoot);
        }
        #endregion

        #region PRIVATE METHODS
        private IEnumerator ReloadFirearm()
        {
            reloading = true;
            //conservo las del cargador
            totalBullets += remainingBullets;

            int bullets = 0;
            if (totalBullets > currentFirearm.magazineSize)
                bullets = currentFirearm.magazineSize;
            else
                bullets = totalBullets;
            //quito las balas que voy a meter del total
            totalBullets -= bullets;
            yield return new WaitForSeconds(currentFirearm.reloadTime);
            //seteo las blas con las que me queden o un cargador nuevo
            remainingBullets = bullets;
            UI.UpdateBullets(remainingBullets);
            reloading = false;
        }
        private IEnumerator ShootBullet()
        {
            if (remainingBullets > 0)
            {
                canShoot = false;
                remainingBullets--;
                shotSound.Play();
                ShootRaycast();
                UI.UpdateBullets(remainingBullets);
                yield return new WaitForSeconds(currentFirearm.rateOfFire);
                canShoot = true;
            }
            yield return null;
        }
        private void ShootRaycast()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, currentFirearm.bulletType.effectiveDistance)){
                IKillable killable = info.collider.GetComponent<IKillable>();
                if (killable != null)
                    killable.ReceiveDamage(currentFirearm.bulletType.Damage);
            }
        }
        private void UpdateFirearm(SO_Firearm firearm)
        {
            currentFirearm = firearm;
            maxBullets = firearm.magazineSize * 6;
            totalBullets = maxBullets;
            remainingBullets = firearm.magazineSize;
            UI.UpdateBullets(remainingBullets);
        }
        private void CancelShot() { canShoot = true; StopCoroutine(shoot); }
		#endregion
	}
}