using UnityEngine;
using TMPro;

public class ColisionPL : MonoBehaviour
{
    [Header("Булевые анимаций")]
	public bool DoJ = false;
	public bool DoDoublleJ = false;
	public bool animJ = false;
	public bool animP = false;
	public bool animOnW = false;

    [Header("Значения бонусных расходников")]
	public int GunCount = 0;
	public int TrowCount = 0;
	public int health = 3;

	public bool OnAtackArea;
	private Rigidbody2D rb;
	public static ColisionPL Instance { get; private set; }

	private void Awake()
	{
        PlayerSource = GetComponent<AudioSource>();
        SavePoint = transform.position;
		Instance = this;
		rb = GetComponent<Rigidbody2D>();
		//playerSpR = GetComponent<SpriteRenderer>();
	}

	public bool PlAtack;
	public bool OnEnemy = false;
	public bool TurnLeft = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Map" && Dash.InstanceD.DoDash == true && Dash.InstanceD.TimerMin < 0)
        {
            if (transform.position.x < collision.transform.position.x)
            {
                Dash.InstanceD.DoDash = false;
                PlayerJump.InstancePJ.DoublJump(0, false);
                Dash.InstanceD.OffFreez();
                Dash.InstanceD.workFAD = true;
                rb.velocity = new Vector2(0, 0);
                rb.transform.position += new Vector3(0.5f, 0);
                Debug.Log("Auch");
            }
            else
            {
                Dash.InstanceD.DoDash = false;
                PlayerJump.InstancePJ.DoublJump(0, false);
                Dash.InstanceD.OffFreez();
                Dash.InstanceD.workFAD = true;
                rb.velocity = new Vector2(0, 0);
                rb.transform.position -= new Vector3(0.5f, 0);
                Debug.Log("Auch2");
            }
        }
    }

    public float DamageForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyArrow")
        {
			TakeDamage(collision.collider.transform.position);
        }
        if (collision.collider.tag == "Spike")
        {
            
            if(health > 1)
            {
                transform.position = SavePoint;
            }
            TakeDamage();
            rb.velocity = new Vector2(0, 0);

            afterSpikes = true;

            if(EnemyClass != null)EnemyClass.PlAtack = false;
            DoJ = false;
            DoDoublleJ = true;
            OnEnemy = false;
            PlayerJump.InstancePJ.DoublJump(0, false);
        }
        if (collision.collider.tag == "Enemy" && Dash.InstanceD.DoDash == false)
        {
			EnemyClass = collision.collider.GetComponent<Enemy>();
			PlAtack = true;
			DoJ = true;
			DoDoublleJ = false;
			OnEnemy = true;
        }
        if(EnemyClass == null)
        {
            PlAtack = false;
            OnEnemy = false;
        }
	}
    private Transform Enemy;
	public TMP_Text HealthCounter;
	public bool WorkMagnet = false;
    public Vector2 SavePoint;

    public int HPefficiency = 1;
    public int BGefficiency = 3;

    [Header("Sounds")]
    public AudioClip SpringSound;
    public AudioClip BonusSound;
    public AudioClip damageSound;
    private AudioSource PlayerSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            SavePoint = collision.GetComponentInChildren<Rigidbody2D>().transform.position;
        }
        if (collision.CompareTag("BonusGun"))
		{
			TrowCount += BGefficiency;
			GunCount += BGefficiency;
            PlayerSource.PlayOneShot(BonusSound);
			Destroy(collision.gameObject);
		}
        if (collision.CompareTag("HealthPoithon"))
        {
			health += HPefficiency;
            PlayerSource.PlayOneShot(BonusSound);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Magnet"))
        {
			MagnetTimer = ResetMagnetTimer;
            PlayerSource.PlayOneShot(BonusSound);
            Destroy(collision.gameObject);
		}
        if (collision.CompareTag("BGcustom"))
        {
            BGcustom bonus = collision.GetComponent<BGcustom>();
            if ((bonus.ThrowCount + TrowCount) < 0)
            {
                bonus.ThrowCount = -((-bonus.ThrowCount) - (-bonus.ThrowCount - TrowCount));
            }
            if ((bonus.DashCount + GunCount) < 0)
            {
                bonus.DashCount = -((-bonus.DashCount) - (-bonus.DashCount - GunCount));
            }
            if ((bonus.HealthCount + health) < 0)
            {
                bonus.HealthCount = -((-bonus.HealthCount) - (-bonus.HealthCount - health));
            }
            TrowCount += bonus.ThrowCount;
            GunCount += bonus.DashCount;
            health += bonus.HealthCount;
            PlayerSource.PlayOneShot(BonusSound);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Spring"))
        {
            if(Dash.InstanceD.DoDash == true)
            {
                Dash.InstanceD.DoDash = false;
                Dash.InstanceD.OffFreez();
            }

            SpringPlatform spring = collision.GetComponent<SpringPlatform>();

            DoDoublleJ = true;
            PlayerJump.InstancePJ.DoublJump(0, false);

            PlayerSource.PlayOneShot(SpringSound);
            rb.velocity = spring.vector;
            spring.ChangeAnim("Spring");
        }
    }
	
    public Enemy EnemyClass;
    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.CompareTag("ATACK"))
		{
			OnAtackArea = false;
		}
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.CompareTag("ATACK"))
		{
			OnAtackArea = true;
			Enemy = collision.GetComponentInParent<Transform>();
			EnemyClass = collision.GetComponentInParent<Enemy>();
		}
        if (collision.CompareTag("Button"))
        {
            if(collision.TryGetComponent(out Button button))
            {
                button.DoPress();
            }
            if(collision.TryGetComponent(out ButtonMulti buttonMulti))
            {
                buttonMulti.DoPress();
            }
        }
	}

	private float grean = 255;
	private float blue = 255;
	public bool OneKick;

	public bool LeftWork = false;
	public bool RightWork = false;
	public bool DownWork = false;
	public float distance;

	public SpriteRenderer playerSpR;
    private void Update()
    {
        if(afterSpikes == true)
        {
            fixPlayer();
        }

		RaycastCheker();

        if (OnAtackArea == true && EnemyClass.DoAtack == true && OneKick == true)
        {
            OneKick = false;
            TakeDamage(Enemy.transform.position);

            //фикс рейкастов после удара
            Offset = OffsetNorm;
            Offset1 = OffsetNorm1;
            OffsetDown = OffsetDownNorm;
            WorkChaker = true;
            WorkChakerPodcat = true; 
        }

        ReturnColor();
        CounterUI();
        MagnetingCoin();
    }

    [Header("Верхний боковой луч")]
    public Vector2 OffsetNorm;
	public Vector2 Offset;
	public Vector2 OffsetDoubleJ;
    public Vector2 OffsetPodcat;

    [Header("Нижний боковой луч")]
    public Vector2 OffsetNorm1;
    public Vector2 Offset1;
	public Vector2 Offset1DoubleJ;
    public Vector2 Offset1Podcat;

    [Header("Нижний луч")]
    public Vector2 OffsetDownNorm;
	public Vector2 OffsetDown;
	public Vector2 OffsetDownDJ;
    public Vector2 OffsetDownPodcat;

    private bool WorkChaker = false;
    private bool WorkChakerPodcat = false;
	private void RaycastCheker()
    {
        Physics2D.queriesStartInColliders = false;

        if (( OnEnemy == true || PlayerJump.InstancePJ.inDaubleAnimation == true ) && WorkChaker == true)
        {
            Offset = OffsetDoubleJ;
            Offset1 = Offset1DoubleJ;
            OffsetDown = OffsetDownDJ;

            WorkChaker = false;
            WorkChakerPodcat = true;
        }
        else if ((PlayerJump.InstancePJ.inDaubleAnimation == false && OnEnemy == false && PlayerJump.InstancePJ.inPodcatAnimation == false) || Dash.InstanceD.DoDash == true && WorkChaker == false)
        {
            Offset = OffsetNorm;
            Offset1 = OffsetNorm1;
            OffsetDown = OffsetDownNorm;

            WorkChaker = true;
            WorkChakerPodcat = true;
        }
        if(PlayerJump.InstancePJ.inPodcatAnimation == true && WorkChakerPodcat == true)
        {
            Offset = OffsetPodcat;
            Offset1 = Offset1Podcat;
            OffsetDown = OffsetDownPodcat;
            WorkChakerPodcat = false;
        }

        RaycastHit2D LeftHit = Physics2D.Raycast(transform.position + new Vector3(-Offset.x, Offset.y), Vector2.left, distance);
		Debug.DrawRay(transform.position + new Vector3(-Offset.x, Offset.y), Vector2.left, Color.red);
		RaycastHit2D LeftHit1 = Physics2D.Raycast(transform.position + new Vector3(-Offset1.x, Offset1.y), Vector2.left, distance);
		Debug.DrawRay(transform.position + new Vector3(-Offset1.x, Offset1.y), Vector2.left, Color.red);

		RaycastHit2D RightHit = Physics2D.Raycast(transform.position + new Vector3(Offset.x, Offset.y), Vector2.right, distance);
		Debug.DrawRay(transform.position + new Vector3(Offset.x, Offset.y), Vector2.right, Color.red);
		RaycastHit2D RightHit1 = Physics2D.Raycast(transform.position + new Vector3(Offset1.x, Offset1.y), Vector2.right, distance);
		Debug.DrawRay(transform.position + new Vector3(Offset1.x, Offset1.y), Vector2.right, Color.red);

		RaycastHit2D DownHit = Physics2D.Raycast(transform.position + new Vector3(OffsetDown.x, OffsetDown.y), Vector2.down * transform.localScale, distance);
		Debug.DrawRay(transform.position + new Vector3(OffsetDown.x, OffsetDown.y), Vector2.down * transform.localScale, Color.red, distance);
		RaycastHit2D DownHit1 = Physics2D.Raycast(transform.position + new Vector3(-OffsetDown.x, OffsetDown.y), Vector2.down * transform.localScale, distance);
		Debug.DrawRay(transform.position + new Vector3(-OffsetDown.x, OffsetDown.y), Vector2.down * transform.localScale, Color.red, distance);


		if ((LeftHit.collider != null && LeftHit.collider.tag == "Map")|| (LeftHit1.collider != null && LeftHit1.collider.tag == "Map") && LeftWork == true)//Проверка левого луча
        {
            if (LeftHit.collider != null)
            {
                if (LeftHit.collider.TryGetComponent(out movingPlatformY plat)) rb.transform.parent = LeftHit.collider.transform;
                if (LeftHit.collider.TryGetComponent(out movingPlatformX platX)) rb.transform.parent = LeftHit.collider.transform;
            }
            else if (LeftHit1.collider != null)
            {
                if (LeftHit1.collider.TryGetComponent(out movingPlatformY plat)) rb.transform.parent = LeftHit1.collider.transform;
                if (LeftHit1.collider.TryGetComponent(out movingPlatformX platX)) rb.transform.parent = LeftHit1.collider.transform;
            }

            if (EnemyClass != null) { EnemyClass.PlAtack = false; EnemyClass.FollDeathCheker = true; }

            PlAtack = false;
            OnEnemy = false;

            animOnW = true;
            DoJ = true;
            animJ = false;
            TurnLeft = false;
            LeftWork = false;
        }
        if (LeftHit.collider == null&& LeftHit1.collider == null && LeftWork == false)
        {
            rb.transform.parent = null;

            animOnW = false;
            DoJ = false;
            DoDoublleJ = true;
            animJ = true;
            LeftWork = true;
        }

        if ((RightHit.collider != null && RightHit.collider.tag == "Map") || (RightHit1.collider != null && RightHit1.collider.tag == "Map") && RightWork == true)//Проверка правого луча
        {
            if(RightHit.collider != null)
            {
                if (RightHit.collider.TryGetComponent(out movingPlatformY plat)) rb.transform.parent = RightHit.collider.transform;
                if (RightHit.collider.TryGetComponent(out movingPlatformX platX)) rb.transform.parent = RightHit.collider.transform;
            }
            else if (RightHit1.collider != null)
            {
                if (RightHit1.collider.TryGetComponent(out movingPlatformY plat)) rb.transform.parent = RightHit1.collider.transform;
                if (RightHit1.collider.TryGetComponent(out movingPlatformX platX)) rb.transform.parent = RightHit1.collider.transform;
            }


            if (EnemyClass != null) { EnemyClass.PlAtack = false; EnemyClass.FollDeathCheker = true; }

            PlAtack = false;
            OnEnemy = false;

            animOnW = true;
            DoJ = true;
            animJ = false;
            TurnLeft = true;
            RightWork = false;
        }
        if (RightHit.collider == null && RightHit1.collider == null && RightWork == false)
        {
            rb.transform.parent = null;

            animOnW = false;
            DoJ = false;
            DoDoublleJ = true;
            animJ = true;
            RightWork = true;
        }

        if ((DownHit.collider != null && DownHit.collider.tag == "Map") || (DownHit1.collider != null && DownHit1.collider.tag == "Map"))//Проверка нижнего луча
        {
            DoJ = true;
            animJ = false;
            animP = true;
            DownWork = false;
        }
        if (DownHit.collider == null && DownHit1.collider == null && DownWork == false)
        {
            animP = false;
            DoJ = false;
            DoDoublleJ = true;
            animJ = true;
            DownWork = true;
        }
    }

    private void ReturnColor()
    {
        grean += Time.deltaTime;
        blue += Time.deltaTime;
        playerSpR.color = new Color(255, grean, blue);
    }

    public float RangeMagneyArea;
	public LayerMask Coin;
	public float speedCoins;
	public Vector2 offsetMagnetArea;
	private Vector2 MagneticPoint;

	public float MagnetTimer;
	public float ResetMagnetTimer;
	private void MagnetingCoin()
    {
		if (MagnetTimer > 0)
        {
			MagneticPoint = (Vector2)transform.position + offsetMagnetArea;
			Collider2D[] Coins = Physics2D.OverlapCircleAll(MagneticPoint, RangeMagneyArea, Coin);
			for(int i = 0; i < Coins.Length; i++)
            {
				Coins[i].GetComponent<Coin>().MoveToPlayer(offsetMagnetArea, speedCoins);
            }
			MagnetTimer -= Time.deltaTime;
        }
    }
	public bool DrowGizmo;
    private void OnDrawGizmosSelected() //отображение зоны магнита в юнити
    {
		if(DrowGizmo == true)
        {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere((Vector2)transform.position + offsetMagnetArea, RangeMagneyArea);
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        rb.velocity = new Vector2(0, 0);
        OnAtackArea = false;
        grean = 0;
        blue = 0;
        playerSpR.color = new Color(255, grean, blue);
        PlayerSource.PlayOneShot(damageSound);
    }
    public void TakeDamage(Vector3 sourceHarm)
    {
        TakeDamage();
		if (sourceHarm.x < transform.position.x)
		{
			rb.AddForce(Vector2.right * 4, ForceMode2D.Impulse);
		}
		else
		{
			rb.AddForce(Vector2.left * 4, ForceMode2D.Impulse);
		}
	}

    public TMP_Text DashCounter;
    public TMP_Text ThrowCounter;
	private void CounterUI()
    {
		HealthCounter.text = health.ToString();
        DashCounter.text = GunCount.ToString();
        ThrowCounter.text = TrowCount.ToString();
    }

    private bool afterSpikes = false;
    private float timeFunk = 0.2f;
    private float timeFunkR = 0.2f;
    private void fixPlayer()
    {
        timeFunk -= Time.deltaTime;
        rb.velocity = Vector2.zero;

        DoJ = false;
        DoDoublleJ = false;
        animJ = false;
        animP = false;
        animOnW = false;
        PlayerJump.InstancePJ.ChangeColider(0);

        if(timeFunk < 0 && afterSpikes == true)
        {
            timeFunk = timeFunkR;
            afterSpikes = false;
        }
    }
}
