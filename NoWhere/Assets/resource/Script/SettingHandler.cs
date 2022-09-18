using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingHandler : MonoBehaviour
{
    VerticalLayoutGroup layout;
    public GameObject[] ContentList;
    int currentSelect = 0;
    bool OnSetting = true;
    void Awake(){
        layout = gameObject.GetComponent<VerticalLayoutGroup>();
        setSize();
        initContentsColor();
        if(!OnSetting) gameObject.SetActive(false);
    }

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

    void Update(){
        if(OnSetting){
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
        }
    }

    void setCurrentContentColor(bool selected){
        if(selected){
            TMP_Text text = ContentList[currentSelect].transform.GetComponentInChildren<TMP_Text>();
            text.color = new Color(1, 0, 0, 1);
        }
        else{
            TMP_Text text = ContentList[currentSelect].transform.GetComponentInChildren<TMP_Text>();
            text.color = new Color(1, 1, 1, 1);
        }
    }

    void initContentsColor(){
        int contentsNum = ContentList.Length;
        setCurrentContentColor(true);
        for(int i=1; i<contentsNum; i++){
            currentSelect = i;
            setCurrentContentColor(false);
        }
        currentSelect = 0;
    }
}
