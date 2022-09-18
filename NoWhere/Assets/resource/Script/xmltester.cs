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

        string curTagName = "Letter1";
        XmlNodeList cost_Table = xmlDoc.GetElementsByTagName(curTagName);
        string temp = cost_Table[0].InnerText;
        UIcontroller.SendMessage(temp, MessageSendMode.mode.fade, 5.0f);
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
