using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    public class PhotonEventReceiver : MonoBehaviour, IOnEventCallback
    {
        public class PhotonEventArgs : EventArgs
        {
            public EventData EventData { get; set; }
        }

        public static PhotonEventReceiver Instance { get; set; }

        public EventHandler<PhotonEventArgs> CreatePatternEventReceived;
        public EventHandler<PhotonEventArgs> PairingEndedEventReceived;
        public EventHandler<PhotonEventArgs> ResetEventReceived;
        public EventHandler<PhotonEventArgs> StartOverEventReceived;
        public EventHandler<PhotonEventArgs> AnchorCreatedEventReceived;
        public EventHandler<PhotonEventArgs> AnchorFoundEventReceived;
        public EventHandler<PhotonEventArgs> FullResetEventReceived;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            var eventCode = photonEvent.Code;

            switch (eventCode)
            {
                case (byte)EventCode.CreatePattern:
                    CreatePatternEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.PairingEnded:
                    PairingEndedEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.Reset:
                    ResetEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.StartOver:
                    StartOverEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.AnchorCreated:
                    AnchorCreatedEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.AnchorFound:
                    AnchorFoundEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
                case (byte)EventCode.FullReset:
                    FullResetEventReceived?.Invoke(this, new PhotonEventArgs { EventData = photonEvent });
                    break;
            }
        }
    }
}
