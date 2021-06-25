using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Nakama;

public enum RequestType
{
    Login,
    Signup
}

public class NakamaNetworkManager : MonoBehaviour
{
    //for ip address we can fetch from secure connection later than hard coding it 
    [SerializeField] private string _ipAddress;
    [SerializeField] private string _protocolKey;
    [SerializeField] private string _serverKey;
    [SerializeField] private int _port = 7350;
    public static NakamaNetworkManager instance;

    private void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public async Task<ISession> NakamaClientAuthentication(string email, string username, string password,
        RequestType type) //email, password, username 
    {
        //create new login session to the server by providing server info 
        Client client = new Client(_protocolKey, _ipAddress, _port, _serverKey);
        ISession session = null;
        if (type == RequestType.Login)
        {
            session = await client.AuthenticateEmailAsync(email, password);
        }
        else
        {
            if (type == RequestType.Signup)
            {
                session = await client.AuthenticateEmailAsync(email, password, username, true);
            }
        }

        Debug.LogFormat("New user: {0}, {1}", session.Created, session);
        return session;
    }
}