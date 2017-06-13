using UnityEngine;
using System.Collections;

public class StartmenuController : MonoBehaviour {


    public TweenScale startpanelTween;
    public TweenScale loginpanelTween;
    public TweenScale registerpanelTween;

    public UIInput usernameInputLogin;
    public UIInput passwordInputLogin;

    public UILabel usernameLabelStart;

    public static string username;
    public static string password;

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
        //TODO
    }

    public void OnEnterGameClick()
    {
        //1.连接服务器，验证用户名和服务器
        //TODO

        //2. 进入角色选择界面
        //TODO
    }
    #endregion

    #region login面吧
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
        //隐藏当前面板，显示注册面板
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        registerpanelTween.gameObject.SetActive(true);
        registerpanelTween.PlayForward();
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

    //隐藏面板
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }
}
