using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonRoom : MonoBehaviourPunCallbacks
    {
        public static PhotonRoom Instance;
        public static int PlayerNumber { get; private set; }

        private static Player[] _photonPlayers;
        private static int _playersInRoom;
        private const string NumberOfJoins = "numberOfJoins";

        public static void AddCustomProperty(object key, object value)
        {
            var customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            if (customProperties == null)
            {
                customProperties = new Hashtable { { key, value } };
            }
            else if (customProperties[key] == null)
            {
                customProperties.Add(key, value);
            }
            else
            {
                customProperties[key] = value;
            }
            PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            _photonPlayers = PhotonNetwork.PlayerList;
            _playersInRoom = _photonPlayers.Length;
        }

        private void RegisterJoin()
        {
            var numberOfJoins = PhotonNetwork.CurrentRoom.CustomProperties[NumberOfJoins];
            if (numberOfJoins is int joinCount)
                CheckNumberOfJoins(joinCount);
            else
                SetNumberOfRoomJoins(1);
        }

        private void CheckNumberOfJoins(int numberOfJoins)
        {
            if (numberOfJoins >= 2)
                SendBackToBeginningEvent();
            else
                SetNumberOfRoomJoins(numberOfJoins + 1);
        }

        private static void SetNumberOfRoomJoins(int count)
        {
            AddCustomProperty(NumberOfJoins, count);
        }

        [ContextMenu("SendBackToBeginningEvent")]
        public void SendBackToBeginningEvent()
        {
        }

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

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _photonPlayers = PhotonNetwork.PlayerList;
            _playersInRoom = _photonPlayers.Length;
            PlayerNumber = _playersInRoom;
            PhotonNetwork.NickName = PlayerNumber.ToString();
            RegisterJoin();
        }
    }
}
