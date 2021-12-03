using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAuth : MonoBehaviour
{
    public InputField userNameField;
    public InputField passwordField;
    public Text status;
    public GameObject toHide;
    public GameObject toShow;
    
    void Start()
    {
    }

    Dictionary<string, string> staffDetails = new Dictionary<string, string>
    {
        {"admin","admin" },
        {"teacher","teacher" },
        {"student","student" }
    };


    public void tryAuth()
    {
        //Get Username from Input then convert it to int
        string userName = userNameField.text;
        //Get Password from Input 
        string password = passwordField.text;

        string foundPassword;
        if (staffDetails.TryGetValue(userName, out foundPassword) && (foundPassword == password))
        {
            status.text = "Удачно";
            toHide.SetActive(false);
            toShow.SetActive(true);
            Debug.Log("User authenticated");
        }
        else
        {
            status.text = "Неверный пароль";
            Debug.Log("Invalid password");
        }
    }
}
