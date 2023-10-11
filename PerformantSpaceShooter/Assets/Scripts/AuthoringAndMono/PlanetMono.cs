﻿using ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace AuthoringAndMono {
    public class PlanetMono : MonoBehaviour {
        public float2 FieldDimensions;
        public int NumberSpawnersToSpawn;
        public GameObject SpawnerPrefab;
        public GameObject AlienPrefab;
        public float AlienSpawnRate;
        public uint RandomSeed;
    }

    public class PlanetBaker : Baker<PlanetMono> {
        public override void Bake(PlanetMono authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlanetProperties {
                FieldDimensions =  authoring.FieldDimensions,
                NumberOfSpawnersToSpawn = authoring.NumberSpawnersToSpawn,
                SpawnerPrefab = GetEntity(authoring.SpawnerPrefab, TransformUsageFlags.Dynamic),
                AlienPrefab = GetEntity(authoring.AlienPrefab, TransformUsageFlags.Dynamic),
                AlienSpawnRate = authoring.AlienSpawnRate
            });
            
            AddComponent(entity, new PlanetRandom {
                Value =  Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<AlienSpawnPoints>();
            AddComponent<AlienSpawnTimer>();
        }
    }
}