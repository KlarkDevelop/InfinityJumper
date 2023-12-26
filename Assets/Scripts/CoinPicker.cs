using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    public int coins;
    public TMP_Text CoinCounter;

    public AudioClip sound;
    private AudioSource PlayerSound;

    private void Start()
    {
        PlayerSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            if (collision.GetComponent<Coin>().OnePick == true)
            {
                coins++;
                PlayerSound.PlayOneShot(sound);
                collision.GetComponent<Coin>().PickUpCoin();
            }
        }
    }

    private void Update()
    {
        CoinCounter.text = coins.ToString();
    }
}
