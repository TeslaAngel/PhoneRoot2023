using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManagerMono<T> : MonoBehaviour where T : MonoBehaviour, new()
{
	private static T instance;

	private static T CreateInstance()
	{
		GameObject obj = new GameObject();
		obj.name = typeof(T).ToString();
		DontDestroyOnLoad(obj);
		return obj.AddComponent<T>();
	}

	private static T GetInstance()
	{
		if (instance == null)
			instance = CreateInstance();
		return instance;
	}

	public static T Inst => GetInstance();


	private static bool HasInst()
	{
		return instance != null;
	}

	private static void RegisterInst(T obj)
	{
		instance = obj;
		DontDestroyOnLoad(obj);
	}

	private static T RegisterInst(string name)
	{
		var obj = GameObject.Find(name)?.GetComponent<T>();
		if (obj)
			RegisterInst(obj);
		return obj;
	}

	protected bool TryRegisterThis()
	{
		if (HasInst())
		{
			return false;
		}
		else if (this is T)
		{
			RegisterInst(this as T);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static void InitInstManager(string name)
	{
		if (!HasInst())
		{
			var obj = (name?.Length > 0)
				? (RegisterInst(name) ?? CreateInstance())
				: CreateInstance();
			(obj as BaseManagerMono<T>).InitManagerThis();
			Debug.Log("Init BaseManagerMono : " + typeof(T).ToString());
		}
	}
	public static void InitInstManager(T obj)
	{
		if (!HasInst())
		{
			(obj as BaseManagerMono<T>).InitManagerThis();
			Debug.Log("Init BaseManagerMono : " + typeof(T).ToString());
		}
	}

	private void InitManagerThis()
	{
		if (TryRegisterThis())
			InitMgrSuccess();
		else
			InitMgrFailed();
	}


	protected abstract void InitMgrSuccess();

	protected virtual void InitMgrFailed()
	{
		gameObject.SetActive(false);
	}
}
