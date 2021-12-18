using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct  LevelScore
{
    public LevelScore(string level, float time, int moves, int pushes)
    {
        this.level = level;
        this.time = time;
        this.moves = moves;
        this.pushes = pushes;
    }
    public string level;
    public float time;
    public int moves;
    public int pushes;

    public override string ToString() => $"({level}, {time}, {moves}, {pushes})";
}
