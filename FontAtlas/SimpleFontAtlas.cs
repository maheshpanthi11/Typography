﻿//MIT, 2016-present, WinterDev
//----------------------------------- 

using System;
using System.Collections.Generic;

using Typography.Rendering;

namespace PixelFarm.Drawing.Fonts
{
    public enum TextureKind : byte
    {
        StencilLcdEffect, //default
        StencilGreyScale,
        Msdf,
        Bitmap
    }
    public class SimpleFontAtlas
    {
        GlyphImage _totalGlyphImage;
        Dictionary<ushort, TextureGlyphMapData> _glyphLocations = new Dictionary<ushort, TextureGlyphMapData>();

        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// original font size in point unit
        /// </summary>
        public float OriginalFontSizePts { get; set; }
        public TextureKind TextureKind { get; set; }
        public string FontFilename { get; set; }
        public int FontKey { get; set; }

        public void AddGlyph(ushort glyphIndex, TextureGlyphMapData glyphData)
        {
            _glyphLocations.Add(glyphIndex, glyphData);
        }
        public bool UseSharedGlyphImage { get; set; }
        public GlyphImage TotalGlyph
        {
            get => _totalGlyphImage;
            set => _totalGlyphImage = value;
        }
        public bool TryGetGlyphMapData(ushort glyphIndex, out TextureGlyphMapData glyphdata)
        {
            if (!_glyphLocations.TryGetValue(glyphIndex, out glyphdata))
            {
                glyphdata = null;
                return false;
            }
            return true;
        }


        public static Dictionary<ushort, TextureGlyphMapData> CloneLocationWithOffset(SimpleFontAtlas org, int dx, int dy)
        {
            Dictionary<ushort, TextureGlyphMapData> cloneDic = new Dictionary<ushort, TextureGlyphMapData>();
            foreach (var kp in org._glyphLocations)
            {
                TextureGlyphMapData orgMapData = kp.Value;
                cloneDic.Add(kp.Key,
                    new TextureGlyphMapData()
                    {
                        Left = orgMapData.Left + dx,
                        Top = orgMapData.Top + dy,
                        //
                        Width = orgMapData.Width,
                        Height = orgMapData.Height,
                        TextureXOffset = orgMapData.TextureXOffset,
                        TextureYOffset = orgMapData.TextureYOffset

                    });
            }
            return cloneDic;
        }
    }

}