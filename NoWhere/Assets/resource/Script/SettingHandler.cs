using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingHandler : MonoBehaviour
{
    VerticalLayoutGroup layout;
    public GameObject[] ContentList;
    public GameObject[] ChildList;
    public GameObject ParentMenu;
    protected int currentSelect = 0;
    protected bool onSetting = false;

    // NOTE :
    // 깨어날 때 초기화, 비활성화
    protected virtual void Awake(){
        layout = gameObject.GetComponent<VerticalLayoutGroup>();
        setSize();
        initContentsColor();
        onSetting = false;
        gameObject.SetActive(false);
    }

    // NOTE :
    // child 숫자에 따라 동적으로 scroll size 조절
    void setSize(){
        float baseHeight = layout.padding.bottom + layout.padding.top;
        float space = layout.spacing;
        float ContentHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        int ContentCount = ContentList.Length;

        float defaultWidth = transform.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(defaultWidth, baseHeight + (ContentCount-1)*space + ContentHeight*ContentCount);
        Debug.Log("new size is " + newSize);
        this.GetComponent<RectTransform>().sizeDelta = newSize;
    }

    // NOTE :
    // Key up/down 통하여 child setting 선택, Enter키로 Setting 진입
    // Esc 통하여 상위 메뉴로 이동
    void Update(){
        if(onSetting){
            if(Input.GetKeyUp(KeyCode.UpArrow)){
                if(currentSelect == 0) return;
                setCurrentContentColor(false);
                currentSelect--;
                setCurrentContentColor(true);
            }   
            if(Input.GetKeyUp(KeyCode.DownArrow)){
                if(currentSelect == ContentList.Length-1) return;
                setCurrentContentColor(false);
                currentSelect++;
                setCurrentContentColor(true);
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                ChildList[currentSelect].SetActive(true);
                ChildList[currentSelect].GetComponent<SettingHandler>().OnSetting();
                OffSetting();
            }
            if(Input.GetKeyDown(KeyCode.Escape)) {
                OffSetting();
                EscapeCallBack();
            }
        }
    }

    // NOTE :
    // 현재 선택된 메뉴를 시각적으로 표현
    protected void setCurrentContentColor(bool selected){
        if(selected){
            TMP_Text text = ContentList[currentSelect].transform.GetComponentInChildren<TMP_Text>();
            text.color = new Color(1, 0, 0, 1);
        }
        else{
            TMP_Text text = ContentList[currentSelect].transform.GetComponentInChildren<TMP_Text>();
            text.color = new Color(1, 1, 1, 1);
        }
    }

    // NOTE :
    // 색깔 초기화
    void initContentsColor(){
        int contentsNum = ContentList.Length;
        setCurrentContentColor(true);
        for(int i=1; i<contentsNum; i++){
            currentSelect = i;
            setCurrentContentColor(false);
        }
        currentSelect = 0;
    }

    // NOTE :
    // 기본적으로 setting 열리면 첫번째 메뉴 선택
    public void OnSetting(){
        onSetting = true;
        gameObject.SetActive(true);
        currentSelect = 0;
    }

    public void OffSetting(){
        onSetting = false;
        gameObject.SetActive(false);
    }

    public void EscapeCallBack(){
        // 최상위 메뉴
        if(ParentMenu.GetComponent<SettingHandler>() == null){
            ParentMenu.SetActive(false);
            GameObject.Find("UI").GetComponent<UIController>().OffSettingUI();
        }
        else{
            ParentMenu.SetActive(true);
            ParentMenu.GetComponent<SettingHandler>().OnSetting();
        }
    }
}
