// dnSpy decompiler from Assembly-UnityScript.dll class: ScreenWipe
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class ScreenWipe : MonoBehaviour
{
	public ScreenWipe()
	{
		this.planeResolution = 90;
	}

	public virtual void Awake()
	{
		if (ScreenWipe.use)
		{
			UnityEngine.Debug.LogWarning("Only one instance of ScreenCrossFadePro is allowed");
		}
		else
		{
			ScreenWipe.use = this;
			this.enabled = false;
		}
	}

	public virtual void OnGUI()
	{
		GUI.depth = -9999999;
		float a = this.alpha;
		Color color = GUI.color;
		float num = color.a = a;
		Color color2 = GUI.color = color;
		GUI.DrawTexture(new Rect((float)0, (float)0, (float)Screen.width, (float)Screen.height), this.tex);
	}

	public virtual IEnumerator AlphaTimer(float time)
	{
		return new ScreenWipe._0024AlphaTimer_002467(time, this).GetEnumerator();
	}

	public virtual void CameraSetup(Camera cam1, Camera cam2, bool cam1Active, bool enableThis)
	{
		if (enableThis)
		{
			this.enabled = true;
		}
		cam1.gameObject.active = cam1Active;
		cam2.gameObject.SetActive(true);
		AudioListener audioListener = (AudioListener)cam2.GetComponent(typeof(AudioListener));
		if (audioListener)
		{
			this.reEnableListener = audioListener.enabled;
			audioListener.enabled = false;
		}
	}

	public virtual void CameraCleanup(Camera cam1, Camera cam2)
	{
		AudioListener audioListener = (AudioListener)cam2.GetComponent(typeof(AudioListener));
		if (audioListener && this.reEnableListener)
		{
			audioListener.enabled = true;
		}
		cam1.gameObject.SetActive(false);
		this.enabled = false;
	}

	public virtual IEnumerator CrossFadePro(Camera cam1, Camera cam2, float time)
	{
		return new ScreenWipe._0024CrossFadePro_002474(cam1, cam2, time, this).GetEnumerator();
	}

	public virtual IEnumerator CrossFade(Camera cam1, Camera cam2, float time)
	{
		return new ScreenWipe._0024CrossFade_002483(cam1, cam2, time, this).GetEnumerator();
	}

	public virtual IEnumerator CrossFade(Camera cam1, Camera cam2, float time, AnimationCurve _curve)
	{
		return new ScreenWipe._0024CrossFade_002492(cam1, cam2, time, _curve, this).GetEnumerator();
	}

	public virtual IEnumerator RectWipe(Camera cam1, Camera cam2, float time, ZoomType zoom)
	{
		return new ScreenWipe._0024RectWipe_0024103(cam1, cam2, time, zoom, this).GetEnumerator();
	}

	public virtual IEnumerator RectWipe(Camera cam1, Camera cam2, float time, ZoomType zoom, AnimationCurve _curve)
	{
		return new ScreenWipe._0024RectWipe_0024114(cam1, cam2, time, zoom, _curve, this).GetEnumerator();
	}

	public virtual IEnumerator ShapeWipe(Camera cam1, Camera cam2, float time, ZoomType zoom, Mesh mesh, float rotateAmount)
	{
		return new ScreenWipe._0024ShapeWipe_0024134(cam1, cam2, time, zoom, mesh, rotateAmount, this).GetEnumerator();
	}

	public virtual IEnumerator ShapeWipe(Camera cam1, Camera cam2, float time, ZoomType zoom, Mesh mesh, float rotateAmount, AnimationCurve _curve)
	{
		return new ScreenWipe._0024ShapeWipe_0024149(cam1, cam2, time, zoom, mesh, rotateAmount, _curve, this).GetEnumerator();
	}

	public virtual IEnumerator SquishWipe(Camera cam1, Camera cam2, float time, TransitionType transitionType)
	{
		return new ScreenWipe._0024SquishWipe_0024173(cam1, cam2, time, transitionType, this).GetEnumerator();
	}

	public virtual IEnumerator SquishWipe(Camera cam1, Camera cam2, float time, TransitionType transitionType, AnimationCurve _curve)
	{
		return new ScreenWipe._0024SquishWipe_0024184(cam1, cam2, time, transitionType, _curve, this).GetEnumerator();
	}

	public virtual void InitializeDreamWipe()
	{
		if (!this.planeMaterial || !this.plane)
		{
			this.planeMaterial = new Material("Shader \"Unlit2Pass\" {" + "Properties {" + "\t_Color (\"Main Color\", Color) = (1,1,1,1)" + "\t_Tex1 (\"Base\", Rect) = \"white\" {}" + "\t_Tex2 (\"Base\", Rect) = \"white\" {}" + "}" + "Category {" + "\tZWrite Off Alphatest Greater 0 ColorMask RGB Lighting Off" + "\tTags {\"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\"}" + "\tBlend SrcAlpha OneMinusSrcAlpha" + "\tSubShader {" + "\t\tPass {SetTexture [_Tex2]}" + "\t\tPass {SetTexture [_Tex1] {constantColor [_Color] Combine texture * constant, texture * constant}}" + "\t}" + "}}");
			this.plane = new GameObject("Plane", new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			this.plane.GetComponent<Renderer>().material = this.planeMaterial;
			this.plane.GetComponent<Renderer>().castShadows = false;
			this.plane.GetComponent<Renderer>().receiveShadows = false;
			this.plane.GetComponent<Renderer>().enabled = false;
			Mesh mesh = new Mesh();
			(((MeshFilter)this.plane.GetComponent(typeof(MeshFilter))) as MeshFilter).mesh = mesh;
			this.planeResolution = Mathf.Clamp(this.planeResolution, 1, 16380);
			this.baseVertices = new Vector3[4 * this.planeResolution + 4];
			this.newVertices = new Vector3[this.baseVertices.Length];
			Vector2[] array = new Vector2[this.baseVertices.Length];
			int[] array2 = new int[18 * this.planeResolution];
			int num = 0;
			float num2 = (float)0;
			for (num2 = (float)0; num2 <= (float)this.planeResolution; num2 += (float)1)
			{
				float num3 = 1f / (float)this.planeResolution * num2;
				array[num] = new Vector2((float)0, 1f - num3);
				Vector3[] array3 = this.baseVertices;
				int num4;
				num = (num4 = num) + 1;
				array3[num4] = new Vector3(-1f, 0.5f - num3, (float)0);
				array[num] = new Vector2((float)0, 1f - num3);
				Vector3[] array4 = this.baseVertices;
				int num5;
				num = (num5 = num) + 1;
				array4[num5] = new Vector3(-0.5f, 0.5f - num3, (float)0);
				array[num] = new Vector2(1f, 1f - num3);
				Vector3[] array5 = this.baseVertices;
				int num6;
				num = (num6 = num) + 1;
				array5[num6] = new Vector3(0.5f, 0.5f - num3, (float)0);
				array[num] = new Vector2(1f, 1f - num3);
				Vector3[] array6 = this.baseVertices;
				int num7;
				num = (num7 = num) + 1;
				array6[num7] = new Vector3(1f, 0.5f - num3, (float)0);
			}
			num = 0;
			for (int i = 0; i < this.planeResolution; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int[] array7 = array2;
					int num8;
					num = (num8 = num) + 1;
					array7[num8] = i * 4 + j;
					int[] array8 = array2;
					int num9;
					num = (num9 = num) + 1;
					array8[num9] = i * 4 + j + 1;
					int[] array9 = array2;
					int num10;
					num = (num10 = num) + 1;
					array9[num10] = (i + 1) * 4 + j;
					int[] array10 = array2;
					int num11;
					num = (num11 = num) + 1;
					array10[num11] = (i + 1) * 4 + j;
					int[] array11 = array2;
					int num12;
					num = (num12 = num) + 1;
					array11[num12] = i * 4 + j + 1;
					int[] array12 = array2;
					int num13;
					num = (num13 = num) + 1;
					array12[num13] = (i + 1) * 4 + j + 1;
				}
			}
			mesh.vertices = this.baseVertices;
			mesh.uv = array;
			mesh.triangles = array2;
			this.renderTex = new RenderTexture(Screen.width, Screen.height, 24);
			this.renderTex2 = new RenderTexture(Screen.width, Screen.height, 24);
			this.renderTex.filterMode = (this.renderTex2.filterMode = FilterMode.Point);
			this.planeMaterial.SetTexture("_Tex1", this.renderTex);
			this.planeMaterial.SetTexture("_Tex2", this.renderTex2);
		}
	}

	public virtual IEnumerator DreamWipe(Camera cam1, Camera cam2, float time)
	{
		return new ScreenWipe._0024DreamWipe_0024205(cam1, cam2, time, this).GetEnumerator();
	}

	public virtual IEnumerator DreamWipe(Camera cam1, Camera cam2, float time, float waveScale, float waveFrequency)
	{
		return new ScreenWipe._0024DreamWipe_0024214(cam1, cam2, time, waveScale, waveFrequency, this).GetEnumerator();
	}

	public virtual void Main()
	{
	}

	private Texture tex;

	private RenderTexture renderTex;

	private Texture2D tex2D;

	private float alpha;

	private bool reEnableListener;

	private Material shapeMaterial;

	private Transform shape;

	private AnimationCurve curve;

	private bool useCurve;

	[NonSerialized]
	public static ScreenWipe use;

	public int planeResolution;

	private Vector3[] baseVertices;

	private Vector3[] newVertices;

	private Material planeMaterial;

	private GameObject plane;

	private RenderTexture renderTex2;

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024AlphaTimer_002467 : List<object>
	{
		public _0024AlphaTimer_002467(float time, ScreenWipe self_)
		{
			this._0024time_002472 = time;
			this._0024self__002473 = self_;
		}

		//public override IEnumerator<object> GetEnumerator()
		//{
  //          return null;// new ScreenWipe._0024AlphaTimer_002467._0024(this._0024time_002472, this._0024self__002473);
		//}

		internal float _0024time_002472;

		internal ScreenWipe _0024self__002473;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024CrossFadePro_002474 : List<Coroutine>
	{
		public _0024CrossFadePro_002474(Camera cam1, Camera cam2, float time, ScreenWipe self_)
		{
			this._0024cam1_002479 = cam1;
			this._0024cam2_002480 = cam2;
			this._0024time_002481 = time;
			this._0024self__002482 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024CrossFadePro_002474._0024(this._0024cam1_002479, this._0024cam2_002480, this._0024time_002481, this._0024self__002482);
  //      }

		internal Camera _0024cam1_002479;

		internal Camera _0024cam2_002480;

		internal float _0024time_002481;

		internal ScreenWipe _0024self__002482;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024CrossFade_002483 : List<Coroutine>
	{
		public _0024CrossFade_002483(Camera cam1, Camera cam2, float time, ScreenWipe self_)
		{
			this._0024cam1_002488 = cam1;
			this._0024cam2_002489 = cam2;
			this._0024time_002490 = time;
			this._0024self__002491 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024CrossFade_002483._0024(this._0024cam1_002488, this._0024cam2_002489, this._0024time_002490, this._0024self__002491);
  //      }

		internal Camera _0024cam1_002488;

		internal Camera _0024cam2_002489;

		internal float _0024time_002490;

		internal ScreenWipe _0024self__002491;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024CrossFade_002492 : List<Coroutine>
	{
		public _0024CrossFade_002492(Camera cam1, Camera cam2, float time, AnimationCurve _curve, ScreenWipe self_)
		{
			this._0024cam1_002498 = cam1;
			this._0024cam2_002499 = cam2;
			this._0024time_0024100 = time;
			this._0024_curve_0024101 = _curve;
			this._0024self__0024102 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024CrossFade_002492._0024(this._0024cam1_002498, this._0024cam2_002499, this._0024time_0024100, this._0024_curve_0024101, this._0024self__0024102);
  //      }

		internal Camera _0024cam1_002498;

		internal Camera _0024cam2_002499;

		internal float _0024time_0024100;

		internal AnimationCurve _0024_curve_0024101;

		internal ScreenWipe _0024self__0024102;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024RectWipe_0024103 : List<Coroutine>
	{
		public _0024RectWipe_0024103(Camera cam1, Camera cam2, float time, ZoomType zoom, ScreenWipe self_)
		{
			this._0024cam1_0024109 = cam1;
			this._0024cam2_0024110 = cam2;
			this._0024time_0024111 = time;
			this._0024zoom_0024112 = zoom;
			this._0024self__0024113 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024RectWipe_0024103._0024(this._0024cam1_0024109, this._0024cam2_0024110, this._0024time_0024111, this._0024zoom_0024112, this._0024self__0024113);
  //      }

		internal Camera _0024cam1_0024109;

		internal Camera _0024cam2_0024110;

		internal float _0024time_0024111;

		internal ZoomType _0024zoom_0024112;

		internal ScreenWipe _0024self__0024113;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024RectWipe_0024114 : List<object>
	{
		public _0024RectWipe_0024114(Camera cam1, Camera cam2, float time, ZoomType zoom, AnimationCurve _curve, ScreenWipe self_)
		{
			this._0024cam1_0024128 = cam1;
			this._0024cam2_0024129 = cam2;
			this._0024time_0024130 = time;
			this._0024zoom_0024131 = zoom;
			this._0024_curve_0024132 = _curve;
			this._0024self__0024133 = self_;
		}

		//public override IEnumerator<object> GetEnumerator()
		//{
  //          return null;//	return new ScreenWipe._0024RectWipe_0024114._0024(this._0024cam1_0024128, this._0024cam2_0024129, this._0024time_0024130, this._0024zoom_0024131, this._0024_curve_0024132, this._0024self__0024133);
  //      }

		internal Camera _0024cam1_0024128;

		internal Camera _0024cam2_0024129;

		internal float _0024time_0024130;

		internal ZoomType _0024zoom_0024131;

		internal AnimationCurve _0024_curve_0024132;

		internal ScreenWipe _0024self__0024133;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024ShapeWipe_0024134 : List<Coroutine>
	{
		public _0024ShapeWipe_0024134(Camera cam1, Camera cam2, float time, ZoomType zoom, Mesh mesh, float rotateAmount, ScreenWipe self_)
		{
			this._0024cam1_0024142 = cam1;
			this._0024cam2_0024143 = cam2;
			this._0024time_0024144 = time;
			this._0024zoom_0024145 = zoom;
			this._0024mesh_0024146 = mesh;
			this._0024rotateAmount_0024147 = rotateAmount;
			this._0024self__0024148 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024ShapeWipe_0024134._0024(this._0024cam1_0024142, this._0024cam2_0024143, this._0024time_0024144, this._0024zoom_0024145, this._0024mesh_0024146, this._0024rotateAmount_0024147, this._0024self__0024148);
  //      }

		internal Camera _0024cam1_0024142;

		internal Camera _0024cam2_0024143;

		internal float _0024time_0024144;

		internal ZoomType _0024zoom_0024145;

		internal Mesh _0024mesh_0024146;

		internal float _0024rotateAmount_0024147;

		internal ScreenWipe _0024self__0024148;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024ShapeWipe_0024149 : List<object>
	{
		public _0024ShapeWipe_0024149(Camera cam1, Camera cam2, float time, ZoomType zoom, Mesh mesh, float rotateAmount, AnimationCurve _curve, ScreenWipe self_)
		{
			this._0024cam1_0024165 = cam1;
			this._0024cam2_0024166 = cam2;
			this._0024time_0024167 = time;
			this._0024zoom_0024168 = zoom;
			this._0024mesh_0024169 = mesh;
			this._0024rotateAmount_0024170 = rotateAmount;
			this._0024_curve_0024171 = _curve;
			this._0024self__0024172 = self_;
		}

		//public override IEnumerator<object> GetEnumerator()
		//{
  //          return null;//	return new ScreenWipe._0024ShapeWipe_0024149._0024(this._0024cam1_0024165, this._0024cam2_0024166, this._0024time_0024167, this._0024zoom_0024168, this._0024mesh_0024169, this._0024rotateAmount_0024170, this._0024_curve_0024171, this._0024self__0024172);
  //      }

		internal Camera _0024cam1_0024165;

		internal Camera _0024cam2_0024166;

		internal float _0024time_0024167;

		internal ZoomType _0024zoom_0024168;

		internal Mesh _0024mesh_0024169;

		internal float _0024rotateAmount_0024170;

		internal AnimationCurve _0024_curve_0024171;

		internal ScreenWipe _0024self__0024172;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024SquishWipe_0024173 : List<Coroutine>
	{
		public _0024SquishWipe_0024173(Camera cam1, Camera cam2, float time, TransitionType transitionType, ScreenWipe self_)
		{
			this._0024cam1_0024179 = cam1;
			this._0024cam2_0024180 = cam2;
			this._0024time_0024181 = time;
			this._0024transitionType_0024182 = transitionType;
			this._0024self__0024183 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024SquishWipe_0024173._0024(this._0024cam1_0024179, this._0024cam2_0024180, this._0024time_0024181, this._0024transitionType_0024182, this._0024self__0024183);
  //      }

		internal Camera _0024cam1_0024179;

		internal Camera _0024cam2_0024180;

		internal float _0024time_0024181;

		internal TransitionType _0024transitionType_0024182;

		internal ScreenWipe _0024self__0024183;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024SquishWipe_0024184 : List<object>
	{
		public _0024SquishWipe_0024184(Camera cam1, Camera cam2, float time, TransitionType transitionType, AnimationCurve _curve, ScreenWipe self_)
		{
			this._0024cam1_0024199 = cam1;
			this._0024cam2_0024200 = cam2;
			this._0024time_0024201 = time;
			this._0024transitionType_0024202 = transitionType;
			this._0024_curve_0024203 = _curve;
			this._0024self__0024204 = self_;
		}

		//public override IEnumerator<object> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024SquishWipe_0024184._0024(this._0024cam1_0024199, this._0024cam2_0024200, this._0024time_0024201, this._0024transitionType_0024202, this._0024_curve_0024203, this._0024self__0024204);
  //      }

		internal Camera _0024cam1_0024199;

		internal Camera _0024cam2_0024200;

		internal float _0024time_0024201;

		internal TransitionType _0024transitionType_0024202;

		internal AnimationCurve _0024_curve_0024203;

		internal ScreenWipe _0024self__0024204;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024DreamWipe_0024205 : List<Coroutine>
	{
		public _0024DreamWipe_0024205(Camera cam1, Camera cam2, float time, ScreenWipe self_)
		{
			this._0024cam1_0024210 = cam1;
			this._0024cam2_0024211 = cam2;
			this._0024time_0024212 = time;
			this._0024self__0024213 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024DreamWipe_0024205._0024(this._0024cam1_0024210, this._0024cam2_0024211, this._0024time_0024212, this._0024self__0024213);
  //      }

		internal Camera _0024cam1_0024210;

		internal Camera _0024cam2_0024211;

		internal float _0024time_0024212;

		internal ScreenWipe _0024self__0024213;
	}

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024DreamWipe_0024214 : List<object>
	{
		public _0024DreamWipe_0024214(Camera cam1, Camera cam2, float time, float waveScale, float waveFrequency, ScreenWipe self_)
		{
			this._0024cam1_0024230 = cam1;
			this._0024cam2_0024231 = cam2;
			this._0024time_0024232 = time;
			this._0024waveScale_0024233 = waveScale;
			this._0024waveFrequency_0024234 = waveFrequency;
			this._0024self__0024235 = self_;
		}

		//public override IEnumerator<object> GetEnumerator()
		//{
  //          return null;//return new ScreenWipe._0024DreamWipe_0024214._0024(this._0024cam1_0024230, this._0024cam2_0024231, this._0024time_0024232, this._0024waveScale_0024233, this._0024waveFrequency_0024234, this._0024self__0024235);
  //      }

		internal Camera _0024cam1_0024230;

		internal Camera _0024cam2_0024231;

		internal float _0024time_0024232;

		internal float _0024waveScale_0024233;

		internal float _0024waveFrequency_0024234;

		internal ScreenWipe _0024self__0024235;
	}
}
