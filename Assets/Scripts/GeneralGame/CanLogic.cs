using UnityEngine;

public class CanLogic : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outlineCan;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private bool _isBranchBreaking;
    [SerializeField] private float _branchBreakTimer;

    public void Interact(Player player)
    {
        if (player.GetHeldObject() == null)
        {
            _audioManager.PlaySound(AudioManager.SoundType.Pick);
            player.HoldObject(this.gameObject);
            if (_isBranchBreaking)
            {
                Invoke("PlayBranch", _branchBreakTimer);
            }
        }
    }

    private void PlayBranch()
    {
        _audioManager.PlayBreakSound();
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