using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewordedAd : MonoBehaviour,IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string andriodAdUnityId = "Rewarded_Android";
    [SerializeField] private string iosAdUnityId = "Rewarded_iOS";

    private string unityAdId;

    private void Awake()
    {
        button.interactable = false;
        unityAdId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosAdUnityId : andriodAdUnityId;
    }

    private void Start()
    {
        StartCoroutine(LoadAdAfterTime());
    }

    private IEnumerator LoadAdAfterTime()
    {
        yield return new WaitForSeconds(1);
        LoadAd();
    }

    private void LoadAd()
    {
        Debug.Log("Loading ad: " + unityAdId);
        Advertisement.Load(unityAdId, this);
    }

    public void ShowAd()
    {
        button.interactable = false;
        Debug.Log("Showing ad: " + unityAdId);
        Advertisement.Show(unityAdId, this);
    }

    public CoinPicker playerCoins;
    public int RevordCount = 50;
    private void GiveReword()
    {
        playerCoins.coins += RevordCount;
    }

    [Header("ErorreWindow")]
    public GameObject jumpJ;
    public GameObject dashJ;
    public GameObject throwJ;
    public GameObject ErrorWindow;
    private void ShowErrorWindow()
    {
        jumpJ.SetActive(false);
        dashJ.SetActive(false);
        throwJ.SetActive(false);
        ErrorWindow.SetActive(true);
    }

    public void HideErorreWind()
    {
        jumpJ.SetActive(true);
        dashJ.SetActive(true);
        throwJ.SetActive(true);
        ErrorWindow.SetActive(false);
    }

    [Header("AdBautton")]
    public UnityEngine.UI.Button button;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad {unityAdId} load completed");
        button.interactable = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ad load faild: {placementId}, Errore: {error.ToString()}, Message: {message}");
        button.interactable = false;
    }



    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(unityAdId) && showCompletionState.ToString() ==  UnityAdsCompletionState.COMPLETED.ToString())
        {
            Debug.Log("Give Reword");
            GiveReword();
        }
        else if (placementId.Equals(unityAdId) && showCompletionState.Equals(UnityAdsCompletionState.SKIPPED))
        {
            ShowErrorWindow();
        }
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad show faild: {placementId}, Errore: {error.ToString()}, Message: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }
}
