using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallJava : MonoBehaviour
{
    AndroidJavaClass javaClass = null;
    AndroidJavaObject javaObject = null;
    public InputField logInput;
    public InputField nameInput;
    public InputField getLogInput;
    public InputField getNameInput;
    // Start is called before the first frame update
    void Start()
    {
        javaClass = new AndroidJavaClass("com.example.testunity.Test");
        // if call unstatic function, have to use AndroidJavaObject
        javaObject = new AndroidJavaObject("com.example.testunity.Test");
    }

    public void SetLog(){
        // call funciton
        javaClass.CallStatic("setLOG",logInput.text);
    }
    public void SetLogField(){
        javaClass.SetStatic("LOG",logInput.text);
    }

    public void SetName(){
        // call funciton
        javaObject.Call("setName",nameInput.text);
    }
    public void SetNameField(){
        javaObject.Set("name",nameInput.text);
    }

    public void GetLog(){
        string log = javaClass.CallStatic<string>("getLOG");
        getLogInput.text = log;
    }
    public void GetLogField(){
        string log = javaClass.GetStatic<string>("LOG");
        getLogInput.text = log;
    }

    public void GetNAME(){
        string name = javaObject.Call<string>("getName");
        getNameInput.text = name;
    }
    public void GetNAMEField(){
        string name = javaObject.Get<string>("name");
        getNameInput.text = name;
    }
}
