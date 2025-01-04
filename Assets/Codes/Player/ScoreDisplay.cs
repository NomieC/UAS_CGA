using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Total XP Text
    public TextMeshProUGUI levelText;     // Level Text

    private CharacterStatus playerStatus;

    void Start()
    {
        playerStatus = FindObjectOfType<CharacterStatus>();  // Find the player status script
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (playerStatus != null)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + playerStatus.totalExperience.ToString("0");
            }
            
            if (levelText != null)
            {
                levelText.text = "Level: " + playerStatus.level.ToString("0");
            }
        }
    }
}
