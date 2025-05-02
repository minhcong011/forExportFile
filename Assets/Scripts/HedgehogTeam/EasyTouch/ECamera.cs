// dnSpy decompiler from Assembly-CSharp.dll class: HedgehogTeam.EasyTouch.ECamera
using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	[Serializable]
	public class ECamera
	{
		public ECamera(Camera cam, bool gui)
		{
			this.camera = cam;
			this.guiCamera = gui;
		}

		public Camera camera;

		public bool guiCamera;
	}
}
