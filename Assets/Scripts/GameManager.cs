using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

     void Awake()
    {
        if (instance == null) instance = this;
    }
    public void IncrementScore(int amount)
    {
        score += amount;
        scoreText.text = $"Coins: {score}";
    }
}