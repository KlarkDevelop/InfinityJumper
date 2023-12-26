using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public GameObject deathEff;
    public GameObject DeathWindow;
    public AudioClip sound;
    private AudioSource PlayerSound;
    private SpriteRenderer playerSpr;
    private Rigidbody2D playerRb;

    [Header("Joysticks")]
    public GameObject jumpJ;
    public GameObject throwJ;
    public GameObject dashJ;

    private void Start()
    {
        PlayerSound = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
        playerSpr = GetComponentInChildren<SpriteRenderer>();
    }

    public float Delay = 2;
    private bool oneSpawn = false;
    [SerializeField] private int countDeathForAd = 3;
    public int countDeath = 0;
    private void Update()
    {
        if(ColisionPL.Instance.health <= 0)
        {
            playerSpr.enabled = false;
            playerRb.velocity = Vector2.zero;
            playerRb.bodyType = RigidbodyType2D.Kinematic;
            if(oneSpawn == false)
            {
                jumpJ.SetActive(false);
                dashJ.SetActive(false);
                throwJ.SetActive(false);

                Instantiate(deathEff, transform);
                PlayerSound.PlayOneShot(sound);
                oneSpawn = true;
                countDeath ++;
            }
            Delay -= Time.deltaTime;
            if(Delay < 0)
            {
                DeathWindow.SetActive(true);
                if(countDeath == countDeathForAd)
                {
                    countDeath = 0;
                    InterstitialAd.Singleton.ShowAd();
                }
            }
        }
    }
}
