using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Singleton<GameplayManager>{

    [SerializeField] private GameObject gameUI;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private int currentScore = 0;

    public void StartNewGame() {
        currentScore = 0;
        currentScoreText.text = currentScore.ToString();
    }

    public void ShowGameUI() {
        gameUI.SetActive(true);
    }
    public void HideGameUI() {
        gameUI.SetActive(false);
    }

    public void IncreaseButtonOnClick() {
        currentScore++;
        currentScoreText.text = currentScore.ToString();
    }

    public void SaveButtonOnClick() {
        if (NetworkManager.instance.IsRequesting()) { return; }
        if(currentScore > PlayerAccountManager.instance.GetScore()) {
            NetworkManager.instance.SaveScore(PlayerAccountManager.instance.GetUsername(), currentScore);
        }
        StartNewGame();
    }
  
}
