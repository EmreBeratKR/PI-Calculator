using UnityEngine;

public static class EventSystem
{
    public delegate void DefaultEvent();
    public delegate void BoolParamsEvent(params bool[] boolParams);
    public delegate void IntParamsEvent(params int[] intParams);
    public delegate void FloatParamsEvent(params float[] floatParams);

    public static DefaultEvent OnSpawnedInside;
    public static DefaultEvent OnSpawnedOutside;
    public static DefaultEvent OnNewRecord;
    public static DefaultEvent OnExperimentStarted;

    
    public static void SpawnedInside() => OnSpawnedInside?.Invoke();

    public static void SpawnedOutside() => OnSpawnedOutside?.Invoke();

    public static void NewRecord() => OnNewRecord?.Invoke();

    public static void ExperimentStarted() => OnExperimentStarted?.Invoke();
}
