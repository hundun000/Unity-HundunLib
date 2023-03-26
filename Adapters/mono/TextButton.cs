using hundun.unitygame.enginecorelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace hundun.unitygame.adapters
{
    


    public class TextButton : MonoBehaviour
    {

        public Text label { get; private set; }
        public Image background { get; private set; }
        public Button button { get; private set; }

        void Awake()
        {
            this.label = this.transform.Find("label").GetComponent<Text>();
            this.background = this.GetComponent<Image>();
            this.button = this.GetComponent<Button>();

            this.background.sprite = Resources.Load<Sprite>("button2_rounded_CC.9");
        }

    }

    


}
