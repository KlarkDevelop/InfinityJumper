using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool OnePick = true;
    
    private void Awake()
    {
        OnePick = true;
    }
    public void PickUpCoin() // вызывается при подборе
    {
        GetComponent<Animator>().Play("PickUpedCoin");
        OnMagnetArea = false;
        OnePick = false;
    }

    public void DestroyCoin() //Вызывается на последнем кадре анимаций подбора
    {
        Destroy(gameObject);
    }

    private bool OnMagnetArea = false;
    private Vector2 OffsetPoint;
    private float speedCoins;

    private void Update()
    {
        if(OnMagnetArea == true)
        {
            MoveToPlayer();
        }
    }
    public void MoveToPlayer(Vector2 OffsetP, float spCoins)
    {
        OffsetPoint = OffsetP;
        speedCoins = spCoins;
        OnMagnetArea = true;
    }
    private void MoveToPlayer()
    {
        Vector2 MagneticPoint = (Vector2)PlayerJump.InstancePJ.player.transform.position + OffsetPoint;
        Vector2 dir = (MagneticPoint - (Vector2)transform.position).normalized * speedCoins * Time.deltaTime;
        transform.Translate(dir);
    }
}
