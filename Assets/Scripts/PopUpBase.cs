// dnSpy decompiler from Assembly-CSharp.dll class: PopUpBase
using System;

[Serializable]
public class PopUpBase
{
	public PopUpBase()
	{
		this.name = string.Empty;
		this.title = string.Empty;
		this.description = string.Empty;
		this.isCancelBtn = false;
		this.screenStayTime = 0f;
	}

	public string name;

	public string title;

	public string description;

	public bool isCancelBtn;

	public bool isOkBtn;

	public float screenStayTime;

	public Action<string> callback;
}
