﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame.Objects
{
    static class GamePreferences
    {
        static private int DisplaySettingsIndex = 0;

        static private Size[] DisplaySizes = new Size[3] { new Size(800, 600), new Size(1024, 768), new Size(1920, 1080) };

        /// <summary>
        /// Устанавливает настройки окна
        /// </summary>
        /// <param name="DisplaySettings"> 
        /// {0} 800 - 600 
        /// {1} 1024 - 768
        /// {2} 1920 - 1080
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">При установлении параметра выше допустимого</exception>
        static public void SetDisplaySettings(int DisplaySettings)
        {
            if(DisplaySettings < 0 || DisplaySettings > DisplaySizes.Count() - 1)
            {
                throw new ArgumentOutOfRangeException("DisplaySettings", "Индекс текущей настройки (" + DisplaySettings + ") превышсил допустимое значение (" + (DisplaySizes.Count() - 1) + ")");
            }
            DisplaySettingsIndex = DisplaySettings;
        }

        static public Size DisplaySettings { get { return DisplaySizes[DisplaySettingsIndex]; } }

    }
}