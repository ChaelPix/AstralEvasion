using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{
    private string playerName;
    private int score;
    private readonly string addScoreURL = "http://dreamlo.com/lb/YEjT9zaMT0eZpQuBfq1utgEMW89ATqGkeTTfe6ZIXaxQ/add/{0}/{1}";
    private readonly string leaderboardURL = "http://dreamlo.com/lb/66046c508f40bb8118033f4f/json-asc";

    [SerializeField] Transform leaderboardGrid;
    [SerializeField] TextMeshProUGUI leaderboardPrfb;
    [SerializeField] TextMeshProUGUI leaderboardScorePrfb;
    [SerializeField] Color32[] colors;

    private void Awake()
    {
        GetLeaderboard();
    }

    public void Init(string _name)
    {
        playerName = _name;
    }

    public void EndGame(int score)
    {
        this.score = score;
        StartCoroutine(UploadScore(playerName, score));
    }

    IEnumerator UploadScore(string playerName, int score)
    {
        string url = string.Format(addScoreURL, WWW.EscapeURL(playerName), score);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Score upload successful");
            }
        }
    }

    public void GetLeaderboard()
    {
        StartCoroutine(DownloadLeaderboard());
    }

    IEnumerator DownloadLeaderboard()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(leaderboardURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                ProcessLeaderboardData(www.downloadHandler.text);
            }
        }
    }

    public void ProcessLeaderboardData(string json)
    {
        LeaderboardRoot leaderboardData = JsonUtility.FromJson<LeaderboardRoot>(json);
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>(leaderboardData.dreamlo.leaderboard.entry);
        entries.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

        foreach (var entry in entries)
        {
            Debug.Log($"Name: {entry.name}, Score: {entry.score}");
        }

        for (int i = 0; i < entries.Count; i++) 
        {
            if (i >= 6)
                break;

            TextMeshProUGUI l = Instantiate(leaderboardPrfb, leaderboardGrid);
            l.SetText("#" + (i + 1) + " " + entries[i].name.Replace("+", " "));

            TextMeshProUGUI ll = Instantiate(leaderboardPrfb, leaderboardGrid);
            ll.SetText("| " + entries[i].score.ToString());

            l.color = colors[i];
            ll.color = colors[i];
        }
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public string name;
    public int score; 
}

[System.Serializable]
public class Leaderboard
{
    public LeaderboardEntry[] entry;
}

[System.Serializable]
public class Dreamlo
{
    public Leaderboard leaderboard;
}

[System.Serializable]
public class LeaderboardRoot
{
    public Dreamlo dreamlo;
}
