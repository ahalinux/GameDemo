using UnityEngine;
using System.Collections;

public class StartmenuController : MonoBehaviour {

    public static StartmenuController _instance;

    public TweenScale startpanelTween;
    public TweenScale loginpanelTween;
    public TweenScale registerpanelTween;
    public TweenScale serverpanelTween;
    public TweenAlpha startpanelTweenAlpha;
    public TweenAlpha characterselectTween;

    public UIInput usernameInputLogin;
    public UIInput passwordInputLogin;
    public UIInput usernameInputRegister;
    public UIInput passwordInputRegister;
    public UIInput repasswordInputRegister;

    public UILabel usernameLabelStart;
    public UILabel usernameLabelRegister;
    public UILabel servernameLabelStart;

    public UIGrid serverlistGrid;

    public GameObject serveritemRed;
    public GameObject serveritemGreen;
    public GameObject serverSelectedGo;
    public GameObject[] characterArray;

    private GameObject characterSelected;

    public static string username;
    public static string password;
    public static ServerProperty sp;

    private bool haveInitServerlist = false;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        InitServerlist();
    }

    #region start面板
    public void OnUsernameClick()
    {
        //输入账号进行登录

        //隐藏当前面板，显示登录面板
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();
    }

    public void OnServerClick()
    {
        //选择服务器
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));
        serverpanelTween.gameObject.SetActive(true);
        serverpanelTween.PlayForward();

        //初始化服务器列表，放在Start()中
        //InitServerlist();
    }

    public void InitServerlist()
    {
        if (haveInitServerlist) return;

        //1. 连接服务器取得游戏服务器列表信息
        //TODO
        //2. 根据上面的信息，添加服务器列表
        for (int i = 0; i < 20; i++)
        {
            string ip = "127.0.0.1:9080";
            string name = (i + 1) + "区 腾讯云";
            int count = Random.Range(0, 100);

            GameObject go = null;
            if(count > 50)
            {
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemRed);
            }
            else
            {
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemGreen);
            }

            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;

            serverlistGrid.AddChild(go.transform);  //让serverlistGrid进行排序（不写也行）
        }

        haveInitServerlist = true;
    }

    public void OnEnterGameClick()
    {
        //1.连接服务器，验证用户名和服务器
        //TODO

        //2. 进入角色选择界面
        startpanelTweenAlpha.PlayForward();
        StartCoroutine(HidePanel(startpanelTweenAlpha.gameObject));
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }
    #endregion

    #region login面板
    public void OnLoginClick()
    {
        //得到用户名和密码后存储起来
        username = usernameInputLogin.value;
        password = passwordInputLogin.value;

        //返回开始界面
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        //用户名返回
        usernameLabelStart.text = username;
    }

    public void OnRegisterShowClick()
    {
        username = usernameInputLogin.value;    //---

        //隐藏当前面板，显示注册面板
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        registerpanelTween.gameObject.SetActive(true);
        registerpanelTween.PlayForward();

        usernameLabelRegister.text = username;  //---
    }

    public void OnLoginCloseClick()
    {
        //返回开始界面
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }
    #endregion

    #region register面板
    public void OnCancelClick()
    {
        //隐藏当前面板，显示登录面板
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void OnRegisterCloseClick()
    {
        OnCancelClick();
    }

    public void OnRegisterAndLoginClick()
    {
        //1. 本地校验，连接服务器进行验证
        //TODO

        //2. 连接失败
        //TODO

        //3. 连接成功，保存用户名密码
        //usernameInputRegister.value = username; //将login面板中的username，引入register中
        username = usernameInputRegister.value;
        password = usernameInputLogin.value;

        //返回开始界面
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        usernameLabelStart.text = username;
    }
    #endregion

    #region server面板
    //
    #endregion

    #region characterselect面板

    public void OnCharactershowButtonSureClick()
    {
        //判断姓名输入是否正确
        //TODO
        //判断是否选择角色
        //TODO

        //面板动画
        characterselectTween.PlayReverse();
        StartCoroutine(HidePanel(characterselectTween.gameObject));
        //TODO
    }

    #endregion

    //隐藏面板
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    //处理面板发送过来的请求
    public void OnServerselect(GameObject serverGo)
    {
        sp = serverGo.GetComponent<ServerProperty>();
        //更新服务器面板背景和显示文字
        serverSelectedGo.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().text = sp.name;
    }

    //确认选择（关闭server面板）
    public void OnServerpanelClose()
    {
        //隐藏服务器面板，显示开始面板
        serverpanelTween.PlayReverse();
        StartCoroutine(HidePanel(serverpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        //将选择列表显示到start面板
        servernameLabelStart.text = sp.name;
    }

    //处理形象放大缩小
    public bool isClick = false;
    public void OnCharacterClick(GameObject go)
    {
        if (!isClick)
        {
            iTween.ScaleTo(go, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
            isClick = true;
        }
        else
        {
            iTween.ScaleTo(go, new Vector3(1f, 1f, 1f), 0.5f);
            isClick = false;
        }
        
        if(characterSelected == null)
        {
            characterSelected = go;
        }
    }
}
