using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managements; // Để dùng EnemyManager

namespace Managements
{
    public class AreaExit : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private string sceneTransitionName;

        private float waitToLoadTime = 1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                // Kiểm tra nếu đã diệt hết quái
                if (EnemyManager.Instance != null && EnemyManager.Instance.AllEnemiesDefeated())
                {
                    SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                    UIFade.Instance.FadeToBlack();
                    StartCoroutine(LoadSceneRoutine());
                }
                else
                {
                    Debug.Log("Khong The Qua Man: con quai vat con song.");
                    // Tuỳ chọn: thêm hiệu ứng âm thanh hoặc UI cảnh báo
                }
            }
        }

        private IEnumerator LoadSceneRoutine()
        {
            while (waitToLoadTime >= 0)
            {
                waitToLoadTime -= Time.deltaTime;
                yield return null;
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
