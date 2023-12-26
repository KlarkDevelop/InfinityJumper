using UnityEngine;
using UnityEngine.Advertisements;

public class AdInicilization : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iOSGameId;
    [SerializeField] private bool AdTestMod;

    private string gameId;
    private void Awake()
    {
        InitializedAds();
    }

    private void InitializedAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSGameId : androidGameId;
#if UNITY_EDITOR
        gameId = androidGameId;
#endif
        Advertisement.Initialize(gameId, AdTestMod, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initilization ad complet");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Initilization ad faild: {error.ToString()}. Message: {message}");
    }
}
