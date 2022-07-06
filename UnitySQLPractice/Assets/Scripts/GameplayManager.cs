using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Singleton<GameplayManager>{

    [SerializeField] private Button increaseButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private int currentScore = 0;

    public void StartNewGame() {
        currentScore = 0;
        currentScoreText.text = currentScore.ToString();
    }

    public void ShowGameUI() {
        currentScoreText.gameObject.SetActive(true);
        increaseButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
    }
    public void HideGameUI() {
        currentScoreText.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
    }

    public void IncreaseButtonOnClick() {
        currentScore++;
        currentScoreText.text = currentScore.ToString();
    }

    public void SaveButtonOnClick() {
        if(currentScore > PlayerAccountManager.instance.GetScore()) {
            NetworkManager.instance.SaveScore(PlayerAccountManager.instance.GetUsername(), currentScore);
        }
        StartNewGame();
    }
  
}
