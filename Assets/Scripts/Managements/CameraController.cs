using Player;
using Unity.Cinemachine;

namespace Managements
{
    public class CameraController : Singleton<CameraController>
    {
        private CinemachineCamera cinemachineCamera;
        private void Start()
        {
            SetPlayerCameraFollow ();
        }

        public void SetPlayerCameraFollow() {
            cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
            cinemachineCamera.Follow = PlayerController.Instance.transform;
        }
    }
}
