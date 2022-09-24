using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    private ChatManager chatManager;
    //[SerializeField] private TMP_Text amountText;
    private void Start()
    {
        if (FindObjectOfType<ChatManager>() != null)
        {
            chatManager = FindObjectOfType<ChatManager>().GetComponent<ChatManager>();
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatManager != null)
        {
            chatManager.SetValueOfChat("Anonymous");  
        }
    }
    public void SetInfoPlayer(Player info)
    {
        nameText.text = info.NickName;
    }
}
