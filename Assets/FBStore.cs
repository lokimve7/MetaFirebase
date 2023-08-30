using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;
using UnityEngine.UI;

[FirestoreData]
public struct UserData
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public List<int> Items { get; set; }
}

public class FBStore : MonoBehaviour
{
    public static FBStore instance;

    FirebaseFirestore fireStore;

    public UserData userData;

    public Text text;

    int index = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            fireStore = FirebaseFirestore.DefaultInstance;
            userData = new UserData();
            userData.Name = "김현진";
            userData.Items = new List<int>();
            userData.Items.Add(12);
            userData.Items.Add(3);
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveUserData();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadUserData();
        }
    }

    public void SaveUserData()
    {
        StartCoroutine(ISaveUserData());
    }

    IEnumerator ISaveUserData()
    {
        //무조건 경로를 두단계 이상으로
        string path = "user/"+ index;
        var task = fireStore.Document(path).SetAsync(userData);
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Exception == null)
        {
            print("저장 성공");
            index++;
        }
        else
        {
            print("저장 실패");
        }
    }

    public void LoadUserData()
    {
        StartCoroutine(ILoadUserData());
    }

    IEnumerator ILoadUserData()
    {
        string path = "user/0";
        
        var task = fireStore.Document(path).GetSnapshotAsync(); //fireStore.Collection(path).GetSnapshotAsync(); //
        
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.Exception == null)
        {
            DocumentSnapshot result = task.Result;
            print("로드 성공");
            userData = task.Result.ConvertTo<UserData>();
            text.text = userData.Name;
            print(userData.Name);
            for (int i = 0; i < userData.Items.Count; i++)
            {
                print(userData.Items[i]);
            }
        }
        else
        {
            print("로드 실패");
        }
    }
}
