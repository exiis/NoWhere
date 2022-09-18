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
    private void OnTriggerEnter(Collider col){
        string guidText = isOpen ? "문 닫기 : [ F ]" : "문 열기 : [ F ]";
        onDoor = true;  
        UIcontroller.showGuide(guidText);
    }

    private void OnTriggerExit(Collider col){
        onDoor = false;
        UIcontroller.hideGuide();
    }

    private void openDoor(){
        isOpen = true;
        door.transform.Rotate(new Vector3(0, openSpeed, 0));
        string guidText = isOpen ? "문 닫기 : [ F ]" : "문 열기 : [ F ]";
        UIcontroller.showGuide(guidText);
    }

    private void closeDoor(){
        isOpen = false;
        door.transform.Rotate(new Vector3(0, -openSpeed, 0));
        string guidText = isOpen ? "문 닫기 : [ F ]" : "문 열기 : [ F ]";
        UIcontroller.showGuide(guidText);
    }

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

    void Update(){
        if(onDoor){
            if(Input.GetKeyDown(KeyCode.F)){
                if(isOpen) currentState = state.CLOSING;
                else currentState = state.OPENING;
            }
        }
        updateDoor();
    }
}
