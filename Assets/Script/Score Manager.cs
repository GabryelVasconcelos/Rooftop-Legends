using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private float pointsPerSecond = 10f;

    private float score = 0f;

    void Update()
    {
        if (playerMovement == null || playerMovement.morreu)
        {
            return;
        }

        score += pointsPerSecond * Time.deltaTime;

        if (scoreTxt != null)
        {
            scoreTxt.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}