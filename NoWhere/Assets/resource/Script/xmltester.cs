using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class xmltester : MonoBehaviour
{
    public UIController UIcontroller;
    public TextAsset textAsset;
    void Start() {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        for(int i=0; i<7; i++){
            string curTagName = "Letter" + i;
            XmlNodeList cost_Table = xmlDoc.GetElementsByTagName(curTagName);
                foreach(XmlNode cost in cost_Table){
                    Debug.Log(cost.InnerText);
                    UIcontroller.SendMessage(cost.InnerText, MessageSendMode.mode.fade, 1.0f);
                }
        }
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
