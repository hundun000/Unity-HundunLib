using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace hundun.unitygame.enginecorelib
{
    public class BackgroundVM : MonoBehaviour
    {
        Image selfImage;
        //AspectRatioFitter aspectRatioFitter;

        void Awake()
        {
            this.selfImage = this.GetComponent<Image>();
            //this.aspectRatioFitter = this.GetComponent<AspectRatioFitter>();
        }

        public void update(Sprite sprite)
        {
            this.selfImage.sprite = sprite;
            //Vector2 parentSize = GetParentSize();
            //this.aspectRatioFitter.aspectRatio = parentSize.x / parentSize.y;
        }

        private Vector2 GetParentSize()
        {
            var parent = transform.parent as RectTransform;
            return parent == null ? Vector2.zero : parent.rect.size;
        }

    }
}

