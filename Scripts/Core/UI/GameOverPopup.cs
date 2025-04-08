using System;
using CWLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : UIBase
{
    [SerializeField] private Button retryBtn;

    private void Awake()
    {
        retryBtn.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        Managers.Instance.DestroyManager();
        SceneManager.LoadScene("StartScene");
    }
}
