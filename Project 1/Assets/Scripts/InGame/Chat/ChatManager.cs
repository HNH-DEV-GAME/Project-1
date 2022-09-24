using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text chatBox;
    [SerializeField] private TMP_InputField inputChat;
    [SerializeField] private GameObject[] hideChatBox;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSensitivity;
    [SerializeField] private bool inGame;
    private PhotonView pv;
    private bool isChating;
    private bool isDisplayed;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        inputChat.characterLimit = 100;
        chatBox.text = "";
        if (inGame)
        {
            HideChatBox();
        }
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && isDisplayed)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                scrollbar.value += scrollSensitivity;
            }else if (Input.mouseScrollDelta.y < 0)
            {
                scrollbar.value -= scrollSensitivity;
            }
        }
        if (inGame)
        {
            if (Input.GetKey(KeyCode.S) && !inputChat.isFocused || Input.GetKey(KeyCode.W) && !inputChat.isFocused)
            {
                scrollbar.enabled = false;
            }
            if (Input.GetKey(KeyCode.D) && !inputChat.isFocused)
            {
                HideChatBox();
                inputChat.DeactivateInputField();
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
            else if (!inputChat.isFocused)
            {
                HideChatBox();
                inputChat.DeactivateInputField();
            }
        }
        else
        {
            inputChat.Select();    
        }

    }

    public void SetValueOfChat(string namePlayer)
    {
        pv.RPC("Chating", RpcTarget.All, inputChat.text, namePlayer);
    }

    [PunRPC]
    private void Chating(string content,string name)
    {
        if (content == "") return;
        chatBox.text += "\n" + "<color=#4C3549>" + name+ "</color>" + " : " +content;
        inputChat.text = "";
    }
    [PunRPC]
    private void Notification(string content,string name)
    {
        chatBox.text += "\n" + "<color=#B80C09>" + name +" "+ content + "</color>";
    }

    private void HideChatBox()
    {
        foreach (var component in hideChatBox)
        {
            if (component.GetComponent<TMP_Text>() != null)
            {
                component.GetComponent<TMP_Text>().color = new Color32(0, 0, 0, 150);
                continue;
            }
            component.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
        }
        if (inGame)
        {
            GameManager.Instance.SetIsChating(false);
        }
        isDisplayed = false;
        inputChat.scrollSensitivity = 0;
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
        if (inGame)
        {
            GameManager.Instance.SetIsChating(true);
        }
        isDisplayed = true;
        inputChat.scrollSensitivity = 1;
        scrollbar.enabled = true;
    }

    public bool GetIsChating()
    {
        return isChating;
    }
    public bool IsDisplayed()
    {
        return isDisplayed;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        NotificationLeaveRoom(otherPlayer);
    }

    private void NotificationLeaveRoom(Player otherPlayer)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            pv.RPC("Notification", RpcTarget.All, "has left the room", otherPlayer.NickName);
        }
        else
        {
            pv.RPC("Notification", RpcTarget.All, "has left the game", otherPlayer.NickName);
        }
    }
    public void NotificationJoinRoom(Player otherPlayer)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            pv.RPC("Notification", RpcTarget.All, "has joined the room", otherPlayer.NickName);
        }
        else
        {
            pv.RPC("Notification", RpcTarget.All, "has joined the game", otherPlayer.NickName);
        }
    }
}
