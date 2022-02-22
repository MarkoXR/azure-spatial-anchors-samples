using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace Assets.Scripts.Networking
{
    public class PhotonEventSender
    {
        public static void SendCreatePatternEvent(int[] sendData)
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCode.CreatePattern, sendData, raiseEventOptions, SendOptions.SendReliable);
        }

        public static void SendPairingEndedEvent(bool pairingAccepted)
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCode.PairingEnded, pairingAccepted, raiseEventOptions,
                SendOptions.SendReliable);
        }

        public static void SendResetEvent()
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCode.Reset, null, raiseEventOptions, SendOptions.SendReliable);
        }

        public static void SendBackToBeginningEvent()
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCode.StartOver, null, raiseEventOptions,
                SendOptions.SendReliable);
        }

        public static void SendAnchorCreatedEvent()
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent((byte)EventCode.AnchorCreated, null, raiseEventOptions,
                SendOptions.SendReliable);
        }

        public static void SendAnchorFoundEvent()
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent((byte)EventCode.AnchorFound, null, raiseEventOptions,
                SendOptions.SendReliable);
        }
        public static void SendFullResetEvent()
        {
            var raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCode.FullReset, null, raiseEventOptions,
                SendOptions.SendReliable);
        }
    }
}
