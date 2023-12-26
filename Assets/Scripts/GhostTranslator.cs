using UnityEngine;

public class GhostTranslator : MonoBehaviour
{
    public Rigidbody2D player;
    public TrainingGhost ghost;
    private Animator plAnim;

    private void Awake()
    {
        plAnim = player.GetComponentInChildren<Animator>();
    }

    [Header("Break Points")]
    public Transform tpPoint;
    public Transform stepPoint;

    private bool tpWork = true;
    private void Update()
    {
        if(player.transform.position.x > tpPoint.transform.position.x && tpWork)
        {
            ghost.transform.localScale = new Vector2(1, 1);
            if (ghost.text.rectTransform.localScale.x < 0)
                ghost.text.rectTransform.localScale *= new Vector2(-1, 1);
            ghost.transform.position = transform.position;
            tpWork = false;
        }
        if(player.transform.position.x > stepPoint.transform.position.x)
        {
            ghost.nextStep();
            player.velocity = new Vector2(0, 0);

            plAnim.SetBool("AnimUP", false);
            plAnim.SetBool("AnimDown", false);
            plAnim.SetBool("AnimDouble", false);
            plAnim.SetBool("DoPodkat", false);
            Destroy(this.gameObject);
        }
    }
}
