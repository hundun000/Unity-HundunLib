using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace hundun.unitygame.adapters
{
    public abstract class ContainerMonoBehaviour<T_CONTENT> : MonoBehaviour
    {
        private T_CONTENT _content;
        
        public T_CONTENT Content
        {
            get {
                return _content;
            }
            set { _content = value; }

        }
    }



}

