using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
     public class ColliderPool
     {
          private const string PULL_IS_EMPTY = "PULL_IS_EMPTY";

          private readonly List<GameObject> _list = new List<GameObject>();

          public ColliderPool(int poolSize, GameObject gameObject)
          {
               for (int i = 0; i < poolSize; i++)
               {
                    _list.Add(gameObject);
               }
          }    
          
          public void Push(GameObject gameObject)
          {
               gameObject.SetActive(false);
               _list.Add(gameObject);
          }

          public GameObject Pull()
          {
               GameObject collider;

               if (_list.Count > 0)
               {
                    collider = _list.Last();
                    _list.Remove(_list.Last());
               } 
               else 
               {
                    throw new Exception(PULL_IS_EMPTY);
               }
               
               collider.SetActive(true);
               return collider;
          }
     }
}
