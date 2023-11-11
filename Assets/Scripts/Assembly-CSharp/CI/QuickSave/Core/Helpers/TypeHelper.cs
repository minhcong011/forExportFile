// ILSpyBased#2
using CI.QuickSave.Core.Models;
using CI.QuickSave.Core.Serialisers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CI.QuickSave.Core.Helpers
{
    public static class TypeHelper
    {
        private static readonly IDictionary<string, Func<object, object>> _unityTypeToQuickSaveType = new Dictionary<string, Func<object, object>> {
            {
                "UnityEngine.Vector2",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Vector2.FromUnityType((UnityEngine.Vector2)value))
            },
            {
                "UnityEngine.Vector3",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Vector3.FromUnityType((UnityEngine.Vector3)value))
            },
            {
                "UnityEngine.Vector4",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Vector4.FromUnityType((UnityEngine.Vector4)value))
            },
            {
                "UnityEngine.Quaternion",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Quaternion.FromUnityType((UnityEngine.Quaternion)value))
            },
            {
                "UnityEngine.Color",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Color.FromUnityType((UnityEngine.Color)value))
            },
            {
                "UnityEngine.Color32",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Color32.FromUnityType((UnityEngine.Color32)value))
            },
            {
                "UnityEngine.Rect",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Rect.FromUnityType((UnityEngine.Rect)value))
            },
            {
                "UnityEngine.Bounds",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Bounds.FromUnityType((UnityEngine.Bounds)value))
            },
            {
                "UnityEngine.Matrix4x4",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Matrix4x4.FromUnityType((UnityEngine.Matrix4x4)value))
            },
            {
                "UnityEngine.Texture2D",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Texture2D.FromUnityType((UnityEngine.Texture2D)value))
            },
            {
                "UnityEngine.Sprite",
                (Func<object, object>)((object value) => CI.QuickSave.Core.Models.Sprite.FromUnityType((UnityEngine.Sprite)value))
            }
        };

        private static readonly IDictionary<string, Func<string, IJsonSerialiser, object>> _quickSaveTypeToUnityType = new Dictionary<string, Func<string, IJsonSerialiser, object>> {
            {
                "UnityEngine.Vector2",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Vector2>(value).ToUnityType())
            },
            {
                "UnityEngine.Vector3",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Vector3>(value).ToUnityType())
            },
            {
                "UnityEngine.Vector4",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Vector4>(value).ToUnityType())
            },
            {
                "UnityEngine.Quaternion",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Quaternion>(value).ToUnityType())
            },
            {
                "UnityEngine.Color",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Color>(value).ToUnityType())
            },
            {
                "UnityEngine.Color32",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Color32>(value).ToUnityType())
            },
            {
                "UnityEngine.Rect",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Rect>(value).ToUnityType())
            },
            {
                "UnityEngine.Bounds",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Bounds>(value).ToUnityType())
            },
            {
                "UnityEngine.Matrix4x4",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Matrix4x4>(value).ToUnityType())
            },
            {
                "UnityEngine.Texture2D",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Texture2D>(value).ToUnityType())
            },
            {
                "UnityEngine.Sprite",
                (Func<string, IJsonSerialiser, object>)((string value, IJsonSerialiser serialiser) => serialiser.Deserialise<CI.QuickSave.Core.Models.Sprite>(value).ToUnityType())
            }
        };

        public static object ReplaceIfUnityType<T>(T value)
        {
            string fullName = typeof(T).FullName;
            if (TypeHelper._unityTypeToQuickSaveType.ContainsKey(fullName))
            {
                return TypeHelper._unityTypeToQuickSaveType[fullName](value);
            }
            return value;
        }

        public static bool IsUnityType<T>()
        {
            string fullName = typeof(T).FullName;
            return TypeHelper._unityTypeToQuickSaveType.ContainsKey(fullName);
        }

        public static T DeserialiseUnityType<T>(string value, IJsonSerialiser jsonSerialiser)
        {
            string fullName = typeof(T).FullName;
            return (T)TypeHelper._quickSaveTypeToUnityType[fullName](value, jsonSerialiser);
        }
    }
}


