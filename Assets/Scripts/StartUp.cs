// dnSpy decompiler from Assembly-CSharp.dll class: StartUp
using System;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class StartUp : MonoBehaviour
{
	public void BtnAction()
	{
		if (this.action == "next")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		else if (this.action == "refresh")
		{
			this.counter = 0;
			this.chkFile();
		}
	}

	private void chkFile()
	{
		if (this.counter < this.files.Length)
		{
			this.fileName = this.files[this.counter];
			this.fileURL = this.url[this.counter];
			this.checkFileExists(this.fileName);
			this.counter++;
			this.action = "refresh";
			MonoBehaviour.print("started ");
		}
		else
		{
			this.action = "next";
		}
	}

	private void Start()
	{
		this.chkFile();
		Time.timeScale = 1f;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void checkFileExists(string file)
	{
		if (File.Exists(this.getPath(file) + ".xml"))
		{
			base.StartCoroutine(this.validateVersion(this.fileURL));
		}
		else
		{
			base.StartCoroutine(this.download(this.fileURL));
		}
		MonoBehaviour.print(File.Exists(this.getPath(file) + ".xml"));
	}

	private string getPath(string name)
	{
		this.filePath = Application.persistentDataPath + name;
		return Application.persistentDataPath + name;
	}

	private string GetiPhoneDocumentsPath()
	{
		string text = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		text = text.Substring(0, text.LastIndexOf('/'));
		return text + "/Documents";
	}

	private IEnumerator validateVersion(string url)
	{
		WWW w = new WWW(url);
		while (!w.isDone)
		{
			this.progress.text = string.Concat(new string[]
			{
				this.counter.ToString(),
				"/",
				this.files.Length.ToString(),
				" Checking Update... ",
				(w.progress * 100f).ToString(),
				"%"
			});
			yield return null;
		}
		yield return w;
		if (w.error != null)
		{
			this.progress.text = this.counter.ToString() + "/" + this.files.Length.ToString() + " Check your internet connection!";
			this.action = "refresh";
			this.ActionButton.GetComponentInChildren<Text>().text = "Refresh";
			MonoBehaviour.print("vdfv " + w.error);
		}
		else
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(w.text);
			XmlNode firstChild = xmlDocument.LastChild.FirstChild;
			if (int.Parse(firstChild.InnerText) > PlayerPrefs.GetInt(this.fileName + "_version"))
			{
				base.StartCoroutine(this.downloadingUpdate(this.fileURL));
				MonoBehaviour.print(string.Concat(new object[]
				{
					"now ",
					int.Parse(firstChild.InnerText),
					"\tbefore ",
					PlayerPrefs.GetInt(this.fileName + "_version")
				}));
			}
			else
			{
				this.progress.text = this.counter.ToString() + "/" + this.files.Length.ToString() + " Downloaded 100%";
				this.bar.fillAmount = (float)this.counter * 1f / 3f;
				this.chkFile();
			}
			MonoBehaviour.print("vdfv " + w.text);
		}
		yield break;
	}

	private IEnumerator download(string url)
	{
		WWW www = new WWW(url);
		while (!www.isDone)
		{
			yield return null;
		}
		yield return www;
		if (www.error != null)
		{
			TextAsset textAsset = Resources.Load("Missions", typeof(TextAsset)) as TextAsset;
			Singleton<MissionReader>.Instance.ReadFromResources(textAsset.text);
		}
		else
		{
			MonoBehaviour.print("vdfv " + www.text);
			if (File.Exists(this.getPath(this.fileName) + ".txt"))
			{
				MonoBehaviour.print("Exist");
			}
			Singleton<MissionReader>.Instance.ReadFromResources(www.text);
			File.WriteAllText(this.getPath(this.fileName) + ".txt", www.text);
		}
		yield break;
	}

	private IEnumerator downloadingUpdate(string url)
	{
		WWW www = new WWW(url);
		while (!www.isDone)
		{
			MonoBehaviour.print(www.progress);
			this.progress.text = string.Concat(new string[]
			{
				this.counter.ToString(),
				"/",
				this.files.Length.ToString(),
				" Installing Update ",
				(www.progress * 100f).ToString(),
				"%"
			});
			yield return null;
		}
		yield return www;
		if (www.error != null)
		{
			this.progress.text = this.counter.ToString() + "/" + this.files.Length.ToString() + " Check your internet connection!";
			this.bar.fillAmount = (float)this.counter * 1f / 3f;
			this.action = "refresh";
			this.ActionButton.GetComponentInChildren<Text>().text = "Refresh";
		}
		else
		{
			this.progress.text = this.counter.ToString() + "/" + this.files.Length.ToString() + " Successfully Updated ... 100%";
			File.WriteAllText(this.getPath(this.fileName) + ".xml", www.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(www.text);
			XmlNode firstChild = xmlDocument.LastChild.FirstChild;
			PlayerPrefs.SetInt(this.fileName + "_version", int.Parse(firstChild.InnerText));
			this.chkFile();
		}
		yield break;
	}

	private void readingXml(string file)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(file);
		XmlNode firstChild = xmlDocument.LastChild.FirstChild;
		this.progress.text = "Version : " + firstChild.InnerText;
	}

	public string[] url;

	public string fileURL;

	public string fileName;

	public string[] files;

	public string filePath;

	public Text progress;

	public Image bar;

	public int counter;

	public string action;

	public Button ActionButton;

	public Image waitingbar;
}
