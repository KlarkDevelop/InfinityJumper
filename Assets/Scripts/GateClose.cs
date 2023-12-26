using UnityEngine;

public class GateClose : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("GateDown");
    }
}
