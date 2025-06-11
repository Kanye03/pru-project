using Managements;
using Unity.Cinemachine;

namespace Misc
{
    public class ScreenShakeManager : Singleton<ScreenShakeManager>
    {
        private CinemachineImpulseSource source;

        protected override void Awake()
        {
            base.Awake();

            source = GetComponent<CinemachineImpulseSource>();
        }

        public void ShakeScreen()
        {
            source.GenerateImpulse();
        }
    }
}
