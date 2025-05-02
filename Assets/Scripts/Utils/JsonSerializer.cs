// dnSpy decompiler from Assembly-CSharp.dll class: Utils.JsonSerializer
using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Utils
{
	public class JsonSerializer
	{
		public static object Decode(byte[] json)
		{
			return JsonSerializer.Decode(Encoding.ASCII.GetString(json));
		}

		public static object Decode(string json)
		{
			bool flag = true;
			return JsonSerializer.Decode(json, ref flag);
		}

		public static void Decode(object instance, string json)
		{
			object obj = JsonSerializer.Decode(json);
			JsonSerializer.PopulateObject(instance.GetType(), obj, instance);
		}

		public static object Decode(string json, ref bool success)
		{
			success = true;
			if (json != null)
			{
				char[] json2 = json.ToCharArray();
				int num = 0;
				return JsonSerializer.ParseValue(json2, ref num, ref success);
			}
			return null;
		}

		public static string Encode(object json)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = JsonSerializer.SerializeValue(json, stringBuilder);
			return (!flag) ? null : stringBuilder.ToString();
		}

		public static T Decode<T>(byte[] json) where T : class, new()
		{
			return JsonSerializer.Decode<T>(Encoding.ASCII.GetString(json));
		}

		public static T Decode<T>(string json) where T : class, new()
		{
			bool flag = true;
			object obj = JsonSerializer.Decode(json, ref flag);
			return JsonSerializer.PopulateObject(typeof(T), obj) as T;
		}

		private static object PopulateObject(Type T, object obj)
		{
			return JsonSerializer.PopulateObject(T, obj, null);
		}

		private static object PopulateObject(Type T, object obj, object instance)
		{
			if (obj == null)
			{
				return null;
			}
			if (T.IsAssignableFrom(obj.GetType()))
			{
				instance = obj;
			}
			else if (obj is Hashtable)
			{
				Hashtable hashtable = (Hashtable)obj;
				if (instance == null)
				{
					instance = Activator.CreateInstance(T);
				}
				foreach (FieldInfo fieldInfo in T.GetFields())
				{
					if (hashtable.ContainsKey(fieldInfo.Name))
					{
						fieldInfo.SetValue(instance, JsonSerializer.PopulateObject(fieldInfo.FieldType, hashtable[fieldInfo.Name]));
					}
				}
			}
			else if (obj is IEnumerable)
			{
				if (instance == null)
				{
					instance = Activator.CreateInstance(T);
				}
				IList list = instance as IList;
				if (list != null)
				{
					Type t = typeof(object);
					Type type = instance.GetType();
					if (type.IsGenericType)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments.Length != 1)
						{
							return null;
						}
						t = genericArguments[0];
					}
					IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							list.Add(JsonSerializer.PopulateObject(t, obj2));
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
			return instance;
		}

		protected static Hashtable ParseObject(char[] json, ref int index, ref bool success)
		{
			Hashtable hashtable = new Hashtable();
			JsonSerializer.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				int num = JsonSerializer.LookAhead(json, index);
				if (num == 0)
				{
					success = false;
					return null;
				}
				if (num == 6)
				{
					JsonSerializer.NextToken(json, ref index);
				}
				else
				{
					if (num == 2)
					{
						JsonSerializer.NextToken(json, ref index);
						return hashtable;
					}
					string key = JsonSerializer.ParseString(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}
					num = JsonSerializer.NextToken(json, ref index);
					if (num != 5)
					{
						success = false;
						return null;
					}
					object value = JsonSerializer.ParseValue(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}
					hashtable[key] = value;
				}
			}
			return hashtable;
		}

		protected static ArrayList ParseArray(char[] json, ref int index, ref bool success)
		{
			ArrayList arrayList = new ArrayList();
			JsonSerializer.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				int num = JsonSerializer.LookAhead(json, index);
				if (num == 0)
				{
					success = false;
					return null;
				}
				if (num == 6)
				{
					JsonSerializer.NextToken(json, ref index);
				}
				else
				{
					if (num == 4)
					{
						JsonSerializer.NextToken(json, ref index);
						break;
					}
					object value = JsonSerializer.ParseValue(json, ref index, ref success);
					if (!success)
					{
						return null;
					}
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		protected static object ParseValue(char[] json, ref int index, ref bool success)
		{
			switch (JsonSerializer.LookAhead(json, index))
			{
			case 1:
				return JsonSerializer.ParseObject(json, ref index, ref success);
			case 3:
				return JsonSerializer.ParseArray(json, ref index, ref success);
			case 7:
				return JsonSerializer.ParseString(json, ref index, ref success);
			case 8:
				return JsonSerializer.ParseNumber(json, ref index, ref success);
			case 9:
				JsonSerializer.NextToken(json, ref index);
				return true;
			case 10:
				JsonSerializer.NextToken(json, ref index);
				return false;
			case 11:
				JsonSerializer.NextToken(json, ref index);
				return null;
			}
			success = false;
			return null;
		}

		protected static string ParseString(char[] json, ref int index, ref bool success)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JsonSerializer.EatWhitespace(json, ref index);
			char c = json[index++];
			bool flag = false;
			while (!flag)
			{
				if (index == json.Length)
				{
					break;
				}
				c = json[index++];
				if (c == '"')
				{
					flag = true;
					break;
				}
				if (c == '\\')
				{
					if (index == json.Length)
					{
						break;
					}
					c = json[index++];
					if (c == '"')
					{
						stringBuilder.Append('"');
					}
					else if (c == '\\')
					{
						stringBuilder.Append('\\');
					}
					else if (c == '/')
					{
						stringBuilder.Append('/');
					}
					else if (c == 'b')
					{
						stringBuilder.Append('\b');
					}
					else if (c == 'f')
					{
						stringBuilder.Append('\f');
					}
					else if (c == 'n')
					{
						stringBuilder.Append('\n');
					}
					else if (c == 'r')
					{
						stringBuilder.Append('\r');
					}
					else if (c == 't')
					{
						stringBuilder.Append('\t');
					}
					else if (c == 'u')
					{
						int num = json.Length - index;
						if (num < 4)
						{
							break;
						}
						uint utf;
						if (!(success = uint.TryParse(new string(json, index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out utf)))
						{
							return string.Empty;
						}
						stringBuilder.Append(char.ConvertFromUtf32((int)utf));
						index += 4;
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (!flag)
			{
				success = false;
				return null;
			}
			return stringBuilder.ToString();
		}

		protected static object ParseNumber(char[] json, ref int index, ref bool success)
		{
			JsonSerializer.EatWhitespace(json, ref index);
			int lastIndexOfNumber = JsonSerializer.GetLastIndexOfNumber(json, index);
			int length = lastIndexOfNumber - index + 1;
			string text = new string(json, index, length);
			float num;
			success = float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
			index = lastIndexOfNumber + 1;
			if (text.Contains("."))
			{
				return num;
			}
			return (int)num;
		}

		protected static int GetLastIndexOfNumber(char[] json, int index)
		{
			int i;
			for (i = index; i < json.Length; i++)
			{
				if ("0123456789+-.eE".IndexOf(json[i]) == -1)
				{
					break;
				}
			}
			return i - 1;
		}

		protected static void EatWhitespace(char[] json, ref int index)
		{
			while (index < json.Length)
			{
				if (" \t\n\r".IndexOf(json[index]) == -1)
				{
					break;
				}
				index++;
			}
		}

		protected static int LookAhead(char[] json, int index)
		{
			int num = index;
			return JsonSerializer.NextToken(json, ref num);
		}

		protected static int NextToken(char[] json, ref int index)
		{
			JsonSerializer.EatWhitespace(json, ref index);
			if (index == json.Length)
			{
				return 0;
			}
			char c = json[index];
			index++;
			switch (c)
			{
			case ',':
				return 6;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return 8;
			default:
				switch (c)
				{
				case '[':
					return 3;
				default:
					switch (c)
					{
					case '{':
						return 1;
					default:
					{
						if (c == '"')
						{
							return 7;
						}
						index--;
						int num = json.Length - index;
						if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
						{
							index += 5;
							return 10;
						}
						if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
						{
							index += 4;
							return 9;
						}
						if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
						{
							index += 4;
							return 11;
						}
						return 0;
					}
					case '}':
						return 2;
					}
					break;
				case ']':
					return 4;
				}
				break;
			case ':':
				return 5;
			}
		}

		protected static bool SerializeValue(object value, StringBuilder builder)
		{
			bool result = true;
			if (value is string)
			{
				result = JsonSerializer.SerializeString((string)value, builder);
			}
			else if (value is Hashtable)
			{
				result = JsonSerializer.SerializeObject((Hashtable)value, builder);
			}
			else if (value is IEnumerable)
			{
				result = JsonSerializer.SerializeArray((IEnumerable)value, builder);
			}
			else if (value is float)
			{
				result = JsonSerializer.SerializeNumber(Convert.ToSingle(value), builder);
			}
			else if (value is int)
			{
				result = JsonSerializer.SerializeNumber(Convert.ToInt32(value), builder);
			}
			else if (value is bool && (bool)value)
			{
				builder.Append("true");
			}
			else if (value is bool && !(bool)value)
			{
				builder.Append("false");
			}
			else if (value == null)
			{
				builder.Append("null");
			}
			else if (value is DateTime)
			{
				builder.Append(((DateTime)value).ToString("o"));
			}
			else
			{
				Hashtable hashtable = new Hashtable();
				foreach (FieldInfo fieldInfo in value.GetType().GetFields())
				{
					if (!fieldInfo.IsNotSerialized)
					{
						hashtable[fieldInfo.Name] = fieldInfo.GetValue(value);
					}
				}
				foreach (PropertyInfo propertyInfo in value.GetType().GetProperties())
				{
					hashtable[propertyInfo.Name] = propertyInfo.GetValue(value, null);
				}
				JsonSerializer.SerializeObject(hashtable, builder);
			}
			return result;
		}

		protected static bool SerializeObject(Hashtable anObject, StringBuilder builder)
		{
			builder.Append("{");
			IDictionaryEnumerator enumerator = anObject.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string aString = enumerator.Key.ToString();
				object value = enumerator.Value;
				if (!flag)
				{
					builder.Append(", ");
				}
				JsonSerializer.SerializeString(aString, builder);
				builder.Append(":");
				if (!JsonSerializer.SerializeValue(value, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append("}");
			return true;
		}

		protected static bool SerializeArray(IEnumerable anArray, StringBuilder builder)
		{
			builder.Append("[");
			bool flag = true;
			IEnumerator enumerator = anArray.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object value = enumerator.Current;
					if (!flag)
					{
						builder.Append(", ");
					}
					if (!JsonSerializer.SerializeValue(value, builder))
					{
						return false;
					}
					flag = false;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			builder.Append("]");
			return true;
		}

		protected static bool SerializeString(string aString, StringBuilder builder)
		{
			builder.Append("\"");
			foreach (char c in aString.ToCharArray())
			{
				if (c == '"')
				{
					builder.Append("\\\"");
				}
				else if (c == '\\')
				{
					builder.Append("\\\\");
				}
				else if (c == '\b')
				{
					builder.Append("\\b");
				}
				else if (c == '\f')
				{
					builder.Append("\\f");
				}
				else if (c == '\n')
				{
					builder.Append("\\n");
				}
				else if (c == '\r')
				{
					builder.Append("\\r");
				}
				else if (c == '\t')
				{
					builder.Append("\\t");
				}
				else
				{
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						builder.Append(c);
					}
					else
					{
						builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
					}
				}
			}
			builder.Append("\"");
			return true;
		}

		protected static bool SerializeNumber(int number, StringBuilder builder)
		{
			builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
			return true;
		}

		protected static bool SerializeNumber(float number, StringBuilder builder)
		{
			builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
			return true;
		}

		protected static bool IsNumeric(object o)
		{
			float num;
			return o != null && float.TryParse(o.ToString(), out num);
		}

		public const int TOKEN_NONE = 0;

		public const int TOKEN_CURLY_OPEN = 1;

		public const int TOKEN_CURLY_CLOSE = 2;

		public const int TOKEN_SQUARED_OPEN = 3;

		public const int TOKEN_SQUARED_CLOSE = 4;

		public const int TOKEN_COLON = 5;

		public const int TOKEN_COMMA = 6;

		public const int TOKEN_STRING = 7;

		public const int TOKEN_NUMBER = 8;

		public const int TOKEN_TRUE = 9;

		public const int TOKEN_FALSE = 10;

		public const int TOKEN_NULL = 11;
	}
}
