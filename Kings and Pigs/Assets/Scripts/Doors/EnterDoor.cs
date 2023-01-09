using Player;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    private void Start()
    {
        GameObject king = GameObject.Find("King");
        if (king != null)
        {
            king.transform.position = transform.position;
            king.GetComponent<King>().Animator.SetTrigger("DoorOut");
            animator.SetTrigger("Open");
        }
    }
}
