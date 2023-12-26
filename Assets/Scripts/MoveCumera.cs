using UnityEngine;

public class MoveCumera : MonoBehaviour
{
    public Rigidbody2D player;
    public float OffsetMoveRight;
    public float OffsetMoveLeft;
    void Update()
    {
        if (player.transform.position.x > (transform.position.x + OffsetMoveRight))
        {
            transform.position = new Vector3(player.transform.position.x - OffsetMoveRight, transform.position.y, transform.position.z);
        }
        if (player.transform.position.x < (transform.position.x - OffsetMoveLeft))
        {
            transform.position = new Vector3(player.transform.position.x + OffsetMoveLeft, transform.position.y, transform.position.z);
        }
    }
}
