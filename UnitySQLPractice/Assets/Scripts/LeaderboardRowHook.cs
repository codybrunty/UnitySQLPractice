using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRowHook : MonoBehaviour{

    [SerializeField] Text rankText;
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;

    public void SetLeaderboardRowData(int rank, string username, int score) {
        rankText.text = rank.ToString()+".";
        nameText.text = username;
        scoreText.text = score.ToString();
    }

}
