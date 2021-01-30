using UnityEngine;

namespace Bullet
{
	public class AutoFire : MonoBehaviour
	{
        public Turret turretSettings;
		private GameObject target;

		void Start()
		{
			if (turretSettings.bulletFollowPlayer)
			{
				turretSettings.bulletDirection = Vector2.zero;
				target = GameObject.FindWithTag("Player");
			}

			if (turretSettings.singleBullet)
				turretSettings.reloadSpeed = 0f;

			InvokeRepeating("FireBullet", 0, turretSettings.reloadSpeed);
		}

		void FireBullet()
		{
			BulletGenerator generator = new BulletGenerator(gameObject, target, turretSettings.usePathfinding, turretSettings.bulletDirection, transform.position, turretSettings.bulletSize, turretSettings.bulletSpeed, turretSettings.bulletSprite, turretSettings.bulletColor, turretSettings.bulletAnimation, turretSettings.bulletDestroyBullet);
			generator.CreateBullet();
		}
	}
}
