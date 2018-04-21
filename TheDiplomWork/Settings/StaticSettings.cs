using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class StaticSettings
    {
        public class Settings
        {
            //You can change here.
            public bool ConsoleIsEnabled = false;

            public bool ReloaderCauseOfChunkRare = false;

            public bool ReloaderCauseOfChangingChunk = true;
            public int RangeOfView = 3; //Range of chunks in every direction from you.

            public bool RealoderCauseOfPointOfView = true;
            public float PointOfViewCoefOfDifference = 0.85f; // How Much Unchanged Point Of View.

            public bool RealoderCauseOfSunSided = true;
            public float SunSidedCoef = 0.25f; //It's a light side to you.

            public bool FlyMod = true;

            public bool PhantomMod = false;

            public bool GradientLightEffect = true;

            //public bool SystemInfo = false;

            public bool HelpInfoForPlayer = true;

            public Settings()
            {
                if (ReloaderCauseOfChangingChunk || RealoderCauseOfPointOfView || RealoderCauseOfSunSided || ReloaderCauseOfChunkRare)
                    RequiredReloader = true;
                if (!ReloaderCauseOfChangingChunk && !ReloaderCauseOfChunkRare) RangeOfView = 9999;
            }

            //Changed on its own way
            public bool RequiredReloader = false;

            public bool RealoderCauseOfBuildingBlocks = false;

            //Enabling Vertex Array
            public bool Secondary_SceneInfo_is_Activated = true;
            public bool GhostCube_Add_in_Data_For_Draw = false;
        }
        public static Settings S = new Settings();
    }
}
