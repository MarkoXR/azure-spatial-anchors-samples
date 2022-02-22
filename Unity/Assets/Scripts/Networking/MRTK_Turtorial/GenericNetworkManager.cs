using System;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Networking.MRTK_Turtorial
{
    public class GenericNetworkManager : MonoBehaviour
    {
        public static GenericNetworkManager Instance { get; set; }

        [HideInInspector] public PhotonView localUser;
        private bool isConnected;

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
