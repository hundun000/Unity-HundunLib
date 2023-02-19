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

    public abstract class ScreenContainerMonoBehaviour<T_CONTENT> : ContainerMonoBehaviour<T_CONTENT>
    {
        protected GameObject PopoupRoot { get; private set; }
        protected GameObject UiRoot { get; private set; }
        protected GameObject Templates { get; private set; }

        virtual protected void Start()
        {
            PopoupRoot = this.transform.Find("_popupRoot").gameObject;
            UiRoot = this.transform.Find("_uiRoot").gameObject;
            Templates = this.transform.Find("_templates").gameObject;
        }
    }


}

