using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private SaveData Save = new SaveData();
    private string path;
    public bool trainingPass = false;
    public string lang = "en";
    public float volumeInSave;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif  
        if (File.Exists(path))
        {
            Save = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }
        ReadData();
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        WriteData();
        if (pause) File.WriteAllText(path, JsonUtility.ToJson(Save));
    }
#endif

    public void DoSaveData()
    {
        WriteData();
        File.WriteAllText(path, JsonUtility.ToJson(Save));
    }

    public ColisionPL Player;
    public Dash PlayerD;
    public DestroyBullet Shuriken;

    public CoinPicker CoinPick;
    public Shop ShopData;
    public RecordWriter PlayerRecord;
    public DeathPlayer deathPlayer;

    private void ReadData()
    {
        trainingPass = Save.TrainingIsPassed;
        lang = Save.language;
        volumeInSave = Save.volume;

        if (PlayerRecord != null) PlayerRecord.bestRecord.text = Save.bestRecord.ToString();

        if (deathPlayer != null) deathPlayer.countDeath = Save.deathCount;

        if ((Player != null) && (PlayerD != null) && (Shuriken != null)) getImproveLvls();
        if (CoinPick != null) CoinPick.coins = Save.CoinsCount;
        if (ShopData != null)
        {
            ShopData.DashDistLvl = Save.DashDist;
            ShopData.ShurLvl = Save.ShurikenDamage;
            ShopData.MagnetLvl = Save.MagnetRadius;
            ShopData.HealthPotLvl = Save.HealthPotionEf;
            ShopData.GunBLvl = Save.GunBonusEf;
        }
    }
    private void WriteData()
    {
        if (deathPlayer != null) 
        {
            if(deathPlayer.countDeath > 3)
            {
                deathPlayer.countDeath = 3;
            }
            Save.deathCount = deathPlayer.countDeath;
        } 
        if ((PlayerRecord != null) && (PlayerRecord.record > Save.bestRecord)) Save.bestRecord = PlayerRecord.record;
        if (CoinPick != null) Save.CoinsCount = CoinPick.coins;
        if (ShopData != null)
        {
            Save.DashDist = ShopData.DashDistLvl;
            Save.ShurikenDamage = ShopData.ShurLvl;
            Save.MagnetRadius = ShopData.MagnetLvl;
            Save.HealthPotionEf = ShopData.HealthPotLvl;
            Save.GunBonusEf = ShopData.GunBLvl;
        }
    }

    private void getImproveLvls()
    {
        Player.HPefficiency = Save.HealthPotionEf + 1;
        Player.BGefficiency = Save.GunBonusEf + 3;
        Player.RangeMagneyArea = (Save.MagnetRadius * 3) + 5;
        Shuriken.damage = Save.ShurikenDamage + 1;

        switch (Save.DashDist)
        {
            case 0: PlayerD.DashDist = 4;
                PlayerD.TrajectoryDistance = 0.1f;
                break;
            case 1: PlayerD.DashDist = 5.5f;
                PlayerD.TrajectoryDistance = 0.12f;
                break;
            case 2: PlayerD.DashDist = 7;
                PlayerD.TrajectoryDistance = 0.16f;
                break;
            case 3: PlayerD.DashDist = 8;
                PlayerD.TrajectoryDistance = 0.19f;
                break;
        }
    }

    private void OnApplicationQuit()
    {
        DoSaveData();
    }

    public void endTraining()
    {
        trainingPass = true;
        Save.TrainingIsPassed = true;
        DoSaveData();
    }

    public void startTraining()
    {
        trainingPass = false;
        Save.TrainingIsPassed = false;
        DoSaveData();
    }

    public void changeLanguage()
    {
        if (Save.language == "en")
        {
            Save.language = "ru";
            lang = "ru";
        }
        else if (Save.language == "ru")
        {
            lang = "en";
            Save.language = "en";
        }
        DoSaveData();
    }

    public void changeVolume(float value)
    {
        Save.volume = value;
        volumeInSave = value;
        DoSaveData();
    }
}

[Serializable]
public class SaveData
{
    public bool TrainingIsPassed = false;

    public int CoinsCount = 0;

    public float bestRecord = 0;

    public int DashDist = 0;
    public int MagnetRadius = 0;
    public int ShurikenDamage = 0;
    public int HealthPotionEf = 0;
    public int GunBonusEf = 0;

    //settings
    public string language = "en"; // "en" ; "ru"
    public float volume = 1;

    public int deathCount = 0;
}