using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject Message;
    
    private float timer = 0.0f;
    private float timerFlag = false;

    public void SendMessage(string text, MessageSendMode.mode mode, float time){
        switch(mode){
            // fade in / fade out 형태로 메세지 UI 노출
            case MessageSendMode.mode.fade:
                timer = time;
                Message.GetComponent<TMP_Text>().text = text;
                break;

            default :
                Debug.LogError("[Error] Send Message Failed.");
                break;
        }
    }

    // Unity Update
    void Update() {
        if(timerFlag){
            timer += Time.DeltaTime;
            
        }
    }

    


}
