using UnityEngine;

public class CanLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outlineCan;
    [SerializeField] private AudioManager _audioManager;

    public void Interact(Player player)
    {
        _audioManager.PlaySound(AudioManager.SoundType.Pick);
        player.HoldObject(this.gameObject);
    }

    public void ShowOutline()
    {
        _outlineCan.SetActive(true);
    }

    public void HideOutline()
    {
        _outlineCan.SetActive(false);
    }
}