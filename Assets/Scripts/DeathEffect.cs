using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
