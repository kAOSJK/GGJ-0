using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GetComponent<SpriteRenderer>().sprite = null;

			StartCoroutine(WaitAndGoToScenes());
		}
	}

	private IEnumerator WaitAndGoToScenes()
	{
		Debug.Log("new scene now");

		yield return new WaitForSeconds(3f);


        Debug.Log("new scene now");

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

		Destroy(gameObject);
	}
}
