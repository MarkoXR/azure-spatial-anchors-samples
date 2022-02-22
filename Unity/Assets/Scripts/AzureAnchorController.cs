using System;
using Assets.Scripts.Networking;
using Assets.Scripts.Networking.MRTK_Turtorial;
using MRTK.Tutorials.MultiUserCapabilities;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class AzureAnchorController : MonoBehaviour
    {
        [SerializeField]
        private AnchorModuleScript _anchorModule;

        public EventHandler AnchorLocated;

        public const string AnchorIdKey = "anchorId";

        private void Awake()
        {
            _anchorModule.AzureSessionStarted += OnAzureSessionStarted;
            _anchorModule.OnCreateAnchorSucceeded += OnCreateAnchorSucceeded;
            _anchorModule.AnchorLocated += OnFindASAAnchor;
            _anchorModule.AzureSessionStartFailed += OnAzureSessionStartFailed;
        }

        public void CreateOrFindAnchor()
        {
            _anchorModule.StartAzureSession();
        }

        public string GetRoomAnchorId()
        {
            return (string)PhotonNetwork.CurrentRoom.CustomProperties[AnchorIdKey];
        }

        private void OnAzureSessionStartFailed(object sender, EventArgs e)
        {
            Debug.Log("Failed");
        }

        private void OnAzureSessionStarted(object sender, EventArgs e)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _anchorModule.CreateAzureAnchor(_anchorModule.gameObject);
            }
            else
            {
                var anchorId = (string)PhotonNetwork.CurrentRoom.CustomProperties[AnchorIdKey];
                _anchorModule.FindAzureAnchor(anchorId);
            }
        }

        private void OnCreateAnchorSucceeded()
        {
            AddAnchorIdToRoomProperties();
            PhotonEventSender.SendAnchorCreatedEvent();
        }

        private void AddAnchorIdToRoomProperties()
        {
            PhotonRoom.AddCustomProperty(AnchorIdKey, _anchorModule.currentAzureAnchorID);
        }

        private void OnFindASAAnchor(object sender, EventArgs e)
        {
            PhotonEventSender.SendAnchorFoundEvent();
            AnchorLocated?.Invoke(this, EventArgs.Empty);
        }
    }
}