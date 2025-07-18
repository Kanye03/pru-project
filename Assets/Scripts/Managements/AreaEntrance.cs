using Player;
using UnityEngine;

namespace Managements
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] private string transitionName;

        private void Start()
        {
            if (transitionName == SceneManagement.Instance.SceneTransitionName)
            {
                PlayerController.Instance.transform.position = this.transform.position;
                CameraController.Instance.SetPlayerCameraFollow();
                UIFade.Instance.FadeToClear();
            }
        }
    }
}
