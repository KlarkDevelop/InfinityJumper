using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject OnButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnButton.SetActive(false);
    }

    public Rigidbody2D PlayerRb;
    public AudioClip uiSound;
    public AudioClip errorSound;
    private AudioSource shopSource;
    public SpriteRenderer PlSp;
    public PlayerJump Player;
    public Joystick Jump;
    public Joystick Throw;
    public Joystick DashJoy;
    public GameObject ShopWindow;
    public GameObject AdButton;

    private void Start()
    {
        shopSource = GetComponent<AudioSource>();
    }

    public Transform plPos;
    public void ShowShop()
    {
        PlayerRb.transform.position = plPos.position;
        PlayerRb.velocity = Vector2.zero;
        AdButton.SetActive(true);
        PlSp.enabled = false;
        Player.enabled = false;
        OnButton.SetActive(false);
        Jump.enabled = false;
        DashJoy.enabled = false;
        Throw.enabled = false;
        ShopWindow.SetActive(true);
        ExitButton.SetActive(true);
        ShowUpgradeProgres();
        UIsound();
    }

    public GameObject ExitButton;
    public void ExitFromShop()
    {
        AdButton.SetActive(false);
        PlSp.enabled = true;
        Player.enabled = true;
        Jump.enabled = true;
        DashJoy.enabled = true;
        Throw.enabled = true;
        ExitButton.SetActive(false);
        ShopWindow.SetActive(false);
        UIsound();
    }
    public TMP_Text DashDistPrText;
    public TMP_Text ShurPrText;
    public TMP_Text MagnetPrText;
    public TMP_Text HealthPotPrtext;
    public TMP_Text GunBonPrText;
    private void ShowUpgradeProgres()
    {
        updatePrices();

        DashDistPrText.text = DashPrice.ToString();
        ShurPrText.text = ShurPrice.ToString();
        MagnetPrText.text = MagnetPrice.ToString();
        HealthPotPrtext.text = HealthPotPrice.ToString();
        GunBonPrText.text = GunBPrice.ToString();

        ChangeButton(UpgrButtonDash, DashDistLvl);
        ChangeButton(UpgrButtonShur, ShurLvl);
        ChangeButton(UpgrButtonMagnet, MagnetLvl);
        ChangeButton(UpgrButtonHealthP, HealthPotLvl);
        ChangeButton(UpgrButtonGunB, GunBLvl);
    }

    public Animator UpgrButtonDash;
    public Animator UpgrButtonShur;
    public Animator UpgrButtonMagnet;
    public Animator UpgrButtonHealthP;
    public Animator UpgrButtonGunB;
    private void ChangeButton(Animator button, int lvl)
    {
        button.Play(lvl.ToString());
    }

    private void UIsound()
    {
        shopSource.PlayOneShot(uiSound);
    }
    private void ErrorSound()
    {
        shopSource.PlayOneShot(errorSound);
    }
    public CoinPicker PlCoins;

    public int DashDistLvl;
    public int DashPrice;
    public void UpgradeDashDist()
    {
        if (DashDistLvl < 3)
        {
            if (PlCoins.coins >= DashPrice)
            { 
                DashDistLvl++;
                PlCoins.coins -= DashPrice;

                ChangeButton(UpgrButtonDash, DashDistLvl);
                ShowUpgradeProgres();
                UIsound();
            }
            else
            {
                ErrorSound();
            }
        }
        else
        {
            ErrorSound();
        }
    }
    public int ShurLvl;
    public int ShurPrice;
    public void UpgradeShur()
    {
        if (ShurLvl < 2)
        {
            if (PlCoins.coins >= ShurPrice)
            {
                ShurLvl++;
                PlCoins.coins -= ShurPrice;

                ChangeButton(UpgrButtonShur, ShurLvl);
                ShowUpgradeProgres();
                UIsound();
            }
            else
            {
                ErrorSound();
            }
        }
        else
        {
            ErrorSound();
        }
    }

    public int MagnetLvl;
    public int MagnetPrice;
    public void UpgradeMagnet()
    {
        if (MagnetLvl < 3)
        {
            if (PlCoins.coins >= MagnetPrice)
            {
                MagnetLvl++;
                PlCoins.coins -= MagnetPrice;

                ChangeButton(UpgrButtonMagnet, MagnetLvl);
                ShowUpgradeProgres();
                UIsound();
            }
            else
            {
                ErrorSound();
            }
        }
        else
        {
            ErrorSound();
        }
    }

    public int HealthPotLvl;
    public int HealthPotPrice;
    public void UpgradeHealthPotion()
    {
        if (HealthPotLvl < 2)
        {
            if (PlCoins.coins >= HealthPotPrice)
            {
                HealthPotLvl++;
                PlCoins.coins -= HealthPotPrice;

                ChangeButton(UpgrButtonHealthP, HealthPotLvl);
                ShowUpgradeProgres();
                UIsound();
            }
            else
            {
                ErrorSound();
            }
        }
        else
        {
            ErrorSound();
        }
    }

    public int GunBLvl;
    public int GunBPrice;
    public void UpgradeGunBonus()
    {
        if (GunBLvl < 2)
        {
            if (PlCoins.coins >= GunBPrice)
            {
                GunBLvl++;
                PlCoins.coins -= GunBPrice;

                ChangeButton(UpgrButtonGunB, GunBLvl);
                ShowUpgradeProgres();
                UIsound();
            }
            else
            {
                ErrorSound();
            }
        }
        else
        {
            ErrorSound();
        }
    }
    
    private void updatePrices()
    {
        switch (DashDistLvl) // Рывок 
        {
            case 0: DashPrice = 300; break;
            case 1: DashPrice = 500; break;
            case 2: DashPrice = 1000; break;
            case 3: DashPrice = 0; break;
        }
        switch(ShurLvl)//Cюрикен
        {
            case 0: ShurPrice = 400; break;
            case 1: ShurPrice = 800; break;
            case 2: ShurPrice = 0; break;
        }
        switch (MagnetLvl)//Магнит
        {
            case 0: MagnetPrice = 150; break;
            case 1: MagnetPrice = 250; break;
            case 2: MagnetPrice = 400; break;
            case 3: MagnetPrice = 0; break;
        }
        switch (HealthPotLvl)//Здоровье
        {
            case 0: HealthPotPrice = 500; break;
            case 1: HealthPotPrice = 1000; break;
            case 2: HealthPotPrice = 0; break;
        }
        switch(GunBLvl)//Бонус рывков 
        {
            case 0: GunBPrice = 400; break;
            case 1: GunBPrice = 700; break;
            case 2: GunBPrice = 0; break;
        }
    }
}
