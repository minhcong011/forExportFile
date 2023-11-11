// ILSpyBased#2
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Utility
{
    public class TimedObjectActivator : MonoBehaviour
    {
        public enum Action
        {
            Activate,
            Deactivate,
            Destroy,
            ReloadLevel,
            Call
        }

        [Serializable]
        public class Entry
        {
            public GameObject target;

            public Action action;

            public float delay;
        }

        [Serializable]
        public class Entries
        {
            public Entry[] entries;
        }

        public Entries entries = new Entries();

        private void Awake()
        {
            Entry[] array = this.entries.entries;
            foreach (Entry entry in array)
            {
                switch (entry.action)
                {
                    case Action.Activate:
                        base.StartCoroutine(this.Activate(entry));
                        break;
                    case Action.Deactivate:
                        base.StartCoroutine(this.Deactivate(entry));
                        break;
                    case Action.Destroy:
                        UnityEngine.Object.Destroy(entry.target, entry.delay);
                        break;
                    case Action.ReloadLevel:
                        base.StartCoroutine(this.ReloadLevel(entry));
                        break;
                }
            }
        }

        private IEnumerator Activate(Entry entry)
        {
            yield return (object)new WaitForSeconds(entry.delay);
            entry.target.SetActive(true);
        }

        private IEnumerator Deactivate(Entry entry)
        {
            yield return (object)new WaitForSeconds(entry.delay);
            entry.target.SetActive(false);
        }

        private IEnumerator ReloadLevel(Entry entry)
        {
            yield return (object)new WaitForSeconds(entry.delay);
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}


