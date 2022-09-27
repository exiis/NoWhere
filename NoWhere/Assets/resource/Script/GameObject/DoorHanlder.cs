using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHanlder : MonoBehaviour
{
    enum state { NORMAL, OPENING, CLOSING };
    UIController UIcontroller;
    public GameObject door;
    state currentState = state.NORMAL;
    bool onDoor = false;
    bool isOpen = false;
    float openSpeed = 0.7f;
    float originY;

    // Note :
    // Guide UI의 유일 표시 조건은 Trigger Enter/Exit
    private void OnTriggerEnter(Collider col){
        string guidText = isOpen ? "문 닫기 : [ F ]" : "문 열기 : [ F ]";
        onDoor = true;  
        UIcontroller.showGuide(guidText);
    }

    private void OnTriggerExit(Collider col){
        onDoor = false;
        UIcontroller.hideGuide();
    }

    // Note :
    // UpdateDoor()를 Update에서 꾸준히 호출함으로 상태변화 반영
    private void openDoor(){
        isOpen = true;
        door.transform.Rotate(new Vector3(0, openSpeed, 0));
    }

    private void closeDoor(){
        isOpen = false;
        door.transform.Rotate(new Vector3(0, -openSpeed, 0));
    }

    // Note :
    // 열리고 닫는 상태 외의 동작은 수행하지 않는다.
    void updateDoor(){
        if(currentState == state.OPENING){
            openDoor();
            if(door.transform.rotation.eulerAngles.y >= originY + 90) {
                currentState = state.NORMAL;
            }
        }
        else if(currentState == state.CLOSING){
            closeDoor();
            if(door.transform.rotation.eulerAngles.y <= originY) {
                currentState = state.NORMAL;
            }
        }
    }
    
    void Start(){
        UIcontroller = GameObject.Find("UI").GetComponent<UIController>();
        originY = door.transform.rotation.eulerAngles.y;
    }


    // Note :
    // Guide Text는 문열기 키를 누를 시 상호 배타적으로 변경됨
    void Update(){
        if(onDoor){
            if(Input.GetKeyDown(KeyCode.F)){
                if(isOpen) currentState = state.CLOSING;
                else currentState = state.OPENING;
                string guidText = isOpen ? "문 닫기 : [ F ]" : "문 열기 : [ F ]";
                UIcontroller.showGuide(guidText);
            }
        }
        updateDoor();
    }
}
