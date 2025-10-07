using System;
using UnityEngine;
using UnityEngine.UI;

public class BusDoor : MonoBehaviour, IInteract
{
    public event Action OnBusEnter;

    [SerializeField] private GameObject _outlineDoor;
    [SerializeField] private CameraManager _camManager;

    public void Interact(Player player)
    {
        if (player.PlayerIsStanding())
        {
            player.SetInBus();
            HideOutline();
            _camManager.SwitchToBusCam();
            OnBusEnter?.Invoke();
        }
    }

    public void ShowOutline()
    {
        _outlineDoor.SetActive(true);
    }

    public void HideOutline()
    {
        _outlineDoor.SetActive(false);
    }
}