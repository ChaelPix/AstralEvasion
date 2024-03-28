using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class LeaderboardManager : MonoBehaviour
{
    public bool DEBUG_DOENABLE;
    [SerializeField] private Transform leaderboardGrid;
    [SerializeField] private TextMeshProUGUI leaderboardPrfb;
    [SerializeField] private Color32[] colors;

    private string playerName;
    private int score;
    private string connStr = "server=ky2.h.filess.io;user=games_enterdeer;database=games_enterdeer;port=3306;password=bd44e7b4ef8b8b7977e391dcd6bf009dc7e0be83";

    private void Awake()
    {
        if (DEBUG_DOENABLE)
            StartCoroutine(GetLeaderboard());
    }

    public void Init(string _name)
    {
        playerName = _name;
    }

    public void EndGame(int _score)
    {
        score = _score;
        if (DEBUG_DOENABLE)
            StartCoroutine(UploadScore(playerName, score));
    }

    IEnumerator UploadScore(string _playerName, int _score)
    {
        using (MySqlConnection conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                // check player
                string checkSql = "SELECT score FROM supernova_players WHERE player_name = @playerName LIMIT 1";
                MySqlCommand checkCmd = new MySqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@playerName", _playerName);
                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    int currentScore = Convert.ToInt32(result);
                    if (_score > currentScore)
                    {
                        // new best score
                        string updateSql = "UPDATE supernova_players SET score = @score WHERE player_name = @playerName";
                        MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                        updateCmd.Parameters.AddWithValue("@score", _score);
                        updateCmd.Parameters.AddWithValue("@playerName", _playerName);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // doesnt exist
                    string insertSql = "INSERT INTO supernova_players (player_name, score) VALUES (@playerName, @score)";
                    MySqlCommand insertCmd = new MySqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@playerName", _playerName);
                    insertCmd.Parameters.AddWithValue("@score", _score);
                    insertCmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
        yield return null;
    }


    IEnumerator GetLeaderboard()
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        using (MySqlConnection conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string sql = "SELECT player_name, score FROM supernova_players ORDER BY score DESC LIMIT 6";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        entries.Add(new LeaderboardEntry { name = rdr["player_name"].ToString(), score = Convert.ToInt32(rdr["score"]) });
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }

        for (int i = 0; i < entries.Count; i++)
        {
            TextMeshProUGUI l = Instantiate(leaderboardPrfb, leaderboardGrid);
            l.text = "#" + (i + 1) + " " + entries[i].name.Replace("+", " ");
            l.color = colors[i % colors.Length];

            TextMeshProUGUI ll = Instantiate(leaderboardPrfb, leaderboardGrid);
            ll.text = "| " + entries[i].score.ToString();
            ll.color = colors[i % colors.Length];
        }

        yield return null;
    }
}
[System.Serializable]
public class LeaderboardEntry
{
    public string name;
    public int score;
}
