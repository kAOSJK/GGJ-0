using UnityEngine;

namespace Bullet
{
	public class BulletMovement : MonoBehaviour
	{
		public Bullet bullet; /* Bullet that is initialized in the Bullet Generator */
		public Rigidbody2D body; /* Rigidbody2D that is initialized in the Bullet Generator */
		private bool hasDirection = false;
		private bool isFacingRight = false; /* direction */

		private void Start()
		{
			transform.position = bullet.startPosition;

			if (bullet.target == null && bullet.direction == Vector2.zero)
				Debug.Log(transform.name + " Bullet doesn't has any target or direction");
			else if (bullet.target != null && bullet.pathfinding)
				MoveToTargetPathfinding();
            else if (bullet.direction != Vector2.zero)
				hasDirection = true;
        }

		private void Update()
		{
            if (hasDirection)
                MoveToDirection();
			else if (bullet.target != null && !bullet.pathfinding)
				MoveToTarget();

			Debug.Log(Mathf.Round(GetComponent<Pathfinding.AIPath>().desiredVelocity.x) + " / " + Mathf.Round(GetComponent<Pathfinding.AIPath>().steeringTarget.x));

			/* Flip */
			if (bullet.target != null && bullet.pathfinding)
			{
				if (GetComponent<Pathfinding.AIPath>().desiredVelocity.x > 0 && !isFacingRight)
					Flip();
				else if (GetComponent<Pathfinding.AIPath>().desiredVelocity.x < 0 && isFacingRight)
					Flip();
			}
		}

        private void MoveToDirection()
        {
            body.velocity = bullet.direction * bullet.speed;
		}

		private void MoveToTarget()
		{
			transform.position = Vector2.MoveTowards(transform.position, bullet.target.transform.position, bullet.speed * Time.deltaTime);
		}

		private void MoveToTargetPathfinding()
		{
            Pathfinding.Seeker seekerScript;
			Pathfinding.AIPath aipathScript;
			Pathfinding.AIDestinationSetter aidestinationsetterScript;

			seekerScript = gameObject.AddComponent<Pathfinding.Seeker>();
			aipathScript = gameObject.AddComponent<Pathfinding.AIPath>();
			aidestinationsetterScript = gameObject.AddComponent<Pathfinding.AIDestinationSetter>();

            /* AIPATH */
            aipathScript.enableRotation = false;
            aipathScript.radius = 1f;
			aipathScript.height = 1f;
			aipathScript.maxSpeed = bullet.speed;
			aipathScript.orientation = Pathfinding.OrientationMode.YAxisForward;
			aipathScript.slowdownDistance = 0f;
			aipathScript.gravity = Vector3.zero;
			/* AI DESTINATION */
			aidestinationsetterScript.target = bullet.target.transform;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.name != bullet.turret.gameObject.name)
			{
				if (collision.gameObject.CompareTag("Bullet"))
				{
					if (bullet.bulletDestroyBullet)
						Destroy(gameObject);
				}
				else
					Destroy(gameObject);
			}
		}

		void Flip()
		{
			//Change le boolean qui permet de savoir si il regarde à droite ou non
			isFacingRight = !isFacingRight;
			//Change la direction du joueur
			transform.Rotate(0f, 180f, 0f);
			//Active la trainée de pas
		}
	}
}
