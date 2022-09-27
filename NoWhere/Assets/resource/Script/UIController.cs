using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    /*──────────────────────────────────── Start Message UI ────────────────────────────────────*/
    #region Message UI  
    /* 
    [ Note ]
    - 스크립트 수행 조건 : 
        Message State를 기준으로 messageUpdate문에서 메세지 UI의 상태 관리 및 message 스크립트 수행
        (효과 추가 시 messageUpdate문에 분기 추가 및 MessageState 추가 )
    - 외부 접근 :
        SendMessage 메서드를 활용하여 메세지 표시
    - Message UI 실행 조건 :
        onSendingMessage를 통하여 Message UI에 대한 처리 Update문에서 실행할지 결정
    - fade 효과 수정 : 
        fadeInTime 및 fadeOutTime을 변경하여 fade 효과 수정 가능
    */

    /* Message base */
    enum MessageState { OFF, FADE_IN, FADE_NORMAL, FADE_OUT };
    public GameObject Message;
    MessageState messageState = MessageState.OFF;
    private bool onSendingMessage = false;
    float messageTime = 0.0f;
    float messageTimer = 0.0f;

    /* fade var */
    float fadeInTime = 0.3f;
    float fadeOutTime = 0.7f;

    public void SendMessage(string text_, MessageSendMode.mode mode_, float time_)
    {
        switch (mode_)
        {
            // fade in / fade out 형태로 메세지 UI 노출
            case MessageSendMode.mode.fade:
                messageState = MessageState.FADE_IN;
                onSendingMessage = true;
                messageTime = time_;
                messageTimer = 0.0f;
                Message.GetComponentInChildren<TMP_Text>().text = text_;
                break;

            default:
                Debug.LogError("[Error] Send Message Failed.");
                break;
        }
    }

    void FadeInMessage()
    {
        messageState = MessageState.FADE_IN;
        Color messageColor = Message.GetComponent<Image>().color;
        Color textColor = Message.GetComponentInChildren<TMP_Text>().color;
        float val = (messageTimer / messageTime) / fadeInTime;

        Message.GetComponent<Image>().color = new Color(0f, 0f, 0f, val);
        Message.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, val);
    }

    void FadeOutMessage()
    {
        messageState = MessageState.FADE_OUT;
        Color messageColor = Message.GetComponent<Image>().color;
        Color textColor = Message.GetComponentInChildren<TMP_Text>().color;
        float val = 1 - ((messageTimer / messageTime) - fadeOutTime) / fadeInTime;

        Message.GetComponent<Image>().color = new Color(0f, 0f, 0f, val);
        Message.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, val);
    }

    void NormalMessage()
    {
        messageState = MessageState.FADE_NORMAL;
        Color messageColor = Message.GetComponent<Image>().color;
        Color textColor = Message.GetComponentInChildren<TMP_Text>().color;
        float val = 1f;

        Message.GetComponent<Image>().color = new Color(0f, 0f, 0f, val);
        Message.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, val);
    }

    void OffMessage()
    {
        messageState = MessageState.OFF;
        Color messageColor = Message.GetComponent<Image>().color;
        Color textColor = Message.GetComponentInChildren<TMP_Text>().color;
        float val = 0f;

        Message.GetComponent<Image>().color = new Color(0f, 0f, 0f, val);
        Message.GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, val);

        onSendingMessage = false;
    }

    void messageUpdate()
    {
        /* fade handling */
        if (messageState == MessageState.FADE_IN)
        {
            FadeInMessage();
            if (messageTimer / messageTime > fadeInTime) messageState = MessageState.FADE_NORMAL;
        }
        else if (messageState == MessageState.FADE_NORMAL)
        {
            NormalMessage();
            if (messageTimer / messageTime > fadeOutTime) messageState = MessageState.FADE_OUT;
        }
        else if (messageState == MessageState.FADE_OUT)
        {
            FadeOutMessage();
            if (messageTimer / messageTime > 1.0f) messageState = MessageState.OFF;
        }
        else if (messageState == MessageState.OFF)
        {
            OffMessage();
        }
    }
    #endregion
    /*───────────────────────────────────── End Message UI ─────────────────────────────────────*/


    /*──────────────────────────────────── Guide UI ──────────────────────────────────────*/
    #region Guide UI
    /* [Note]
    showGuid, hideGuide 호출하여 간단하게 Guide UI 제어 가능
    */
    public GameObject Guide;


    public void showGuide(string text_)
    {
        if(!Guide.activeSelf) {
            Guide.SetActive(true);
            Debug.Log("Guide on");
        }
        TMP_Text guidText = Guide.GetComponent<TMP_Text>();
        guidText.text = text_;

        guidText.color = new Color(1, 1, 1, 1);
    }

    public void hideGuide()
    {
        TMP_Text guidText = Guide.GetComponent<TMP_Text>();
        guidText.text = "";

        guidText.color = new Color(1, 1, 1, 0);
        if(Guide.activeSelf) {
            Debug.Log("Guide off");
            Guide.SetActive(false);
        }
    }
    #endregion
    /*─────────────────────────────────── End Guide UI ───────────────────────────────────*/


    /*──────────────────────────────────── Letter UI ──────────────────────────────────────*/
    #region Letter UI
    enum LetterState { OFF, FADE_IN, FADE_NORMAL, FADE_OUT };
    public GameObject Letter;
    LetterState letterState = LetterState.OFF;

    void showLetter(string text, bool fadeText)
    {
        Letter.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.GetComponentInChildren<TMP_Text>().text = text;

        if (fadeText) {
            
        }
        else {
            transform.GetComponentInChildren<TMP_Text>().color = new Color(0, 0, 0, 1);
        }
    }

    void hideLetter()
    {
        Letter.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        transform.GetComponentInChildren<TMP_Text>().color = new Color(0, 0, 0, 0);
        transform.GetComponentInChildren<TMP_Text>().text = "";

        letterState = LetterState.OFF;
    }

    #endregion
    /*────────────────────────────────── End Letter UI ──────────────────────────────────────*/


    /*──────────────────────────────────  Setting UI ──────────────────────────────────────*/
    #region Setting UI
    public GameObject SettingTop;
    public GameObject Menu;
    bool onSetting = false;
    public void OnSettingUI(){
        SettingHandler TopHandler = SettingTop.GetComponent<SettingHandler>();
        TopHandler.OnSetting();
        Menu.SetActive(true);
    }
    public void OffSettingUI(){
        Debug.Log("Off called");
        onSetting = false;
        Menu.SetActive(false);
    }
    #endregion
    /*───────────────────────────────── End Setting UI ────────────────────────────────────*/


    #region Unity Function
    void Awake(){
        OffSettingUI();
    }

    void Update(){
        if (onSendingMessage){
            messageTimer += Time.deltaTime;
            messageUpdate();
        }

        if(!onSetting && Input.GetKeyDown(KeyCode.Escape)){
            onSetting = true;
            OnSettingUI();
            Debug.Log("On called");
            Time.timeScale = 0;
            return;
        }
    }
    #endregion
}
