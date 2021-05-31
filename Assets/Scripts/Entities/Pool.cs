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

        public void Push(T gameObject)
        {
            gameObject.gameObject.SetActive(false);
            _list.Add(gameObject);
        }

        public T Pull()
        {
            if (_list.Count <= 0)
            {
                CreatEntity();
            }

            var item = _list.Last();
            _list.Remove(_list.Last());

            item.gameObject.SetActive(true);
            return item;
        }

        private void Prepopulate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                CreatEntity();
            }
        }

        private void CreatEntity()
        {
            var spawnedEntity = GameObject.Instantiate(entity);
            Push(spawnedEntity);
        }
    }
}