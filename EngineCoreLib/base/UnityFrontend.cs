using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace hundun.unitygame.enginecorelib
{
    public class UnityFrontend : IFrontend
    {
        public string[] fileGetChilePathNames(string folder)
        {
            throw new NotImplementedException();
        }

        public string fileGetContent(string file)
        {
            throw new NotImplementedException();
        }

        public void log(string logTag, string format)
        {
            Debug.Log(JavaFeatureForGwt.stringFormat("[{0}] {1}", logTag, format));
        }
    }
}
