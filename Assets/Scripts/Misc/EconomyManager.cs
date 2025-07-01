using Managements;
using TMPro;
using UnityEngine;

namespace Misc
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        private TMP_Text goldText;
        private TMP_Text highscoreText;
        private int currentGold = 0;
        private int highScore = 0;

        const string COIN_AMOUNT_TEXT = "Gold Amount Text";
        const string HIGH_SCORE_TEXT = "High Score Text";

        private void Start()
        {
            // Load high score từ PlayerPrefs (hoặc mặc định là 0)
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            // Tìm text UI
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
            highscoreText = GameObject.Find(HIGH_SCORE_TEXT).GetComponent<TMP_Text>();

            UpdateUI();
        }

        public void UpdateCurrentGold()
        {
            currentGold += 1;

            // Cập nhật high score nếu cần
            if (currentGold > highScore)
            {
                highScore = currentGold;
                PlayerPrefs.SetInt("HighScore", highScore); // Lưu lại
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (goldText != null)
                goldText.text = currentGold.ToString("D3");

            if (highscoreText != null)
                highscoreText.text = "High Score: " + highScore.ToString("D3");
        }
        public void ResetCurrentGold()
        {
            currentGold = 0;
            UpdateUI();
        }
    }
}
