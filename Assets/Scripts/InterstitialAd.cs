using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAd Singleton;

    [SerializeField] private string andriodAdUnityId = "Interstitial_Android";
    [SerializeField] private string iosAdUnityId = "Interstitial_iOS";

    private string unityAdId;

    private void Awake()
    {
        Singleton = this;
        unityAdId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosAdUnityId : andriodAdUnityId;
    }

    private void Start()
    {
        LoadAd();
    }

    private void LoadAd()
    {
        Debug.Log("Loading ad: " + unityAdId);
        Advertisement.Load(unityAdId, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing ad: " + unityAdId);
        Advertisement.Show(unityAdId, this);
    }



    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad {placementId} load completed");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ad load faild: {placementId}, Errore: {error.ToString()}, Message: {message}");
    }



    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Time.timeScale = 1f;
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad show faild: {placementId}, Errore: {error.ToString()}, Message: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Time.timeScale = 0;
    }
}
