using UnityEngine;
using TMPro;

namespace UI
{
    public class UIManager : SingletonGeneric<UIManager>
    {
        public GameObject achievementPanel;
        public TextMeshProUGUI achievementText;
        public TextMeshProUGUI achievementNameText;
        public TextMeshProUGUI achievementInfoText;
        public TextMeshProUGUI waveTxt;
        public TextMeshProUGUI scoreTxt;
        private int waveNumber = 1;
        private int scoreNumber = 0;

        async public void showAchievement(string achievement, string achievementInfo)
        {
            achievementPanel.SetActive(true);
            achievementText.text = "ACHIEVEMENT UNLOCKED !";
            achievementNameText.text = achievement;
            achievementInfoText.text = achievementInfo;
            await new WaitForSeconds(3f);
            achievementPanel.SetActive(false);
        }

        public async void showWaves()
        {
            float fontSize = waveTxt.fontSize;
            waveNumber++;
            await new WaitForSeconds(0.05f);
            waveTxt.fontSize = fontSize * 1.3f;
            await new WaitForSeconds(0.05f);
            waveTxt.text = waveNumber.ToString();
            await new WaitForSeconds(0.05f);
            waveTxt.fontSize = fontSize;
        }

        public async void showScore()
        {
            float fontSize = scoreTxt.fontSize;
            scoreNumber += 10;
            await new WaitForSeconds(0.02f);
            scoreTxt.fontSize = fontSize * 1.3f;
            await new WaitForSeconds(0.02f);
            scoreTxt.text = scoreNumber.ToString();
            await new WaitForSeconds(0.02f);
            scoreTxt.fontSize = fontSize;
        }
    }
}
