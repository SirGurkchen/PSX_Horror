using UnityEngine;
using UnityEngine.UI;

public class BusDoor : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject _outlineDoor;

    public void Interact(Player player)
    {
        Debug.Log("Interact!");
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