using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class AmericanTimeResetDB
{
    private const string timeZoneDBUrl = "http://api.timezonedb.com/v2.1/get-time-zone?key=6E27FKEZ9KIM&format=json&by=zone&zone=America/Chicago";

    public static IEnumerator GetAmericanTime(Action<DateTime> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(timeZoneDBUrl))
        {
            // Gửi yêu cầu HTTP GET
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error getting time: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                TimeZoneDBResponse timeData = JsonUtility.FromJson<TimeZoneDBResponse>(jsonResponse);

                DateTime americanTime = DateTime.Parse(timeData.formatted).ToUniversalTime();
                callback(americanTime);
            }
        }
    }

    public static IEnumerator GetTimeRemainReset(Action<TimeSpan> callback)
    {
        yield return GetAmericanTime(currentAmericanTime =>
        {
            DateTime nextResetTime = currentAmericanTime.Date.AddDays(1);
            TimeSpan timeUntilReset = nextResetTime - currentAmericanTime;
            callback(timeUntilReset);
        });
    }

    public static IEnumerator GetTimeRemainString(Action<string> callback)
    {
        yield return GetTimeRemainReset(timeUntilReset =>
        {
            string timeString = $"{timeUntilReset.Hours}h {timeUntilReset.Minutes}m";
            callback(timeString);
        });
    }
}

[Serializable]
public class TimeZoneDBResponse
{
    public string formatted;
}
