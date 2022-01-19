using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerClient.Network;

namespace Player
{
	public class Move : MonoBehaviour
	{
		// Start is called before the first frame update
		[SerializeField] private float speed = 4;
		// public Animator animator;

		[SerializeField] private NetworkIdentity networkIdentity;

		public void Update()
		{
			if (networkIdentity.IsControlling())
			{
				checkMovement();
			}
		}

		private void checkMovement()
		{
			float horizatal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");

			transform.position += new Vector3(horizatal, vertical, 0) * speed * Time.deltaTime;
			// Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

			// animator.SetFloat("Horizontal", movement.x);
			// animator.SetFloat("Vertical", movement.y);
			// animator.SetFloat("Magnitude", movement.magnitude);

			// transform.position = transform.position + movement * speed * Time.deltaTime;
		}

	}
}