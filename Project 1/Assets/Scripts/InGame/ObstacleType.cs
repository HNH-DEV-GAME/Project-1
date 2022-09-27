using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacle;

namespace Obstacle
{
    public enum ObstacleTypes
    {
        Human,
        Wall,
        Wood,
        Ground,
        Metal
    }
}

public class ObstacleType : MonoBehaviour
{
    [SerializeField] private Obstacle.ObstacleTypes type;
    [SerializeField] private AudioClip audio;
   
    public Obstacle.ObstacleTypes GetObstacleType()
    {
        return type;
    }
    public AudioClip GetAudio()
    {
        return audio;
    }
}
