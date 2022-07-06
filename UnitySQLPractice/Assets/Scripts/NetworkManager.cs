using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkManager : Singleton<NetworkManager>{

    [SerializeField] private Text requestOutputText;
    private bool requesting = false;

    #region Login
    public void Login(string username, string password) {
        StartCoroutine(LoginRequest(username, password));
    }
    IEnumerator LoginRequest(string username, string password) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password. In live app id use the app bundle identifier
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/UnitySQLPractice/loginplayer.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            LoginManager.instance.LoginResponse(req.downloadHandler.text);
        }
        else {
            LoginManager.instance.LoginError(req.error);
        }
        requesting = false;
    }
    #endregion    
    
    #region Create New Player
    public void CreateNewPlayer(string email, string username, string password) {
        StartCoroutine(CreateNewPlayerRequest(email, username, password));
    }
    IEnumerator CreateNewPlayerRequest(string email, string username, string password) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password. In live app id use the app bundle identifier
        form.AddField("email", email);
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/UnitySQLPractice/createnewplayer.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            LoginManager.instance.CreateNewPlayerResponse(req.downloadHandler.text);
        }
        else {
            LoginManager.instance.CreateNewPlayerError(req.error);
        }
        requesting = false;
    }
    #endregion

    #region Save Score

    public void SaveScore(string username, int score) {
        StartCoroutine(SaveScoreRequest(username, score));
    }
    IEnumerator SaveScoreRequest(string username, int score) {
        requesting = true;
        WWWForm form = new WWWForm();
        form.AddField("apppassword", "thisisplaceholderpassword");//Placeholder app password. In live app id use the app bundle identifier
        form.AddField("username", username);
        form.AddField("score", score);
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/UnitySQLPractice/savescore.php/", form);
        yield return req.SendWebRequest();
        if (req.error == null) {
            switch (req.downloadHandler.text) {
                case "1":
                    Debug.LogError("Database Connection Error");
                    break;
                case "2":
                    Debug.LogError("Update Query Failed");
                    break;
                default:
                    PlayerAccountManager.instance.UpdateScore(int.Parse(req.downloadHandler.text.Split(':')[1]));
                    break;
            }
        }
        else {
            Debug.LogError(req.error);
        }
        requesting = false;
    }

    #endregion

    #region Get Requesting Status
    public bool IsRequesting() {
        return requesting;
    }
    #endregion

}
