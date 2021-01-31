using UnityEngine;

public class AwareAnimation : MonoBehaviour
{
	[SerializeField] PiqueBehaviour piqueBehaviourScript;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Feet"))
		{
			piqueBehaviourScript.SwitchToAwareMode();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Feet"))
		{
			piqueBehaviourScript.SwitchToCoolMode();
		}
	}
}
