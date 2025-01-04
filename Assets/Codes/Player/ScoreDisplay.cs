using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Total XP Text
    public TextMeshProUGUI levelText;  // Level Text
    private CharacterStatus playerStatus;

    void Start()
    {
        playerStatus = FindObjectOfType<CharacterStatus>();
        UpdateScoreText();  // Ensure initial update
    }

    void Update()
    {
        UpdateScoreText();  // Keep updating in real-time (or move this to an event)
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
