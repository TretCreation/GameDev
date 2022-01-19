using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServerClient.Utility;
using SocketIO;
using System.Globalization;

namespace ServerClient.Network
{
	public class NetworkManager : SocketIOComponent
	{
		[Header("Network Client")]
		[SerializeField] private Transform networkContainer;
		[SerializeField] private GameObject TestObject;

		public static string ClientID { get; private set; }

		private Dictionary<string, NetworkIdentity> serverObjects;

		public override void Start()
		{
			base.Start();

			// NetworkManager.Instance = this;

			CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");


			initialize();
			setupEvents();

		}

		public override void Update()
		{
			base.Update();
		}

		private void initialize()
		{
			serverObjects = new Dictionary<string, NetworkIdentity>();
		}

		private void setupEvents()
		{
			On("open", (E) =>
			{
				Debug.Log("Connection made to the server");

			});

			On("register", (E) =>
			{
				ClientID = E.data["id"].ToString().RemoveQuotes();

				Debug.LogFormat("Our Client's ID ({0})", ClientID);
			});

			On("spawn", (E) =>
			{
				string id = E.data["id"].ToString().RemoveQuotes();

				GameObject go = Instantiate(TestObject, networkContainer);
				go.name = string.Format("Player ({0})", id);
				NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
				ni.SetControllerID(id);
				ni.SetSocketReference(this);
				serverObjects.Add(id, ni);
			});

			On("disconnected", (E) =>
			{
				string id = E.data["id"].ToString().RemoveQuotes();

				GameObject go = serverObjects[id].gameObject;
				Destroy(go);
				serverObjects.Remove(id);
			});

			On("updatePosition", (E) =>
			{

				// CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
				string id = E.data["id"].ToString().RemoveQuotes();
				// float f = float.Parse(id,
				// 	  System.Globalization.NumberStyles.AllowThousands,
				// 	  CultureInfo.InvariantCulture);
				float x = E.data["position"]["x"].f;
				float y = E.data["position"]["y"].f;
				// float z = E.data["position"]["z"].f;

				// NetworkIdentity ni = serverObjects[id];
				// ni.transform.position = new Vector3(x, y, z);

				// string id = E.data["id"].ToString().RemoveQuotes();
				// float x = float.Parse(E.data["position"]["x"].str);
				// float y = float.Parse(E.data["position"]["y"].str);
				// float z = float.Parse(E.data["position"]["z"].str);


				NetworkIdentity ni = serverObjects[id];

				ni.transform.position = new Vector3(x, y, 0);
			});
		}
	}

	[SerializeField]
	public class Player
	{
		public string id;
		public Position position;
	}

	[SerializeField]
	public class Position
	{
		public float x;
		public float y;
	}
}
