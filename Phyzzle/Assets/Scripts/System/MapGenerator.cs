using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMap {
    private class Path {
        public int from;
        public int to;
        public int difficulty;

        public Path(int from, int to, int difficulty) {
            this.from = from;
            this.to = to;
            this.difficulty = difficulty;
        }
    }

    private class Zone {
        public int id;

        public Zone(int id) {
            this.id = id;
        }
    }

    private Zone[] zones;
    private Path[] playerPaths;

    public DynamicMap(int level) {
        GenerateMainPaths(level);
        GenerateZones();
    }

    private void GenerateMainPaths(int level) {
        if (level == 0) {
            GenerateMainPaths(2, 3, 4, 1, 2);
        } else if (level == 1) {
            GenerateMainPaths(3, 7, 2, 3);
        } else if (level == 2) {
            GenerateMainPaths(3, 4, 10, 2, 4);
        } else {
            GenerateMainPaths(3, 4, 15, 4, 6);
        }
    }

    private void GenerateZones() {
        int n = playerPaths.Length + 1;
        zones = new Zone[n];
        for (int i = 0; i < n; i++) {
            zones[i] = new Zone(i);
        }
    }

    private void GenerateMainPaths(int n, int sum, int dMin, int dMax) {
        System.Random rnd = new System.Random();

        playerPaths = new Path[n];
        for (int i = 0; i < n; i++) {
            playerPaths[i] = new Path(i, i + 1, dMin);
        }

        int remain = sum - (dMin * n);
        while (remain > 0) {
            for (int i = 0; i < n && remain > 0; i++) {
                int add = rnd.Next(0, Math.Min(dMax - playerPaths[i].difficulty, remain));
                playerPaths[i].difficulty += add;
                remain -= add;
            }
        }
    }

    private void GenerateMainPaths(int nMin, int nMax, int sum, int dMin, int dMax) {
        System.Random rnd = new System.Random();
        GenerateMainPaths(rnd.Next(nMin, nMax + 1), sum, dMin, dMax);
    }
}

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private DynamicMap Generate(int level) {
        DynamicMap map = new DynamicMap(level);
        return map;
    }
}
