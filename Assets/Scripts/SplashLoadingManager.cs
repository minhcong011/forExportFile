// dnSpy decompiler from Assembly-CSharp.dll class: SplashLoadingManager
using System;
using System.Collections;
using System.IO;
using Lean;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class SplashLoadingManager : MonoBehaviour
{
	private void Awake()
	{
		if (!SplashLoadingManager.isCreated)
		{
			SplashLoadingManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		if (this.checkFromLocalRepo)
		{
			if (this.loadingSlider != null)
			{
				this.loadingSlider.value = 0.5f;
			}
		}
	}

	private void setLanguage()
	{
		string text;
		if (!PlayerPrefs.HasKey("Language"))
		{
			text = "English";
			PlayerPrefs.SetString("Language", text);
		}
		else
		{
			text = PlayerPrefs.GetString("Language", "English");
		}
		LeanLocalization.Instance.CurrentLanguage = text;
	}

	private void loadScene()
	{
		if (this.loadingSlider != null)
		{
			this.loadingSlider.value = 1f;
		}
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu")
		{
			SceneManager.LoadScene("MainMenuNew");
		}
	}

	private void IsFileExists(string file, string extension)
	{
		if (!File.Exists(file + "." + extension))
		{
			base.StartCoroutine(this.download(file));
		}
		MonoBehaviour.print(File.Exists(this.getPath(file) + "." + extension));
	}

	private string getPath(string name)
	{
		string result = "jar:file://" + Application.persistentDataPath + "/MissionsMeta/" + name;
		MonoBehaviour.print("aaaho");
		return result;
	}

	private string GetiPhoneDocumentsPath()
	{
		string text = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		text = text.Substring(0, text.LastIndexOf('/'));
		return text + "/Documents";
	}

	private void callToCheckLocally()
	{
		TextAsset textAsset = Resources.Load("MissionsMeta/Missions", typeof(TextAsset)) as TextAsset;
		Singleton<MissionReader>.Instance.ReadFromResources(textAsset.text);
	}

	private IEnumerator download(string url)
	{
		WWW www = new WWW(url);
		while (!www.isDone)
		{
			if (this.loadingSlider != null)
			{
				this.loadingSlider.value = www.progress;
			}
			yield return null;
		}
		yield return www;
		if (this.loadingSlider != null)
		{
			this.loadingSlider.value = www.progress;
		}
		if (www.error != null)
		{
			if (File.Exists(this.getPath("MissionsDownloaded") + ".txt"))
			{
				TextAsset textAsset = Resources.Load("MissionsMeta/MissionsDownloaded", typeof(TextAsset)) as TextAsset;
				Singleton<MissionReader>.Instance.ReadFromResources(textAsset.text);
				MonoBehaviour.print("path " + this.getPath("MissionsDownloaded"));
			}
			else
			{
				TextAsset textAsset = Resources.Load("MissionsMeta/Missions", typeof(TextAsset)) as TextAsset;
				Singleton<MissionReader>.Instance.ReadFromResources(textAsset.text);
				MonoBehaviour.print(" not exists");
			}
		}
		else
		{
			MonoBehaviour.print("UK :: " + www.text);
			if (this.checkFromLocalRepo)
			{
				this.callToCheckLocally();
			}
			else
			{
				TextAsset missions = Resources.Load("MissionsMeta/MissionsDownloaded", typeof(TextAsset)) as TextAsset;
				UKMap tempMap = new UKMap(JsonSerializer.Decode(www.text) as Hashtable);
				yield return new WaitForSeconds(0.2f);
				UKMap localMap = new UKMap(JsonSerializer.Decode(missions.text) as Hashtable);
				float ver = 0f;
				if (tempMap.ContainsProperty("Version"))
				{
					ver = (float)tempMap.GetDoubleValueForKey("Version", 0.0);
				}
				UnityEngine.Debug.Log("ver" + ver);
				float localVer = 0f;
				if (localMap.ContainsProperty("Version"))
				{
					localVer = (float)localMap.GetDoubleValueForKey("Version", 0.0);
				}
				if (ver > localVer)
				{
					Singleton<MissionReader>.Instance.ReadFromResources(www.text);
					PlayerPrefs.SetFloat("MissionVersion", ver);
				}
				else
				{
					Singleton<MissionReader>.Instance.ReadFromResources(missions.text);
					MonoBehaviour.print("here in " + missions.text);
					MonoBehaviour.print("here in " + this.getPath("MissionsDownloaded"));
				}
			}
		}
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}
		yield break;
	}

	public static bool isCreated;

	public bool checkFromLocalRepo;

	public Slider loadingSlider;
}
