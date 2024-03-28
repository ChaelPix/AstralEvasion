using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Dan.Main;


public class LeaderboardManager : MonoBehaviour
{
    public bool DEBUG_DOENABLE;
    [SerializeField] private Transform leaderboardGrid;
    [SerializeField] private TextMeshProUGUI leaderboardPrfb;
    [SerializeField] private Color32[] colors;

    private string playerName;
    private int score;

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.supernova.GetEntries(entries =>
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if (i >= 6)
                    return;
                TextMeshProUGUI l = Instantiate(leaderboardPrfb, leaderboardGrid);
                l.text = "#" + (i + 1) + " " + entries[i].Username;
                l.color = colors[i % colors.Length];

                TextMeshProUGUI ll = Instantiate(leaderboardPrfb, leaderboardGrid);
                ll.text = "| " + entries[i].Score.ToString();
                ll.color = colors[i % colors.Length];
            }
        });
    }

    public void Init(string _name)
    {
        playerName = _name;
    }

    public void EndGame(int _score)
    {
        score = _score;
        Leaderboards.supernova.UploadNewEntry(playerName, score);
    }

}
