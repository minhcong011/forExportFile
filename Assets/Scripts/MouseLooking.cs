// dnSpy decompiler from Assembly-CSharp.dll class: MouseLooking
using System;
using UnityEngine;

public class MouseLooking : MonoBehaviour
{
	private void Awake()
	{
	}

	public void setControllerPositionTemp()
	{
		if (UnityEngine.Input.touchCount > 0 && Singleton<GameController>.Instance.touchIndex <= UnityEngine.Input.touchCount)
		{
			this.controllerPositionNext = new Vector2(UnityEngine.Input.GetTouch(Singleton<GameController>.Instance.touchIndex - 1).position.x, (float)Screen.height - UnityEngine.Input.GetTouch(Singleton<GameController>.Instance.touchIndex - 1).position.y);
			this.controllerPositionTemp = this.controllerPositionNext;
		}
	}

	private void Start()
	{
		this.refGameSceneController = Singleton<GameController>.Instance.gameSceneController;
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.originalRotation = base.transform.localRotation;
		base.Invoke("SetOriginalRot", 2f);
		this.setSettingsSensitivity();
	}

	private void SetOriginalRot()
	{
		this.originalRotation = base.transform.localRotation;
	}

	public void setSettingsSensitivity()
	{
		this.settingsSensitivity = Constants.getSensitivity();
		if (this.settingsSensitivity > 1.5f)
		{
			this.settingsSensitivity = 1.5f;
		}
		if (this.settingsSensitivity <= 0.5f)
		{
			this.settingsSensitivity = 0.5f;
		}
		this.sensitivityX *= this.settingsSensitivity;
	}

	private void Update()
	{
		if (this.refGameSceneController.isGameOver || this.refGameSceneController.isGameCompleted || !this.refGameSceneController.isGameStarted)
		{
			this.updateParams = false;
			return;
		}
		if (!Singleton<GameController>.Instance.canTouch)
		{
			this.index = -1;
			return;
		}
		if (UnityEngine.Input.touchCount <= 0)
		{
			this.index = -1;
		}
		this.sensitivityY = this.sensitivityX;
		this.stunY += (0f - this.stunY) / 20f;
		for (int i = 0; i < UnityEngine.Input.touchCount; i++)
		{
			Touch touch = Input.touches[i];
			if (touch.phase == TouchPhase.Began)
			{
				this.controllerPositionNext = new Vector2(touch.position.x, (float)Screen.height - touch.position.y);
				this.controllerPositionTemp = this.controllerPositionNext;
				this.index = i;
			}
			else if (this.index == i)
			{
				this.controllerPositionNext = new Vector2(touch.position.x, (float)Screen.height - touch.position.y);
				Vector2 vector = this.controllerPositionNext - this.controllerPositionTemp;
				this.rotationX = this.rotationXtemp + vector.x * this.sensitivityX * Time.deltaTime;
				this.rotationY = this.rotationYtemp + (float)this.dir * vector.y * this.sensitivityY * Time.deltaTime;
				this.controllerPositionTemp = Vector2.Lerp(this.controllerPositionTemp, this.controllerPositionNext, 0.5f);
				if (touch.phase == TouchPhase.Ended)
				{
					if (UnityEngine.Input.touchCount == 2 && this.index == 0)
					{
						this.controllerPositionNext = new Vector2(Input.touches[1].position.x, (float)Screen.height - Input.touches[1].position.y);
						this.controllerPositionTemp = this.controllerPositionNext;
						this.index = 0;
						return;
					}
					if (UnityEngine.Input.touchCount == 1)
					{
						this.controllerPositionNext = new Vector2(Input.touches[0].position.x, (float)Screen.height - Input.touches[0].position.y);
						this.controllerPositionTemp = this.controllerPositionNext;
						this.index = 0;
						return;
					}
				}
				if (vector.magnitude < 2f)
				{
					return;
				}
				break;
			}
		}
		if (UnityEngine.Input.touchCount == 1 && this.index != 0)
		{
			this.controllerPositionNext = new Vector2(Input.touches[0].position.x, (float)Screen.height - Input.touches[0].position.y);
			this.controllerPositionTemp = this.controllerPositionNext;
			this.index = 0;
		}
		if (this.rotationX >= 360f)
		{
			this.rotationX = 0f;
			this.rotationXtemp = 0f;
		}
		if (this.rotationX <= -360f)
		{
			this.rotationX = 0f;
			this.rotationXtemp = 0f;
		}
		this.rotationX = MouseLooking.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
		this.rotationY = MouseLooking.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
		Quaternion rhs = Quaternion.AngleAxis(this.rotationX, Vector3.up);
		Quaternion rhs2 = Quaternion.AngleAxis(this.rotationY + this.stunY, Vector3.left);
		if (this.root != null)
		{
			base.transform.localRotation = this.originalRotation * rhs;
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
			this.root.localRotation = this.originalRotation * rhs2;
			this.root.localEulerAngles = new Vector3(this.root.localEulerAngles.x, 0f, 0f);
		}
		else
		{
			base.transform.localRotation = this.originalRotation * rhs * rhs2;
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, 0f);
		}
		this.rotationXtemp = this.rotationX;
		this.rotationYtemp = this.rotationY;
	}

	private void Stun(float val)
	{
		this.stunY = val;
	}

	private void LateUpdate()
	{
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public float sensitivityX = 1f;

	public float sensitivityY = 1f;

	public float minimumX = -360f;

	public float maximumX = 360f;

	public float minimumY = -60f;

	public float maximumY = 60f;

	public float delayMouse = 1f;

	private float rotationX;

	public float rotationY;

	private float rotationXtemp;

	private float rotationYtemp;

	private Quaternion originalRotation;

	private float stunY;

	private Vector2 controllerPositionTemp;

	private Vector2 controllerPositionNext;

	public Texture2D tex;

	private Rect bounds;

	private int width;

	public int dir = 1;

	public Transform root;

	public float settingsSensitivity = 1f;

	private GameSceneController refGameSceneController;

	private bool updateParams;

	private int index = -1;
}
