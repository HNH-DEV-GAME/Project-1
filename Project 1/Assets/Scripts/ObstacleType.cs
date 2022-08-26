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
        Ground
    }
}

public class ObstacleType : MonoBehaviour
{
    [SerializeField] private Obstacle.ObstacleTypes type;
   
    public Obstacle.ObstacleTypes GetObstacleType()
    {
        return type;
    }
}
