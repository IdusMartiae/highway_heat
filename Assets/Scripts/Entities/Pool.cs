using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly List<T> _list = new List<T>();
        private readonly List<T> _entities = new List<T>();
        private readonly T _gameEntity;
        private readonly Transform _parent;
        private int _spawnIndex;
        
        public Pool(int poolSize, T gameEntity, Transform parent)
        {
            _gameEntity = gameEntity;
            _parent = parent;
            Prepopulate(poolSize);
        }

        public Pool(List<T> gameEntity, Transform parent)
        {
            _parent = parent;
            _entities = gameEntity;
        }

        public void Push(T gameEntity)
        {
            gameEntity.gameObject.SetActive(false);
            _list.Add(gameEntity);
        }

        public T Pull()
        {
            if (_list.Count <= 0)
            {
                CreatEntity(_gameEntity);
            }

            var item = _list.Last();
            _list.Remove(_list.Last());

            item.gameObject.SetActive(true);
            return item;
        }

        public T PullRandom()
        {
            if (_spawnIndex < _entities.Count)
            {
                CreatEntity(_entities[_spawnIndex]);
                _spawnIndex = ++_spawnIndex;
            }
            
            var index = Random.Range(0, _list.Count);
            var item = _list[index];
            _list.RemoveAt(index);

            item.gameObject.SetActive(true);
            return item;
        }
        
        private void Prepopulate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                CreatEntity(_gameEntity);
            }
        }

        private void Prepopulate(List<T> gameEntities)
        {
            foreach (var gameEntity in gameEntities)
            {
                CreatEntity(gameEntity);
            }
        }

        private void CreatEntity(T newEntity)
        {
            var spawnedEntity = Object.Instantiate(newEntity, _parent, true);
            Push(spawnedEntity);
        }

    }
}
