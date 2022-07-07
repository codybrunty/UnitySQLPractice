using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAccountManager : Singleton<PlayerAccountManager>{

    [SerializeField] Text usernameText;
    [SerializeField] Text scoreText;
    [SerializeField] private string username;
    [SerializeField] private int score;

    public void SetPlayerAccountData(string username, int score) {
        UpdateUsername(username);
        UpdateScore(score);
    }
    private void UpdateUsername(string username) {
        this.username = username;
        usernameText.text = "Username: " + username;
    }

    public void UpdateScore(int score) {
        this.score = score;
        scoreText.text = "Score: " + score.ToString();
    }

    public string GetUsername() {
        return username;
    }
    public int GetScore() {
        return score;
    }

}
