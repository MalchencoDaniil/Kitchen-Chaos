using System;
using System.Collections.Generic;
using UnityEngine;
using KitchenChaos.Kitchen.Counters.Plates;

namespace KitchenChaos.Kitchen.Counters.Visual
{
    public class PlatesCounterVisual : CounterVisual
    {
        [SerializeField] private PlateCounter _platesCounter;
        [SerializeField] private Transform _counterTopPoint;

        [Space(10)]
        [SerializeField] private Transform _platesVisualPrefab;

        private List<GameObject> _plateVisualGameObjectList;

        private void Awake()
        {
            _plateVisualGameObjectList = new List<GameObject>();

            _platesCounter.OnPlateSpawned += VisualView;
            _platesCounter.OnPlateRemove += OnPlateRemove;
        }

        public override void VisualView(object sender, EventArgs eventArgs)
        {
            Transform _prefabToSpawn = Instantiate(_platesVisualPrefab, _counterTopPoint);

            float _plateOffsetY = 0.1f;
            _prefabToSpawn.localPosition = new Vector3(0, _plateOffsetY * _plateVisualGameObjectList.Count, 0);
            _plateVisualGameObjectList.Add(_prefabToSpawn.gameObject);
        }

        public void OnPlateRemove(object sender, EventArgs eventArgs)
        {
            GameObject _removeObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
            _plateVisualGameObjectList.Remove(_removeObject);
            Destroy(_removeObject);
        }

        private void OnDestroy()
        {
            _platesCounter.OnPlateSpawned -= VisualView;
            _platesCounter.OnPlateRemove -= OnPlateRemove;
        }
    }
}