using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using UnityEngine.UI;

public class FireAuth : MonoBehaviour
{
    FirebaseAuth auth;

    //email, password inputfield
    public InputField inputEmail;
    public InputField inputPassword;

    void Start()
    {
        //�α��� ���� üũ �̺�Ʈ ���
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += OnChangedAuthState;
    }

    private void OnDestroy()
    {
        auth.StateChanged -= OnChangedAuthState;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnClickSingIn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnClickLogin();
        }
    }

    void OnChangedAuthState(object sender, EventArgs e)
    {
        //���࿡ �������� ������
        if (auth.CurrentUser != null)
        {
            print("Email : " + auth.CurrentUser.Email);
            print("UserId : " + auth.CurrentUser.UserId);

            //FireDatabase.instance.myInfo.email = auth.CurrentUser.Email;
            //FireDatabase.instance.myInfo.fbId = auth.CurrentUser.UserId;

            print("�α��� ����");
        }
        //�׷��� ������
        else
        {
            print("�α׾ƿ� ����");
        }
    }

    public void OnClickSingIn()
    {
        //if (inputEmail.text.Length == 0 || inputPassword.text.Length == 0)
        //{
        //    print("������ �� �Է��� �ּ���");
        //    return;
        //}
        StartCoroutine(SingIn("lokimve7@naver.com", "Guswls1225"));
    }

    IEnumerator SingIn(string email, string password)
    {
        //ȸ�����Խõ�
        var task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        //����� �Ϸ�ɶ����� ��ٸ���
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Exception == null)
        {
            print("ȸ������ ����");

        }
        else
        {
            print("ȸ������ ���� : " + task.Exception);
        }
    }

    public void OnClickLogin()
    {
        
        StartCoroutine(Login("lokimve7@naver.com", "Guswls1225"));
    }

    IEnumerator Login(string email, string password)
    {
        //�α��� �õ�
        var task = auth.SignInWithEmailAndPasswordAsync(email, password);
        //����� �Ϸ�ɶ����� ��ٸ���
        yield return new WaitUntil(() => task.IsCompleted);
        //���࿡ Error�� ���ٸ�
        if (task.Exception == null)
        {
            print("�α��� ����");
        }
        else
        {
            print("�α��� ���� : " + task.Exception);
        }
    }

    public void OnClickLogOut()
    {
        //�α׾ƿ�
        auth.SignOut();
    }
}