using UnityEngine;

public class randomDestroy : MonoBehaviour
{
    public float chanceNotDestroy = 50;
    private void Awake()
    {
        float rnd = Random.Range(0, 101);
        if (rnd > chanceNotDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
