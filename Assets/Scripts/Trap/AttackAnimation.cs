using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
	[SerializeField] PiqueBehaviour piqueBehaviourScript;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Feet"))
		{
			piqueBehaviourScript.SwitchToAttackMode();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Feet"))
		{
			piqueBehaviourScript.SwitchToAwareMode();
		}
	}
}
