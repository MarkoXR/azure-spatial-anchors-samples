using UnityEngine;

namespace Assets.Scripts.Networking.MRTK_Turtorial
{
    public class MultiplayerAnchor : MonoBehaviour
    {
        public static MultiplayerAnchor Instance { get; set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance == this) return;
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }
    }
}
