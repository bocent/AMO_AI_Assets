using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button signUpButton;
    public Button forgetButton;

    private Login login;

    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        signUpButton.onClick.AddListener(SignUp);
        forgetButton.onClick.AddListener(ForgetPassword);
        login = GetComponent<Login>();
    }

    private void Login()
    {
        if (Validate())
        {
            StartCoroutine(login.CheckLogin(emailInputField.text, passwordInputField.text, () =>
            {
                CustomSceneManager.Instance.LoadScene("Home", null);
            },
            (error) =>
            {
                PopupManager.Instance.ShowPopupMessage("err", "Gagal Login", error,
                    new ButtonInfo { content = "Ulangi" },
                    new ButtonInfo { content = "Keluar", onButtonClicked = Application.Quit });
            }));
        }
        else
        {
            PopupManager.Instance.ShowPopupMessage("err", "Data Tidak Valid", "Username dan Password harus diisi",
                    new ButtonInfo { content = "Ulangi" });
        }

        //CustomSceneManager.Instance.LoadScene("Home", null);
    }

    private bool Validate()
    {
        return !string.IsNullOrEmpty(emailInputField.text) && !string.IsNullOrEmpty(passwordInputField.text);
    }

    private void SignUp()
    {
        login.ShowRegistrationPage(true);
    }

    private void ForgetPassword()
    {
        login.ShowForgetPassword();
    }
}
