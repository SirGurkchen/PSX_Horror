using System;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanLogic : MonoBehaviour, IInteract
{
    public event Action OnAllTrashCollected;

    [SerializeField] private GameObject _outlineTrash;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Player _player;

    private CanLogic[] _cans;
    private List<CanLogic> _canList;

    public void Interact(Player player)
    {
        if (player.GetHeldObject() != null && player.GetHeldObject().TryGetComponent<CanLogic>(out CanLogic can))
        {
            _audioManager.PlaySound(AudioManager.SoundType.Trash);
            HideOutline();
            RemoveCan(can);
            Destroy(can.gameObject);
        }
    }

    public void ShowOutline()
    {
        if (_player.GetHeldObject() != null && _player.GetHeldObject().GetComponent<CanLogic>())
        {
            _outlineTrash.SetActive(true);
        }
    }

    public void HideOutline()
    {
        _outlineTrash.SetActive(false);
    }

    public void ActivateTrashCan()
    {
        _canList = new List<CanLogic>();

        _cans = FindObjectsByType<CanLogic>(FindObjectsSortMode.None);
        foreach (var c in _cans)
        {
            _canList.Add(c);
        }
    }

    private void RemoveCan(CanLogic can)
    {
        _canList.Remove(can);
        if (_canList.Count <= 0)
        {
            OnAllTrashCollected?.Invoke();
        }
    }
}