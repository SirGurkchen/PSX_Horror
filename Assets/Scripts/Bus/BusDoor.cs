using System;
using UnityEngine;

public class BusDoor : MonoBehaviour, IInteract
{
    public event Action OnBusEnter;

    [SerializeField] private GameObject _outlineDoor;
    [SerializeField] private CameraManager _camManager;
    [SerializeField] private Player _player;

    public void Interact(Player player)
    {
        /*
        if (player.PlayerIsStanding())
        {
            player.SetInBus();
            HideOutline();
            _camManager.SwitchToBusCam();
            OnBusEnter?.Invoke();
        }*/
        _player.SetInBus();
        HideOutline();
        _camManager.SwitchToBusCam();
        OnBusEnter?.Invoke();
    }

    public void ShowOutline()
    {
        if (!_player.IsInBus())
        {
            _outlineDoor.SetActive(true);
        }
    }

    public void HideOutline()
    {
        _outlineDoor.SetActive(false);
    }
}