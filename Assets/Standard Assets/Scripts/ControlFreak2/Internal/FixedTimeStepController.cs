// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.FixedTimeStepController
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class FixedTimeStepController
	{
		public FixedTimeStepController(int framesPerSecond)
		{
			this.SetFPS(framesPerSecond);
			this.Reset();
		}

		public float GetDeltaTime()
		{
			return this.fixedDeltaTime;
		}

		public float GetDeltaTimeCombined()
		{
			return this.fixedDeltaTimeCombined;
		}

		public int GetFrameCount()
		{
			return this.totalFrameCount;
		}

		public int GetFrameSteps()
		{
			return this.frameSteps;
		}

		public float GetTime()
		{
			return this.fixedTime;
		}

		public void Reset()
		{
			this.fixedTime = 0f;
			this.totalFrameCount = 0;
			this.frameSteps = 0;
			this.fixedDeltaTimeCombined = 0f;
		}

		public void SetFPS(int framesPerSecond)
		{
			this.fixedDeltaTime = 1f / (float)Mathf.Max(1, framesPerSecond);
		}

		public void Update(float deltaTime)
		{
			this.timeAccum += deltaTime;
			this.fixedDeltaTimeCombined = 0f;
			this.frameSteps = 0;
			while (this.timeAccum > this.fixedDeltaTime)
			{
				this.fixedDeltaTimeCombined += this.fixedDeltaTime;
				this.frameSteps++;
				this.timeAccum -= this.fixedDeltaTime;
			}
			this.totalFrameCount += this.frameSteps;
			this.SetStaticData();
		}

		public void Execute(Action updateCallback)
		{
			if (this.frameSteps <= 0)
			{
				return;
			}
			this.SetStaticData();
			for (int i = 0; i < this.frameSteps; i++)
			{
				updateCallback();
			}
		}

		public void SetStaticData()
		{
			FixedTimeStepController.deltaTime = this.fixedDeltaTime;
		}

		public static float deltaTime;

		private float fixedTime;

		private float fixedDeltaTime;

		private float fixedDeltaTimeCombined;

		private float timeAccum;

		private int totalFrameCount;

		private int frameSteps;
	}
}
