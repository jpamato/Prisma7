using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class Splash : MonoBehaviour
{

    public Button startBtn;
    public Button loginBtn;

    public GameObject login;

    public InputField nombre, pass;
    public Button entrar;


    private void Start()
    {
        if (PlayerPrefs.GetInt("Logged") > 0)
        {
            startBtn.gameObject.SetActive(true);
            loginBtn.gameObject.SetActive(false);
        }
        else
        {
            startBtn.gameObject.SetActive(false);
            loginBtn.gameObject.SetActive(true);
        }

        Permission.RequestUserPermission(Permission.Camera);
    }

    public void SplashClicked()
    {
        //Debug.Log("#ACA");
        Events.ClickSfx();
        Data.Instance.LoadScene("cutscenes");
    }

    public void Reiniciar()
    {
        Data.Instance.Logout();
        loginBtn.gameObject.SetActive(true);
        startBtn.gameObject.SetActive(false);
    }

    public void LoginShow(bool enable)
    {
        login.SetActive(enable);
    }

    public void SendLogin(){
        //StartCoroutine(Data.Instance.usersDB.GetUser(nombre.text,pass.text));
        Data.Instance.usersDB.GetUser(nombre.text);
        entrar.interactable = false;
        //Invoke("OnLogged", 4);
        OnLogged();
    }

    public void OnLogged()
    {
        if (Data.Instance.usersDB.logged)
        {
            startBtn.gameObject.SetActive(true);
            loginBtn.gameObject.SetActive(false);
        }
        else
        {
            startBtn.gameObject.SetActive(false);
            loginBtn.gameObject.SetActive(true);
            nombre.text = "";
            pass.text = "";
            entrar.interactable = true;
        }
        LoginShow(false);
        entrar.interactable = true;
    }

}