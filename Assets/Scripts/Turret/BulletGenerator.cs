using UnityEngine;

namespace Bullet
{
	public class BulletGenerator
	{
		private GameObject turret; /* the turret that fired the bullet */
		private GameObject target; /* the bullet's target */
		public bool pathfinding; /* the bullet go to the target using pathfinding a* tech */
		private Vector2 direction; /* the bullet's direction (if it has no target) */
		private Vector2 startPosition; /* the bullet's start position */
		private float speed; /* the bullet's speed */
		private float size; /* the bullet's size */
		private Sprite sprite; /* the bullet's sprite */
		private Color color; /* the bullet sprite's color */
		private RuntimeAnimatorController animation; /* the bullet's animation */
		private bool bulletDestroyBullet; /* the bullet destroy on contact to an other bullet */

		public BulletGenerator(GameObject turret, Vector2 direction, Sprite sprite)
		{
			this.turret = turret;
			this.target = null;
			this.startPosition = turret.transform.position;
			this.direction = direction;
			this.size = 1f;
			this.speed = 5f;
			this.sprite = sprite;
			this.color = Color.red;
			this.bulletDestroyBullet = true;
		}

		public BulletGenerator(GameObject turret, Vector2 direction, Vector2 startPosition, Sprite sprite)
		{
			this.turret = turret;
			this.target = null;
			this.startPosition = startPosition;
			this.direction = direction;
			this.size = 1f;
			this.speed = 5f;
			this.sprite = sprite;
			this.color = Color.red;
			this.bulletDestroyBullet = true;
		}

		public BulletGenerator(GameObject turret, Vector2 direction, Vector2 startPosition, float size, float speed, Sprite sprite, Color color, bool bulletDestroyBullet)
		{
			this.turret = turret;
			this.target = null;
			this.startPosition = startPosition;
			this.direction = direction;
			this.size = size;
			this.speed = speed;
			this.sprite = sprite;
			this.color = color;
			this.bulletDestroyBullet = bulletDestroyBullet;
		}

		public BulletGenerator(GameObject turret, GameObject target, Sprite sprite)
		{
			this.turret = turret;
			this.target = target;
			this.startPosition = turret.transform.position;
			this.direction = Vector3.zero;
			this.size = 1f;
			this.speed = 5f;
			this.sprite = sprite;
			this.color = Color.red;
			this.bulletDestroyBullet = true;
		}

		public BulletGenerator(GameObject turret, GameObject target, Vector2 startPosition, Sprite sprite)
		{
			this.turret = turret;
			this.target = target;
			this.startPosition = startPosition;
			this.direction = Vector3.zero;
			this.size = 1f;
			this.speed = 5f;
			this.sprite = sprite;
			this.color = Color.red;
			this.bulletDestroyBullet = true;
		}

		public BulletGenerator(GameObject turret, GameObject target, Vector2 startPosition, float size, float speed, Sprite sprite, Color color, bool bulletDestroyBullet)
		{
			this.turret = turret;
			this.target = target;
			this.startPosition = startPosition;
			this.direction = Vector3.zero;
			this.size = size;
			this.speed = speed;
			this.sprite = sprite;
			this.color = color;
			this.bulletDestroyBullet = bulletDestroyBullet;
		}

		public BulletGenerator(GameObject turret, GameObject target, bool pathfinding, Vector2 direction, Vector2 startPosition, float size, float speed, Sprite sprite, Color color, RuntimeAnimatorController animation, bool bulletDestroyBullet)
		{
			this.turret = turret;
			this.target = target;
			this.pathfinding = pathfinding;
			this.startPosition = startPosition;
			this.direction = direction;
			this.size = size;
			this.speed = speed;
			this.sprite = sprite;
			this.color = color;
			this.animation = animation;
			this.bulletDestroyBullet = bulletDestroyBullet;
		}

		public GameObject CreateBullet()
		{
			GameObject bullet = new GameObject();
			SpriteRenderer bulletSprite;
			BulletMovement bulletScript;
			PolygonCollider2D bulletCollider;
			Rigidbody2D bulletBody;

			/* Name & Tag */
			bullet.name = "Bullet_tmp";
			bullet.gameObject.tag = "Bullet";
			/* Size */
			bullet.transform.localScale = new Vector3(size, size, 1);
			/* Sprite */
			bulletSprite = bullet.AddComponent<SpriteRenderer>();
			bulletSprite.sprite = sprite;
			bulletSprite.sortingOrder = 1;
			/* Color */
			bulletSprite.color = color;
			/* Script */
			bulletScript = bullet.AddComponent<BulletMovement>();
			bulletScript.bullet = new Bullet(turret, target, pathfinding, direction, startPosition, speed, bulletDestroyBullet);
            /* Collider2D */
            bulletCollider = bullet.AddComponent<PolygonCollider2D>();
            bulletCollider.isTrigger = true;
			/* Rigibody2D if it has a direction */
			if (direction != Vector2.zero || !pathfinding)
			{
				bulletBody = bullet.AddComponent<Rigidbody2D>();
				bulletBody.gravityScale = 0f;
				bulletBody.constraints = RigidbodyConstraints2D.FreezeRotation;
				bulletScript.body = bulletBody;
			}
			/* Animation if it has one */
			if (animation != null)
            {
				Animator animator;
				animator = bullet.AddComponent<Animator>();
				animator.runtimeAnimatorController = animation;
			}

            return bullet;
		}
	}
}