using UnityEngine;

using System;

// Do not use namespace to ensure overriding of default unity behaviour.
// ReSharper disable once CheckNamespace
public static class Debug
{
	public static bool isDebugBuild
	{
		get { return UnityEngine.Debug.isDebugBuild; }
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Log(object message)
	{
		UnityEngine.Debug.Log(message);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Log(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.Log(message, context);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogError(object message)
	{
		UnityEngine.Debug.LogError(message);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogError(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogError(message, context);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogWarning(object message)
	{
		UnityEngine.Debug.LogWarning(message.ToString());
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogWarning(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogWarning(message.ToString(), context);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogException(Exception exception)
	{
		UnityEngine.Debug.LogException(exception);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void LogException(Exception exception, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogException(exception, context);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Assert(bool condition)
	{
		if (!condition) throw new Exception();
	}
}