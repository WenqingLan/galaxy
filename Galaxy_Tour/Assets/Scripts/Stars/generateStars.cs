using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace chardata{
    [System.Serializable]
    public struct StarData
    {
        public Vector3 position;
        public float mag;
        public Color color;

    }

    //[CreateAssetMenu(fileName = "StarData", menuName = "Star/StarSet", order = 1)]
    public class GenerateStars : ScriptableObject
    {
        [SerializeField]
        public StarData[] stars;
    }
}
