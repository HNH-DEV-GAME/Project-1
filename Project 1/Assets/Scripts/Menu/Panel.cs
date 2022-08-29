using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public enum TypePanel 
    {
        LoadingPanel,
        LoginPanel,
        SelectionPanel,
        CreateRoomPanel,
        JoinRandomRoomPanel,
        RoomListPanel
    }
    public TypePanel typePanel = new TypePanel();
    
    public TypePanel GetTypePanel()
    {
        return typePanel;
    }
}
