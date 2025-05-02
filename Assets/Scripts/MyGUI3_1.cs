// dnSpy decompiler from Assembly-CSharp.dll class: MyGUI3_1
using System;
using UnityEngine;

public class MyGUI3_1 : MonoBehaviour
{
	private void Start()
	{
		this.oldAmbientColor = RenderSettings.ambientLight;
		this.oldLightIntensity = this.DirLight.intensity;
		this.anim = this.Target.GetComponent<Animator>();
		this.guiStyleHeader.fontSize = 14;
		this.guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
		EffectSettings component = this.Prefabs[this.current].GetComponent<EffectSettings>();
		if (component != null)
		{
			this.prefabSpeed = component.MoveSpeed;
		}
		this.current = this.CurrentPrefabNomber;
		this.InstanceCurrent(this.GuiStats[this.CurrentPrefabNomber]);
	}

	private void InstanceEffect(Vector3 pos)
	{
		this.currentGo = UnityEngine.Object.Instantiate<GameObject>(this.Prefabs[this.current], pos, this.Prefabs[this.current].transform.rotation);
		this.effectSettings = this.currentGo.GetComponent<EffectSettings>();
		this.effectSettings.Target = this.GetTargetObject(this.GuiStats[this.current]);
		if (this.isHomingMove)
		{
			this.effectSettings.IsHomingMove = this.isHomingMove;
		}
		this.prefabSpeed = this.effectSettings.MoveSpeed;
		this.effectSettings.EffectDeactivated += this.effectSettings_EffectDeactivated;
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.Middle)
		{
			this.currentGo.transform.parent = this.GetTargetObject(MyGUI3_1.GuiStat.Middle).transform;
			this.currentGo.transform.position = this.GetInstancePosition(MyGUI3_1.GuiStat.Middle);
		}
		else
		{
			this.currentGo.transform.parent = base.transform;
		}
		this.effectSettings.CollisionEnter += delegate(object n, CollisionInfo e)
		{
			if (e.Hit.transform != null)
			{
				UnityEngine.Debug.Log(e.Hit.transform.name);
			}
		};
	}

	private GameObject GetTargetObject(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			return this.Target;
		case MyGUI3_1.GuiStat.BallRotate:
			return this.Target;
		case MyGUI3_1.GuiStat.Bottom:
			return this.BottomPosition;
		case MyGUI3_1.GuiStat.Middle:
			this.MiddlePosition.transform.localPosition = this.defaultRobotPos;
			this.MiddlePosition.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			return this.MiddlePosition;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			return this.MiddlePosition.transform.parent.gameObject;
		case MyGUI3_1.GuiStat.Top:
			return this.TopPosition;
		case MyGUI3_1.GuiStat.TopTarget:
			return this.BottomPosition;
		}
		return base.gameObject;
	}

	private void effectSettings_EffectDeactivated(object sender, EventArgs e)
	{
		if (this.GuiStats[this.current] != MyGUI3_1.GuiStat.Middle)
		{
			this.currentGo.transform.position = this.GetInstancePosition(this.GuiStats[this.current]);
		}
		this.isReadyEffect = true;
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 15f, 105f, 30f), "Previous Effect"))
		{
			this.ChangeCurrent(-1);
		}
		if (GUI.Button(new Rect(130f, 15f, 105f, 30f), "Next Effect"))
		{
			this.ChangeCurrent(1);
		}
		if (this.Prefabs[this.current] != null)
		{
			GUI.Label(new Rect(300f, 15f, 100f, 20f), "Prefab name is \"" + this.Prefabs[this.current].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
		}
		if (GUI.Button(new Rect(10f, 60f, 225f, 30f), "Day/Night"))
		{
			this.DirLight.intensity = (this.isDay ? this.oldLightIntensity : 0f);
			RenderSettings.ambientLight = (this.isDay ? this.oldAmbientColor : new Color(0.1f, 0.1f, 0.1f));
			this.isDay = !this.isDay;
		}
		if (GUI.Button(new Rect(10f, 105f, 225f, 30f), "Change environment"))
		{
			if (this.isDefaultPlaneTexture)
			{
				this.Plane1.GetComponent<Renderer>().material = this.PlaneMaterials[0];
				this.Plane2.GetComponent<Renderer>().material = this.PlaneMaterials[0];
			}
			else
			{
				this.Plane1.GetComponent<Renderer>().material = this.PlaneMaterials[1];
				this.Plane2.GetComponent<Renderer>().material = this.PlaneMaterials[2];
			}
			this.isDefaultPlaneTexture = !this.isDefaultPlaneTexture;
		}
		if (this.current <= 40)
		{
			GUI.Label(new Rect(10f, 152f, 225f, 30f), "Ball Speed " + (int)this.prefabSpeed + "m", this.guiStyleHeader);
			this.prefabSpeed = GUI.HorizontalSlider(new Rect(115f, 155f, 120f, 30f), this.prefabSpeed, 1f, 30f);
			this.isHomingMove = GUI.Toggle(new Rect(10f, 190f, 150f, 30f), this.isHomingMove, " Is Homing Move");
			this.effectSettings.MoveSpeed = this.prefabSpeed;
		}
	}

	private void Update()
	{
		this.anim.enabled = this.isHomingMove;
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			this.fps = this.accum / (float)this.frames;
			this.timeleft = this.UpdateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
		if (this.isReadyEffect)
		{
			this.isReadyEffect = false;
			this.currentGo.SetActive(true);
		}
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.BallRotate)
		{
			this.currentGo.transform.localRotation = Quaternion.Euler(0f, Mathf.PingPong(Time.time * 5f, 60f) - 50f, 0f);
		}
		if (this.GuiStats[this.current] == MyGUI3_1.GuiStat.BallRotatex4)
		{
			this.currentGo.transform.localRotation = Quaternion.Euler(0f, Mathf.PingPong(Time.time * 30f, 100f) - 70f, 0f);
		}
	}

	private void InstanceCurrent(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.BallRotate:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.BallRotatex4:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(base.transform.position);
			break;
		case MyGUI3_1.GuiStat.Bottom:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.BottomPosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.Middle:
			this.MiddlePosition.SetActive(true);
			this.InstanceEffect(this.MiddlePosition.transform.parent.transform.position);
			break;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.MiddlePosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.Top:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.TopPosition.transform.position);
			break;
		case MyGUI3_1.GuiStat.TopTarget:
			this.MiddlePosition.SetActive(false);
			this.InstanceEffect(this.TopPosition.transform.position);
			break;
		}
	}

	private Vector3 GetInstancePosition(MyGUI3_1.GuiStat stat)
	{
		switch (stat)
		{
		case MyGUI3_1.GuiStat.Ball:
			return base.transform.position;
		case MyGUI3_1.GuiStat.BallRotate:
			return base.transform.position;
		case MyGUI3_1.GuiStat.BallRotatex4:
			return base.transform.position;
		case MyGUI3_1.GuiStat.Bottom:
			return this.BottomPosition.transform.position;
		case MyGUI3_1.GuiStat.Middle:
			return this.MiddlePosition.transform.parent.transform.position;
		case MyGUI3_1.GuiStat.MiddleWithoutRobot:
			return this.MiddlePosition.transform.parent.transform.position;
		case MyGUI3_1.GuiStat.Top:
			return this.TopPosition.transform.position;
		case MyGUI3_1.GuiStat.TopTarget:
			return this.TopPosition.transform.position;
		default:
			return base.transform.position;
		}
	}

	private void ChangeCurrent(int delta)
	{
		UnityEngine.Object.Destroy(this.currentGo);
		base.CancelInvoke("InstanceDefaulBall");
		this.current += delta;
		if (this.current > this.Prefabs.Length - 1)
		{
			this.current = 0;
		}
		else if (this.current < 0)
		{
			this.current = this.Prefabs.Length - 1;
		}
		if (this.effectSettings != null)
		{
			this.effectSettings.EffectDeactivated -= this.effectSettings_EffectDeactivated;
		}
		this.MiddlePosition.SetActive(this.GuiStats[this.current] == MyGUI3_1.GuiStat.Middle);
		this.InstanceEffect(this.GetInstancePosition(this.GuiStats[this.current]));
	}

	public int CurrentPrefabNomber;

	public float UpdateInterval = 0.5f;

	public Light DirLight;

	public GameObject Target;

	public GameObject TopPosition;

	public GameObject MiddlePosition;

	public Vector3 defaultRobotPos;

	public GameObject BottomPosition;

	public GameObject Plane1;

	public GameObject Plane2;

	public Material[] PlaneMaterials;

	public MyGUI3_1.GuiStat[] GuiStats;

	public GameObject[] Prefabs;

	private float oldLightIntensity;

	private Color oldAmbientColor;

	private GameObject currentGo;

	private bool isDay;

	private bool isHomingMove;

	private bool isDefaultPlaneTexture;

	private int current;

	private Animator anim;

	private float prefabSpeed = 4f;

	private EffectSettings effectSettings;

	private bool isReadyEffect;

	private Quaternion defaultRobotRotation;

	private float accum;

	private int frames;

	private float timeleft;

	private float fps;

	private GUIStyle guiStyleHeader = new GUIStyle();

	public enum GuiStat
	{
		Ball,
		BallRotate,
		BallRotatex4,
		Bottom,
		Middle,
		MiddleWithoutRobot,
		Top,
		TopTarget
	}
}
