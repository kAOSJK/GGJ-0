using UnityEngine;

namespace Bullet
{
	public class Bullet
	{

		public GameObject turret; /* the turret that fired the bullet */
		public GameObject target; /* the bullet's target */
		public bool pathfinding; /* the bullet go to the target using pathfinding a* tech */
		public Vector2 direction; /* the bullet's direction (if it has no target) */
		public Vector2 startPosition; /* the bullet's start position */
		public float speed; /* the bullet's speed */
		public bool bulletDestroyBullet; /* the bullet destroy on contact to an other bullet */

		public Bullet(GameObject turret, GameObject target, bool pathfinding, Vector3 direction, Vector3 startPosition, float speed, bool bulletDestroyBullet)
		{
			this.turret = turret;
			this.target = target;
			this.pathfinding = pathfinding;
			this.direction = direction;
			this.startPosition = startPosition;
			this.speed = speed;
			this.bulletDestroyBullet = bulletDestroyBullet;
		}
	}
}