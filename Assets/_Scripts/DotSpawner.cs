using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawner : Scenegleton<DotSpawner>
{
    public const int DOT_POOL_LIMIT = 50000;
    public const int DOT_POOLING_PER_FRAME = 100000;
    public const int SPAWN_PER_FRAME = 200;


    [SerializeField] private Dot dotPrefab;
    [SerializeField] private Transform dotParent;
    [field: SerializeField] public int dotCount { get; set; }
    public bool isSpawning { get; private set; }

    public static float radius => DotSpawner.Instance.transform.localScale.x * 0.5f;


    public void SpawnAll()
    {
        EventSystem.ExperimentStarted();
        isSpawning = true;
        StartCoroutine(SpawnCoroutine());
    }

    public int DPF => poolExceeded ? DOT_POOLING_PER_FRAME : SPAWN_PER_FRAME;

    private IEnumerator SpawnCoroutine()
    {
        int counter = 0;
        while (counter < dotCount)
        {
            for (int i = 0; i < DPF; i++)
            {
                SpawnDot();
                counter++;
                if (counter >= dotCount) break;
            }
            PiCalculator.CalculatePI();
            yield return 0;
        }
        isSpawning = false;
    }

    private bool poolExceeded => dotParent.childCount > DOT_POOL_LIMIT;

    private void SpawnDot()
    {
        if (poolExceeded)
        {
            Dot firstDot = dotParent.GetChild(0).GetComponent<Dot>();
            firstDot.transform.localPosition = DotSpawner.randomPosition;
            firstDot.CheckPosition();
        }
        else
        {
            Dot dot = Instantiate(dotPrefab, dotParent);
            dot.transform.localPosition = DotSpawner.randomPosition;
            dot.CheckPosition();
        }
    }

    private static Vector3 randomPosition
    {
        get
        {
            var radius = DotSpawner.radius;
            return new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        }
    }
}
