using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

using UnityEngine.Networking;

[System.Serializable]
public class Player
{
    public string pseudo;
    public int score;
}

[System.Serializable]
public class PlayersList
{
    public Player[] players;
}

public class LeaderboardManager : MonoBehaviour
{
    public bool DEBUG_DOENABLE;
    [SerializeField] private Transform leaderboardGrid;
    [SerializeField] private TextMeshProUGUI leaderboardPrfb;
    [SerializeField] private Color32[] colors;

    private string playerName;
    private int score;
    private string scoresUrl = "https://stein-ind.fr/supernova_sprint/get_scores.php";
    private string subUrl = "https://stein-ind.fr/supernova_sprint/submit.php";

    private void Awake()
    {
        StartCoroutine(GetTopScores());
    }
    IEnumerator GetTopScores()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(scoresUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors de la récupération des scores : " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                PlayersList playersList = JsonUtility.FromJson<PlayersList>("{\"players\":" + jsonResponse + "}");

                for (int i = 0; i < playersList.players.Length; i++)
                {
                    if (i >= 6)
                        break;

                    TextMeshProUGUI l = Instantiate(leaderboardPrfb, leaderboardGrid);
                    l.text = "#" + (i + 1) + " " + playersList.players[i].pseudo;
                    l.color = colors[i % colors.Length];

                    TextMeshProUGUI ll = Instantiate(leaderboardPrfb, leaderboardGrid);
                    ll.text = "| " + playersList.players[i].score.ToString();
                    ll.color = colors[i % colors.Length];
                }

            }
        }
    }
    public void Init(string _name)
    {
        playerName = _name;
    }

    public void EndGame(int _score)
    {
        score = _score;
        StartCoroutine(PostScore(playerName, _score));
    }

    IEnumerator PostScore(string pseudo, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("pseudo", pseudo);
        form.AddField("score", score);

        string jsonData = JsonUtility.ToJson(new Player { pseudo = playerName, score = score });

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(subUrl, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();
        }
    }
}

