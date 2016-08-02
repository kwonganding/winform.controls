using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI
{
    public static class LoadResource
    {
        private static System.Drawing.Bitmap[] loadImages;

        static LoadResource()
        {
            loadImages = new System.Drawing.Bitmap[15];
            loadImages[0] = Properties.Resources.loader;
            loadImages[1] = Properties.Resources.loader1;
            loadImages[2] = Properties.Resources.loader2;
            loadImages[3] = Properties.Resources.loader3;
            loadImages[4] = Properties.Resources.loader4;
            loadImages[5] = Properties.Resources.loader5;
            loadImages[6] = Properties.Resources.loader6;
            loadImages[7] = Properties.Resources.loader7;
            loadImages[8] = Properties.Resources.loader8;
            loadImages[9] = Properties.Resources.loader9;
            loadImages[10] = Properties.Resources.loader10;
            loadImages[11] = Properties.Resources.loader11;
            loadImages[12] = Properties.Resources.loader12;
            loadImages[13] = Properties.Resources.loader13;
        }

        public static System.Drawing.Bitmap GetRandomLoadImage()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int n = random.Next(0, loadImages.Length);
            var img = loadImages[n];
            return img == null ? Properties.Resources.loader : img;
        }
    }
}
