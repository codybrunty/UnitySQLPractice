using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : Singleton<NetworkManager>{

    private bool requesting = false;
    [SerializeField] private bool isLocal = true;
    private string localhost = "http://localhost/UnitySQLPractice/";
    private string aws = "https://database.codybrunty.com/UnitySQLPractice/";

    #region Login
    public void Login(string username, string password) {
        StartCoroutine(LoginRequest(username, password));
    }
    IEnumerator LoginRequest(string username, string password) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password.
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest req = UnityWebRequest.Post(GetServer() +"loginplayer.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            LoginResponse(req.downloadHandler.text);
        }
        else {
            LoginManager.instance.LoginError(req.error);
        }
        requesting = false;
    }

    public void LoginResponse(string response) {
        switch (response) {
            case "1":
                LoginManager.instance.LoginError("Database Connection Error");
                break;
            case "2":
                LoginManager.instance.LoginError("Username Query Failed");
                break;
            case "3":
                LoginManager.instance.LoginError("Username Error");
                break;
            case "4":
                LoginManager.instance.LoginError("Password Error");
                break;
            default:
                LoginManager.instance.LoginSuccess(response);
                break;
        }
    }

    #endregion

    #region Create New Player
    public void CreateNewPlayer(string email, string username, string password) {
        StartCoroutine(CreateNewPlayerRequest(email, username, password));
    }
    IEnumerator CreateNewPlayerRequest(string email, string username, string password) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password.
        form.AddField("email", email);
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest req = UnityWebRequest.Post(GetServer() +"createnewplayer.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            CreateNewPlayerResponse(req.downloadHandler.text);
        }
        else {
            LoginManager.instance.CreateNewPlayerError(req.error);
        }
        requesting = false;
    }
    public void CreateNewPlayerResponse(string response) {
        switch (response) {
            case "1":
                LoginManager.instance.CreateNewPlayerError("Database Connection Error");
                break;
            case "2":
                LoginManager.instance.CreateNewPlayerError("Username Query Failed");
                break;
            case "3":
                LoginManager.instance.CreateNewPlayerError("Username Already Exists");
                break;
            case "4":
                LoginManager.instance.CreateNewPlayerError("Email Query Failed");
                break;
            case "5":
                LoginManager.instance.CreateNewPlayerError("Email Already Exists");
                break;
            case "6":
                LoginManager.instance.CreateNewPlayerError("Insert Query Failed");
                break;
            default:
                LoginManager.instance.CreateNewPlayerSuccess();
                break;
        }
    }
    #endregion

    #region Save Score

    public void SaveScore(string username, int score) {
        StartCoroutine(SaveScoreRequest(username, score));
    }
    IEnumerator SaveScoreRequest(string username, int score) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password.
        form.AddField("username", username);
        form.AddField("score", score);
        UnityWebRequest req = UnityWebRequest.Post(GetServer()+"savescore.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            SaveScoreRespone(req.downloadHandler.text);
        }
        else {
            Debug.LogError(req.error);
        }
        requesting = false;
    }

    private void SaveScoreRespone(string response) {
        switch (response) {
            case "1":
                Debug.LogError("Database Connection Error");
                break;
            case "2":
                Debug.LogError("Update Query Failed");
                break;
            default:
                PlayerAccountManager.instance.UpdateScore(int.Parse(response.Split(':')[1]));
                break;
        }
    }

    #endregion

    #region Get Leaderboards
    public void GetLeaderboard() {
        StartCoroutine(GetLeaderboardRequest());
    }
    IEnumerator GetLeaderboardRequest() {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password.
        UnityWebRequest req = UnityWebRequest.Post(GetServer()+"getleaderboard.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            GetLeaderboardResponse(req.downloadHandler.text);
        }
        else {
            Debug.LogError(req.error);
        }
        requesting = false;
    }

    private void GetLeaderboardResponse(string response) {
        switch (response) {
            case "1":
                Debug.LogError("Database Connection Error");
                break;
            case "2":
                Debug.LogError("Leaderboard Table Query Failed");
                break;
            case "3":
                Debug.LogError("Leaderboard Empty");
                break;
            default:
                LeaderboardManager.instance.LoadLeaderboard(response);
                break;
        }
    }
    #endregion

    #region Get Requesting Status
    public bool IsRequesting() {
        return requesting;
    }
    #endregion

    #region Get Server
    private string GetServer() {
        string server = "";
        if (isLocal) {
            server = localhost;
        }
        else {
            server = aws;
        }
        return server;
    }
    #endregion

}
