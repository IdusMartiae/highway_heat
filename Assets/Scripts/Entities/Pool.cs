using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly List<T> _list = new List<T>();
        private readonly T entity;

        public Pool(int poolSize, T gameObject)
        {
            entity = gameObject;
            Prepopulate(poolSize);
        }

        public Pool(List<T> gameObjects)
        {
            Prepopulate(gameObjects);
        }

        public void Push(T gameObject)
        {
            gameObject.gameObject.SetActive(false);
            _list.Add(gameObject);
        }

        public T Pull()
        {
            if (_list.Count <= 0)
            {
                CreatEntity(entity);
            }

            var item = _list.Last();
            _list.Remove(_list.Last());

            item.gameObject.SetActive(true);
            return item;
        }

        public T Pull(T gameObject)
        {
            var itemIndex = _list.FindIndex(item => item.Equals(gameObject));

            if (itemIndex == -1)
            {
                CreatEntity(gameObject);
                itemIndex = _list.Count - 1;
            }
            
            var item = _list[itemIndex];
            _list.RemoveAt(itemIndex);
            
            item.gameObject.SetActive(true);
            return item;
        }

        private void Prepopulate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                CreatEntity(entity);
            }
        }

        private void Prepopulate(List<T> gameObjects)
        {
            foreach (T gameObject in gameObjects)
            {
                CreatEntity(gameObject);
            }
        }

        private void CreatEntity(T newEntity)
        {
            var spawnedEntity = GameObject.Instantiate(newEntity);
            Push(spawnedEntity);
        }
    }
}