/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using Steamworks;
using UnityEngine;

namespace HomewreckersStudio
{
    public static class SteamUtilsTest
    {
        /**
         * Creates a texture from a Steam image and returns the size.
         */
        public static Texture2D GetSteamImageAsTexture2D(int iImage, out Vector2 size)
        {
            Texture2D ret = null;
            uint ImageWidth;
            uint ImageHeight;
            bool bIsValid = SteamUtils.GetImageSize(iImage, out ImageWidth, out ImageHeight);

            if (bIsValid)
            {
                byte[] Image = new byte[ImageWidth * ImageHeight * 4];

                bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
                if (bIsValid)
                {
                    Image = FlipVertical(Image, (int)ImageWidth, (int)ImageHeight);

                    ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
                    ret.LoadRawTextureData(Image);
                    ret.Apply();
                }
            }

            size.x = (int)ImageWidth;
            size.y = (int)ImageHeight;

            return ret;
        }

        /**
         * Flips an image along the Y axis.
         */
        private static byte[] FlipVertical(byte[] image, int width, int height)
        {
            width *= 4;

            var copy = new byte[image.Length];

            for (var y0 = 0; y0 < height; y0++)
            {
                var y1 = (height - 1) - y0;

                for (var x = 0; x < width; x++)
                {
                    var lines0 = width * y0;
                    var lines1 = width * y1;

                    copy[lines1 + x] = image[lines0 + x];
                }
            }

            return copy;
        }
    }
}

#endif // UNITY_STANDALONE_WIN
