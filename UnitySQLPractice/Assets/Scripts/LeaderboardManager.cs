using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LeaderboardManager : Singleton<LeaderboardManager>{

    [SerializeField] private GameObject leaderboardUI;
    [SerializeField] private GameObject rowPrefab;
    public LeaderboardData leaderboardData;
    private List<GameObject> rows = new List<GameObject>();

    private void ShowLeaderboardUI() {
        leaderboardUI.SetActive(true);
    }

    public void HideLeaderboardUI() {
        leaderboardUI.SetActive(false);
    }

    public void LeaderboardButtonOnClick() {
        NetworkManager.instance.GetLeaderboard();
        GameplayManager.instance.HideGameUI();
        ShowLeaderboardUI();
    }

    public void LeaderboardBackButtonOnCick() {
        HideLeaderboardUI();
        GameplayManager.instance.ShowGameUI();
    }

    public void LoadLeaderboard(string data) {
        leaderboardData.ClearLeaderboardData();
        JSONNode allDataRows = JSON.Parse(data);
        foreach (JSONNode row in allDataRows) {
            leaderboardData.AddEntry(row[0],row[1]);
        }
        CreateLeaderboardUI();
    }

    private void CreateLeaderboardUI() {
        DestroyLeaderboardUI();
        for (int i = 0; i < leaderboardData.leaderboard.Count; i++) {
            GameObject row = Instantiate(rowPrefab, rowPrefab.transform.parent);
            LeaderboardRowHook hook = row.GetComponent<LeaderboardRowHook>();
            hook.SetLeaderboardRowData(i+1,leaderboardData.leaderboard[i].name, leaderboardData.leaderboard[i].score);
            row.SetActive(true);
            rows.Add(row);
        }
    }
    private void DestroyLeaderboardUI() {
        foreach (GameObject row in rows) {
            Destroy(row);
        }
        rows.Clear();
    }
}


[System.Serializable]
public class LeaderboardData{

    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    public void AddEntry(string username, int score) {
        LeaderboardEntry entry = new LeaderboardEntry();
        entry.name = username;
        entry.score = score;
        leaderboard.Add(entry);
    }
    public void ClearLeaderboardData() {
        leaderboard.Clear();
    }

}

[System.Serializable]
public class LeaderboardEntry {
    public string name;
    public int score;
}
