using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 6;
    public int score = 0;
    public int winScore = 20;
    public int pointsPerMatch = 2;

    public void CircleHitGround()
    {
        lives--;

        if (lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score >= winScore)
        {
            Debug.Log("You Win");
        }
    }
    public void AddMatchScore()
    {
        score += pointsPerMatch;
        Debug.Log("Score: " + score);  
        if (score >= winScore)
        {
            Debug.Log("success");
        }
    }
}