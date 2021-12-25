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

        async public void ShowAchievement(string achievement, string achievementInfo)
        {
            achievementPanel.SetActive(true);
            achievementText.text = "ACHIEVEMENT UNLOCKED";
            achievementNameText.text = achievement;
            achievementInfoText.text = achievementInfo;
            await new WaitForSeconds(3f);
            achievementPanel.SetActive(false);
        }
    }
}
