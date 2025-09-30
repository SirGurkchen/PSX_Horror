using UnityEngine;

public class TrashCanLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outlineTrash;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Player _player;

    public void Interact(Player player)
    {
        if (player.GetHeldObject() != null && player.GetHeldObject().GetComponent<CanLogic>())
        {
            _audioManager.PlaySound(AudioManager.SoundType.Trash);
            HideOutline();
            Destroy(player.GetHeldObject());
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
}