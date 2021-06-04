using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly List<T> _list = new List<T>();
        private readonly T _gameEntity;

        public Pool(int poolSize, T gameEntity)
        {
            _gameEntity = gameEntity;
            Prepopulate(poolSize);
        }

        public Pool(List<T> gameEntity)
        {
            Prepopulate(gameEntity);
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
            if (_list.Count <= 0)
            {
                CreatEntity(_gameEntity);
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
            var spawnedEntity = GameObject.Instantiate(newEntity);
            Push(spawnedEntity);
        }
    }
}