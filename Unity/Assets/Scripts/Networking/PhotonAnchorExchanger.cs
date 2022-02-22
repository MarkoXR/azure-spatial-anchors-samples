using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class PhotonAnchorExchanger : MonoBehaviour, IOnEventCallback
{
    public static PhotonAnchorExchanger Instance { get; set; }
    public string AzureAnchorId { get; set; } = "";

    public EventHandler AzureAnchorCreated;

    public enum EventCode : byte
    {
        AzureAnchorCreated
    }

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

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public static void SendAzureAnchorCreatedEvent(string anchorId)
    {
        var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent((byte)EventCode.AzureAnchorCreated, anchorId, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        var eventCode = photonEvent.Code;

        switch (eventCode)
        {
            case (byte)EventCode.AzureAnchorCreated:
                AzureAnchorId = (string)photonEvent.CustomData;
                AzureAnchorCreated?.Invoke(this, EventArgs.Empty);
                break;
        }
    }
}
