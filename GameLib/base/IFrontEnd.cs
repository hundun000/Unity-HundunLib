using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hundun.unitygame.gamelib
{
    public interface IFrontend
    {
        String[] fileGetChilePathNames(String folder);

        String fileGetContent(String file);

        void log(String logTag, String format);
    }

}
