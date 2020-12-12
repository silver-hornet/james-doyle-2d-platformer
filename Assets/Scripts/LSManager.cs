using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;
    MapPoint[] allPoints;

    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in allPoints)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        AudioManager.instance.PlaySFX(4);
        LSUIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed) + 0.25f);
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
