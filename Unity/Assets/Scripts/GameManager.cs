using Assets.Scripts;
using Assets.Scripts.Networking;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AzureAnchorController _azureAnchorController;
    [SerializeField] private GameObject _ux;

    private void Awake()
    {
        PhotonEventReceiver.Instance.AnchorCreatedEventReceived += OnAnchorCreated; 
    }

    private void OnAnchorCreated(object sender, PhotonEventReceiver.PhotonEventArgs e)
    {
        SynchronizeSpaces();
    }

    public void SynchronizeSpaces()
    {
        _ux.SetActive(false);
        _azureAnchorController.CreateOrFindAnchor();
    }
}
