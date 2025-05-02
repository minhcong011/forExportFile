// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.GameState
using System;
using UnityEngine;

namespace ControlFreak2
{
	public abstract class GameState : MonoBehaviour
	{
		public bool IsRunning()
		{
			return this.isRunning;
		}

		protected virtual void OnStartState(GameState parentState)
		{
			this.parentState = parentState;
			this.isRunning = true;
		}

		protected virtual void OnExitState()
		{
			this.isRunning = false;
			if (this.subState != null)
			{
				this.subState.OnExitState();
			}
		}

		protected virtual void OnPreSubStateStart(GameState prevState, GameState nextState)
		{
		}

		protected virtual void OnPostSubStateStart(GameState prevState, GameState nextState)
		{
		}

		protected virtual void OnUpdateState()
		{
			if (this.subState != null)
			{
				this.subState.OnUpdateState();
			}
		}

		protected virtual void OnFixedUpdateState()
		{
			if (this.subState != null)
			{
				this.subState.OnFixedUpdateState();
			}
		}

		public void StartSubState(GameState state)
		{
			if (this.FindStateInHierarchy(state))
			{
				throw new Exception(string.Concat(new string[]
				{
					"Gamestate (",
					base.name,
					") tries to start sub state (",
					state.name,
					") that's already running!"
				}));
			}
			GameState prevState = this.subState;
			this.OnPreSubStateStart(prevState, state);
			if (this.subState != null)
			{
				this.subState.OnExitState();
			}
			this.subState = state;
			if (state != null)
			{
				this.subState.OnStartState(this);
			}
			this.OnPostSubStateStart(prevState, state);
		}

		protected bool FindStateInHierarchy(GameState state)
		{
			if (state == null)
			{
				return false;
			}
			GameState gameState = this;
			while (gameState != null)
			{
				if (gameState == state)
				{
					return true;
				}
				gameState = gameState.parentState;
			}
			return false;
		}

		public void EndState()
		{
			if (this.parentState != null)
			{
				this.parentState.EndSubState();
			}
		}

		public void EndSubState()
		{
			this.StartSubState(null);
		}

		public GameState GetSubState()
		{
			return this.subState;
		}

		public bool IsSubStateRunning()
		{
			return this.subState != null;
		}

		protected GameState parentState;

		protected GameState subState;

		protected bool isRunning;
	}
}
