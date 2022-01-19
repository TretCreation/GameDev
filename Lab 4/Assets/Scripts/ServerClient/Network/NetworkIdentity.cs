using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerClient.Utility;
using SocketIO;

namespace ServerClient.Network
{
	public class NetworkIdentity : MonoBehaviour
	{
		[SerializeField]
		[GreyOut]
		private string id;

		[SerializeField]
		[GreyOut]
		private bool isControlling;

		private SocketIOComponent socket;

		public void Awake()
		{
			isControlling = false;
		}

		public void SetControllerID(string ID)
		{
			id = ID;
			isControlling = (NetworkManager.ClientID == ID) ? true : false;
		}

		public void SetSocketReference(SocketIOComponent Socket)
		{
			socket = Socket;
		}

		public string GetID()
		{
			return id;
		}

		public bool IsControlling()
		{
			return isControlling;
		}

		public SocketIOComponent GetSocket()
		{
			return socket;
		}
	}
}
