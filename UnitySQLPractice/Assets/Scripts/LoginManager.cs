using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : Singleton<LoginManager>{

    [SerializeField] private Button goToLoginButton;
    [SerializeField] private Button goToCreateNewPlayerButton;
    [SerializeField] private InputField emailInputText;
    [SerializeField] private InputField usernameInputText;
    [SerializeField] private InputField passwordInputText;
    [SerializeField] private Button submitLoginButton;
    [SerializeField] private Button submitCreateNewPlayerButton;
    [SerializeField] private Button goToBackButton;
    [SerializeField] private Text submitLoginButtonErrorText;
    [SerializeField] private Text submitCreateNewPlayerButtonErrorText;
    [SerializeField] private Button logoutButton;

    #region Go To
    public void GoToLogin() {
        goToLoginButton.gameObject.SetActive(false);
        goToCreateNewPlayerButton.gameObject.SetActive(false);
        emailInputText.gameObject.SetActive(false);
        usernameInputText.gameObject.SetActive(true);
        passwordInputText.gameObject.SetActive(true);
        submitLoginButton.gameObject.SetActive(true);
        submitCreateNewPlayerButton.gameObject.SetActive(false);
        goToBackButton.gameObject.SetActive(true);
        logoutButton.gameObject.SetActive(false);
        GameplayManager.instance.HideGameUI();
    }
    public void GoToCreateNewPlayer() {
        goToLoginButton.gameObject.SetActive(false);
        goToCreateNewPlayerButton.gameObject.SetActive(false);
        emailInputText.gameObject.SetActive(true);
        usernameInputText.gameObject.SetActive(true);
        passwordInputText.gameObject.SetActive(true);
        submitLoginButton.gameObject.SetActive(false);
        submitCreateNewPlayerButton.gameObject.SetActive(true);
        goToBackButton.gameObject.SetActive(true);
        logoutButton.gameObject.SetActive(false);
        GameplayManager.instance.HideGameUI();
    }
    public void GoToMainMenu() {
        CreateNewPlayerClearError();
        LoginClearError();
        goToLoginButton.gameObject.SetActive(true);
        goToCreateNewPlayerButton.gameObject.SetActive(true);
        emailInputText.gameObject.SetActive(false);
        usernameInputText.gameObject.SetActive(false);
        passwordInputText.gameObject.SetActive(false);
        submitLoginButton.gameObject.SetActive(false);
        submitCreateNewPlayerButton.gameObject.SetActive(false);
        goToBackButton.gameObject.SetActive(false);
        logoutButton.gameObject.SetActive(false);
        GameplayManager.instance.HideGameUI();
    }
    public void GoToGame() {
        goToLoginButton.gameObject.SetActive(false);
        goToCreateNewPlayerButton.gameObject.SetActive(false);
        emailInputText.gameObject.SetActive(false);
        usernameInputText.gameObject.SetActive(false);
        passwordInputText.gameObject.SetActive(false);
        submitLoginButton.gameObject.SetActive(false);
        submitCreateNewPlayerButton.gameObject.SetActive(false);
        goToBackButton.gameObject.SetActive(false);
        logoutButton.gameObject.SetActive(true);
        GameplayManager.instance.ShowGameUI();
    }
    #endregion

    #region Login
    public void SubmitLoginButtonOnClick() {
        if (string.IsNullOrEmpty(usernameInputText.text) || string.IsNullOrEmpty(passwordInputText.text)) { LoginError("NullOrEmpty InputField"); return; }
        if (NetworkManager.instance.IsRequesting()) { return; }
        LoginClearError();
        NetworkManager.instance.Login(usernameInputText.text, passwordInputText.text);
    }

    public void LoginResponse(string response) {
        switch (response) {
            case "1":
                LoginError("Database Connection Error");
                break;
            case "2":
                LoginError("Username Query Failed");
                break;
            case "3":
                LoginError("Username Error");
                break;
            case "4":
                LoginError("Password Error");
                break;
            default:
                LoginSuccess(response);
                break;
        }
    }

    public void LoginSuccess(string response) {
        PlayerAccountManager.instance.SetPlayerAccountData(response.Split(':')[0], int.Parse(response.Split(':')[1]));
        GoToGame();
    }

    public void LoginError(string error) {
        submitLoginButtonErrorText.text = error;
    }

    private void LoginClearError() {
        submitLoginButtonErrorText.text = "";
    }

    #endregion

    #region Create New Player
    public void SubmitCreateNewPlayerOnClick() {
        if (string.IsNullOrEmpty(emailInputText.text) || string.IsNullOrEmpty(usernameInputText.text) || string.IsNullOrEmpty(passwordInputText.text)) { CreateNewPlayerError("NullOrEmpty InputField"); return; }
        if (NetworkManager.instance.IsRequesting()) { return; }
        CreateNewPlayerClearError();
        NetworkManager.instance.CreateNewPlayer(emailInputText.text, usernameInputText.text, passwordInputText.text);
    }

    public void CreateNewPlayerResponse(string response) {
        switch (response) {
            case "1":
                CreateNewPlayerError("Database Connection Error");
                break;
            case "2":
                CreateNewPlayerError("Username Query Failed");
                break;
            case "3":
                CreateNewPlayerError("Username Already Exists");
                break;
            case "4":
                CreateNewPlayerError("Email Query Failed");
                break;
            case "5":
                CreateNewPlayerError("Email Already Exists");
                break;
            case "6":
                CreateNewPlayerError("Insert Query Failed");
                break;
            default:
                CreateNewPlayerSuccess();
                break;
        }
    }

    private void CreateNewPlayerSuccess() {
        NetworkManager.instance.Login(usernameInputText.text, passwordInputText.text);
    }

    public void CreateNewPlayerError(string error) {
        submitCreateNewPlayerButtonErrorText.text = error;
    }

    private void CreateNewPlayerClearError() {
        submitCreateNewPlayerButtonErrorText.text = "";
    }

    #endregion

    #region Log Out
    public void LogOutButtonOnClick() {
        PlayerAccountManager.instance.ClearPlayerAccountData();
        GoToMainMenu();
    }
    #endregion

}
