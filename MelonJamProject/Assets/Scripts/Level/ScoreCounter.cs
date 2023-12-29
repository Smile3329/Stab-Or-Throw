using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public static ScoreCounter instance {get; private set;}
    private int score;
    
    private void Awake() {
        instance = this;
        SetScore(0);
    }

    public void AddScore(int score) {
        this.score += score;
        scoreText.text = "Score: " + this.score.ToString();
    }   

    public void SetScore(int score) {
        this.score = score;
        scoreText.text = "Score: " + this.score.ToString();
    }   
}
