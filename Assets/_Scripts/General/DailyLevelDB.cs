using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLevelDB : SingletonBaseDontDestroyOnLoad<DailyLevelDB>
{
    public List<CountryScoreEntry> countryScoreEntries = new List<CountryScoreEntry>();
    public CountryScoreEntry userCountryScore;
    public bool finishLoadDB;
    private int championID = -1;
    public int ChampionID { get { return championID; } }

    private void Start()
    {
        StartCoroutine(WaitingFirebaseInit());
    }
    public CountryScoreEntry GetUserCountryScore()
    {
        foreach(CountryScoreEntry countryScoreEntry in countryScoreEntries)
        {
            if (countryScoreEntry.countryID == GameCache.GC.userCountryID) return countryScoreEntry;
        }
        return null;
    }
    public IEnumerator WaitingFirebaseInit()
    {
        while (!FireBaseManager.Instance.finishInit)
        {
            yield return new WaitForSeconds(0.5f);
        }
        yield return GetLastChampionID();
        yield return CheckAndResetDatabase();
        GetCountryScores();
    }

    IEnumerator CheckAndResetDatabase()
    {
        var task = FireBaseManager.Instance.databaseReference.Child("LastResetDate").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Error getting LastResetDate: " + task.Exception);
            yield break;
        }

        string lastResetDate = task.Result.Value?.ToString();
        DateTime lastReset = DateTime.MinValue;

        if (!string.IsNullOrEmpty(lastResetDate))
        {
            lastReset = DateTime.Parse(lastResetDate);
        }
        DateTime currentDay = new();
        yield return AmericanTimeResetDB.GetAmericanTime(currentAmericanTime =>
        {
            currentDay = currentAmericanTime.Date;
        });

        if (currentDay > lastReset.Date)
        {
            yield return GetChampionIDInLB();
            ResetDatabase();

            var updateTask = FireBaseManager.Instance.databaseReference.Child("LastResetDate").SetValueAsync(currentDay.ToString("o"));
            yield return new WaitUntil(() => updateTask.IsCompleted);

            if (updateTask.IsFaulted)
            {
                Debug.LogError("Error updating LastResetDate: " + updateTask.Exception);
            }
        }
    }

    public void UpdateCountryScoreToLB(int order, CountryScoreEntry countryScoreEntry)
    {
        string json = JsonUtility.ToJson(countryScoreEntry);
        FireBaseManager.Instance.databaseReference.Child($"Leaderboard/{order}").SetRawJsonValueAsync(json);
    }

    private void ResetDatabase()
    {
        for (int i = 0; i < CountryInfo.countryCodes.Length; i++)
        {
            CountryScoreEntry countryScoreEntry = new CountryScoreEntry(0, i, i);
            UpdateCountryScoreToLB(i, countryScoreEntry);
        }
    }

    IEnumerator GetLastChampionID()
    {
        var task = FireBaseManager.Instance.databaseReference.Child("DailyLevelLastChampion").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        if (task.IsFaulted)
        {
            Debug.LogError("Error getting DailyLevelLastChampion: " + task.Exception);
            yield break;
        }

        string lastChampionID = task.Result.Value?.ToString();

        if (!string.IsNullOrEmpty(lastChampionID))
        {
            championID = int.Parse(lastChampionID);
        }
    }

    IEnumerator GetChampionIDInLB()
    {
        var task = FireBaseManager.Instance.databaseReference.Child("Leaderboard").Child("0").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Error getting ChampionID in Leaderboard: " + task.Exception);
            yield break;
        }

        DataSnapshot snapshot = task.Result;
        CountryScoreEntry countryScoreEntry = JsonUtility.FromJson<CountryScoreEntry>(snapshot.GetRawJsonValue());
        championID = countryScoreEntry.countryID;
        yield return SetLastChampionID();
    }

    IEnumerator SetLastChampionID()
    {
        var task = FireBaseManager.Instance.databaseReference.Child("DailyLevelLastChampion").SetValueAsync(championID.ToString());
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Error setting DailyLevelLastChampion: " + task.Exception);
        }
    }

    private void GetCountryScores()
    {
        FireBaseManager.Instance.databaseReference.Child("Leaderboard").OrderByChild("order").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error getting data: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                countryScoreEntries.Clear();

                foreach (var childSnapshot in snapshot.Children)
                {
                    CountryScoreEntry countryScoreEntry = JsonUtility.FromJson<CountryScoreEntry>(childSnapshot.GetRawJsonValue());
                    countryScoreEntries.Add(countryScoreEntry);

                    if (countryScoreEntry.countryID == GameCache.GC.userCountryID)
                    {
                        userCountryScore = countryScoreEntry;
                    }
                }
                finishLoadDB = true;
            }
        });
    }

    private void SortAndUpdateLeaderboard()
    {
        if (userCountryScore.order != 0 && userCountryScore.score > countryScoreEntries[userCountryScore.order - 1].score)
        {
            countryScoreEntries.Sort((x, y) => y.score.CompareTo(x.score));
        }
        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        for (int i = 0; i < countryScoreEntries.Count; i++)
        {
            countryScoreEntries[i].order = i;
            UpdateCountryScoreToLB(i, countryScoreEntries[i]);
        }
    }

    public void IncreaseUserCountryScore()
    {
        userCountryScore.score++;
        SortAndUpdateLeaderboard();
    }
}
