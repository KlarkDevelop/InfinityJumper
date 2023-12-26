using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator anim;
    public static Gate Instance { get; set; }
    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }
}
