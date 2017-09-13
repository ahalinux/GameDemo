using UnityEngine;
using System.Collections;

public class ServerProperty : MonoBehaviour {

    public string ip = "127.0.0.1:9080";

    //public string name = "1区 腾讯云";
    private string _name;
    public string name
    {
        get { return _name; }
        set
        {
            this.transform.Find("Label").GetComponent<UILabel>().text = value;  //改写文字显示
            _name = value;
        }
    }
    public int count = 100;

    public void OnPress(bool isPress)
    {
        if(isPress == false)
        {
            //选择了当前的服务器
            this.transform.root.SendMessage("OnServerselect", this.gameObject);
        }
    }
}
