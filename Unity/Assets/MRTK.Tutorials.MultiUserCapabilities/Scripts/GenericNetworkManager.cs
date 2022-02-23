using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class GenericNetworkManager : MonoBehaviour
    {
        public static GenericNetworkManager Instance;

        public string AzureAnchorId { get; set; } = "";
        public PhotonView LocalUser { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(Instance.gameObject);
                    Instance = this;
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(ConnectWhenOnline());
        }

        private IEnumerator ConnectWhenOnline()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                Debug.Log("No connection");
            while (Application.internetReachability == NetworkReachability.NotReachable)
            {
                yield return new WaitForSeconds(.5f);
            }
            ConnectToNetwork();
        }

        // For future non PUN solutions
        private void StartNetwork(string ipAddress, string port)
        {
            throw new NotImplementedException();
        }

        private void ConnectToNetwork()
        {
            OnReadyToStartNetwork?.Invoke();
        }

        public static event Action OnReadyToStartNetwork;
    }
}
