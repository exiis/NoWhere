using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSettingHandler :  SettingHandler
{
    CharController_Motor charController;
    Camera charCam;

    SettingHandler SettingTop;
    enum settingState { SENSITIVITY = 0, FOV };
    int Sensitivity = 150;
    int FieldOfView = 45;



    protected override void Awake(){
        base.Awake();
        SettingTop = transform.parent.GetComponentInChildren<SettingHandler>();
        UpdateValue();
    }

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
            if(Input.GetKey(KeyCode.RightArrow)){
                PlayerSettingController(true);
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                PlayerSettingController(false);
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                OffSetting();
                SettingTop.OnSetting();
            }
        }
    }

    void IncSensitivity(){
        int maxVal = 500;
        if(Sensitivity < maxVal) Sensitivity++;
    }

    void DecSensitivity(){
        int minVal = 50;
        if(Sensitivity > minVal) Sensitivity--;
    }

    void IncFOV(){
        int maxVal = 80;
        if(FieldOfView < maxVal) FieldOfView++;
    }

    void DecFOV(){
        int minVal = 25;
        if(FieldOfView > minVal) FieldOfView--;
    }

    void PlayerSettingController(bool isInc){
        Debug.Log("call player setting?");
        switch(currentSelect){
            case ((int)settingState.SENSITIVITY):
                if(isInc) IncSensitivity();
                else DecSensitivity();
                break;
            case ((int)settingState.FOV):
                if(isInc) IncFOV();
                else DecFOV();
                break;
        }
        UpdateValue();
    }

    void initPlayerSettings(){
        Sensitivity = 150;
        FieldOfView = 45;
    }

    void UpdateValue(){
        int ContentCount = ContentList.Length;
        for(int currentSelect = 0; currentSelect < ContentCount; currentSelect++){
            TMP_Text Text = ContentList[currentSelect].transform.GetComponentInChildren<TMP_Text>();

            switch(currentSelect){
                case ((int)settingState.SENSITIVITY):
                    Text.text = "화면 이동 : " + Sensitivity;
                    break;
                case ((int)settingState.FOV):
                    Text.text = "시야각 : " + FieldOfView;
                    break;
            }
        }
        charController.sensitivity = Sensitivity;
        charCam.fieldOfView = FieldOfView;
    }
}
