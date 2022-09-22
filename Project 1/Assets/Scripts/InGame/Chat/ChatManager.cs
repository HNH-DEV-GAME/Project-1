using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private TMP_Text chatBox;
    [SerializeField] private TMP_InputField inputChat;
    [SerializeField] private GameObject[] hideChatBox;
    private PhotonView pv;
    private bool isChating;
    private bool isDisplayed;
    private string namePlayer;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        inputChat.characterLimit = 100;
        chatBox.text = "";
        HideChatBox();
        if (pv.IsMine) { namePlayer = pv.Owner.NickName; }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isDisplayed)
        {
            pv.RPC("Chating", RpcTarget.All,inputChat.text,namePlayer);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !inputChat.isFocused)
        {
            inputChat.Select();
            DisplayChatBox();
        }
        else if (inputChat.isFocused)
        {
            DisplayChatBox();
        }
        else if(!inputChat.isFocused)
        {
            HideChatBox();
        }

    }
    [PunRPC]
    private void Chating(string content,string name)
    {
        if (string.IsNullOrEmpty(content)) return;
        chatBox.text += "\n" + name +" : " +content;
        inputChat.text = "";
    }

    private void HideChatBox()
    {
        foreach (var component in hideChatBox)
        {
            if (component.GetComponent<TMP_Text>() != null)
            {
                component.GetComponent<TMP_Text>().color = new Color32(0, 0, 0, 120);
                continue;
            }
            component.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
        }
        GameManager.Instance.SetIsChating(false);
        isDisplayed = false;
    }

    private void DisplayChatBox()
    {
        foreach (var component in hideChatBox)
        {
            if (component.GetComponent<TMP_Text>() != null)
            {
                component.GetComponent<TMP_Text>().color = new Color32(0, 0, 0, 255);
                continue;
            }
            component.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        GameManager.Instance.SetIsChating(true);
        isDisplayed = true;
    }

    public bool GetIsChating()
    {
        return isChating;
    }
}
