﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiqueBehaviour : MonoBehaviour
{
	public static PiqueBehaviour Instance { get; private set; }

	[SerializeField] private float stayTimeToDie;
	[SerializeField] private Animator animator; /* animator */

	private float currentStayTime;
	private bool finishedCooldown;

	private void Start()
    {
		currentStayTime = 0f;
		finishedCooldown = false;
	}

    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Feet"))
		{
			currentStayTime += Time.deltaTime;

			if (currentStayTime >= stayTimeToDie)
            {
				collision.transform.parent.GetComponent<Controller>().KillPlayer();
			}
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Feet"))
		{
			currentStayTime = 0f;
		}
	}

	public void SwitchToCoolMode()
    {
		animator.SetBool("isInTheAwareZone", false);
	}

	public void SwitchToAwareMode()
	{
		animator.SetBool("isInTheAwareZone", true);
		animator.SetBool("isInTheAttackZone", false);
	}

	public void SwitchToAttackMode()
	{
		animator.SetBool("isInTheAttackZone", true);
	}
}
