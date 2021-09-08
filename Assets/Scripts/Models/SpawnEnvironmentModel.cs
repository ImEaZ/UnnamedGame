using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public enum idlandID
    {
        First_SW,
        First_W1,
        First_W2,
        First_W3,
        First_C1,
        First_C2,
        First_C3,
        First_N1,
        First_N2,
        First_N3
    }
    class SpawnEnvironmentModel
    {
    }

    public class spawnedItem
    {
        public Vector2 position { get; set; }
        public string itemName { get; set; }
    }
    public enum evmType
    {
        tree,
        stump,
        stone1,
        stone2,
        bush,
        berryBush
    }

}
