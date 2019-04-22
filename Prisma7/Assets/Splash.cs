using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void SplashClicked()
    {
        //Debug.Log("#ACA");
        Events.ClickSfx();
        Data.Instance.LoadScene("cutscenes");
    }

    public void Reiniciar()
    {
        Data.Instance.Reset();        
    }

    public void LoginShow(bool enable)
    {
        login.SetActive(enable);
    }

    public void SendLogin(){
        StartCoroutine(Data.Instance.usersDB.GetUser(nombre.text,pass.text));
        entrar.interactable = false;
        Invoke("OnLogged", 4);
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
    }

}