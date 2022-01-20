using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerClient;


public class Move : MonoBehaviour
{
	public Animator animator;
	[Header("Data")]
	[SerializeField]
	private float speed = 2;

	[SerializeField] private Transform shootPosition;

	[Header("Class References")]
	[SerializeField]
	private NetworkIdentity networkIdentity;

	public void Update()
	{
		if (networkIdentity.IsControlling())
		{
			checkMovement();
		}
	}
	private void checkMovement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

		// animator.SetFloat("Horizontal", movement.x);
		// animator.SetFloat("Vertical", movement.y);
		// animator.SetFloat("Magnitude", movement.magnitude);

		transform.position = transform.position + movement * speed * Time.deltaTime;
	}

}

