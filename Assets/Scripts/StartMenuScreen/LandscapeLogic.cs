using UnityEngine;

public class LandscapeLogic : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private const float _despawnX = -250f;

    private Vector3 move;

    private void Start()
    {
        move = new Vector3(-_moveSpeed, 0, 0);
    }

    private void Update()
    {
        this.gameObject.transform.position += move * Time.deltaTime;

        if (this.gameObject.transform.position.x <= _despawnX)
        {
            LandscapePool.Instance.ReturnLand(this.gameObject);
        }
    }
}