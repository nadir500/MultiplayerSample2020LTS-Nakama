using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using UnityEngine.UI;


public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private Transform _buttonsGroup;
    [SerializeField] private Button[] _choiceButtons;
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject signupCanvas;

    [Header("Main connection button events ")] [SerializeField]
    private Button _confirmLogin;

    [SerializeField] private Button _confirmSignup;
    private ISession _session;

    private void Start()
    {
        _choiceButtons = new Button[2];
        for (int i = 0; i < 2; i++)
        {
            _choiceButtons[i] = _buttonsGroup.GetChild(i).GetComponent<Button>();
        }

        _choiceButtons[0].onClick.AddListener(LoginChoice);
        _choiceButtons[1].onClick.AddListener(SignupChoice);

        _confirmLogin.onClick.AddListener(ConfirmLogin);
        _confirmSignup.onClick.AddListener(ConfirmSignup);
    }

    public async void ConfirmLogin()
    {
        //session on this machine 
        InputField[] inputFields = loginCanvas.GetComponentsInChildren<InputField>();
        if (!string.IsNullOrEmpty(inputFields[0].text) && !string.IsNullOrEmpty(inputFields[0].text))
        {
            _session = await NakamaNetworkManager.instance.NakamaClientAuthentication(inputFields[0].text, "",
                inputFields[1].text,
                RequestType.Login);
            loginCanvas.SetActive(false);
            Debug.LogFormat("New user: {0}, {1}", _session.Created, _session);
        }
        else
        {
            //send warning 
        }
    }

    public async void ConfirmSignup()
    {
        InputField[] inputFields = signupCanvas.GetComponentsInChildren<InputField>();
        if (!string.IsNullOrEmpty(inputFields[0].text) && !string.IsNullOrEmpty(inputFields[0].text))
        {
            _session = await NakamaNetworkManager.instance.NakamaClientAuthentication(inputFields[0].text, "",
                inputFields[1].text,
                RequestType.Signup);
            signupCanvas.SetActive(false);
            Debug.LogFormat("New user: {0}, {1}", _session.Created, _session);
        }
        else
        {
            //send warning 
        }
    }

    private void LoginChoice()
    {
        signupCanvas.SetActive(false);
        loginCanvas.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            _choiceButtons[i].gameObject.SetActive(false);
        }
    }

    private void SignupChoice()
    {
        signupCanvas.SetActive(true);
        loginCanvas.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            _choiceButtons[i].gameObject.SetActive(false);
        }
    }
}