// dnSpy decompiler from Assembly-CSharp.dll class: Console
using System;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
	private void OnEnable()
	{
		Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
	}

	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	private void Awake()
	{
		this.backGroundTexture = Utils1.CreateTexture1x1(this.backGroundColor);
	}

	private void Start()
	{
		Utils1.CLog(string.Empty, "Debug", "green");
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			gameObject.SendMessage("OnConsoleStart", SendMessageOptions.DontRequireReceiver);
		}
		this.fpsCalculator.FPS_Start();
	}

	private void Update()
	{
		this.fpsCalculator.FPS_Update();
		if (UnityEngine.Input.GetKeyDown(this.toggleKey))
		{
			this.showUI_Console = !this.showUI_Console;
		}
	}

	private void OnGUI()
	{
		if (this.showUI_Console)
		{
			this.UI_Console();
		}
		this.UI_Stats();
	}

	private void UI_Console()
	{
		GUI.depth = 0;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height * 0.75f), this.backGroundTexture);
		this.scrollPosition = GUI.BeginScrollView(new Rect(10f, 10f, (float)(Screen.width - 10), (float)Screen.height * 0.75f - 10f), this.scrollPosition, new Rect(10f, 10f, (float)(Screen.width - 10), (float)(10 + this.messages.Count * 12)), false, false);
		for (int i = 0; i < this.messages.Count; i++)
		{
			GUI.Label(new Rect(10f, (float)(10 + i * 12), (float)(Screen.width - 10), 12f), this.messages[i].message, this.textStyle);
		}
		GUI.EndScrollView();
	}

	private void UI_Stats()
	{
		GUI.Label(new Rect(10f, (float)(Screen.height - 65), 300f, 20f), this.fpsCalculator.output, this.textStyle);
	}

	private void HandleLog(string message, string stackTrace, LogType type)
	{
		global::Console.ConsoleMessage item = new global::Console.ConsoleMessage(message, stackTrace, type);
		this.messages.Add(item);
	}

	public FPS_Calculator fpsCalculator;

	public KeyCode toggleKey = KeyCode.BackQuote;

	public GUIStyle textStyle;

	public Color backGroundColor;

	private List<global::Console.ConsoleMessage> messages = new List<global::Console.ConsoleMessage>();

	private bool showUI_Console;

	private bool showUI_Stats;

	private Vector2 scrollPosition = Vector2.zero;

	private Texture2D backGroundTexture;

	public struct ConsoleMessage
	{
		public ConsoleMessage(string message, string stackTrace, LogType type)
		{
			this.message = message;
			this.stackTrace = stackTrace;
			this.type = type;
		}

		public string message;

		public string stackTrace;

		public LogType type;
	}
}
