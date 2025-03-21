using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalamud.Interface.GameFonts
{
    /// <summary>
    /// Describes a font based on game resource file.
    /// </summary>
    public struct GameFontStyle
    {
        /// <summary>
        /// Font family of the font.
        /// </summary>
        public GameFontFamilyAndSize FamilyAndSize;

        /// <summary>
        /// Weight of the font.
        ///
        /// 0 is unaltered.
        /// Any value greater than 0 will make it bolder.
        /// </summary>
        public float Weight;

        /// <summary>
        /// Skewedness of the font.
        ///
        /// 0 is unaltered.
        /// Greater than 1 will make upper part go rightwards.
        /// Less than 1 will make lower part go rightwards.
        /// </summary>
        public float SkewStrength;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFontStyle"/> struct.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="size">Size in points.</param>
        public GameFontStyle(GameFontFamily family, float size)
        {
            this.FamilyAndSize = GetRecommendedFamilyAndSize(family, size);
            this.Weight = this.SkewStrength = 0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFontStyle"/> struct.
        /// </summary>
        /// <param name="familyAndSize">Font family and size.</param>
        public GameFontStyle(GameFontFamilyAndSize familyAndSize)
        {
            this.FamilyAndSize = familyAndSize;
            this.Weight = this.SkewStrength = 0f;
        }

        /// <summary>
        /// Gets the font family.
        /// </summary>
        public GameFontFamily Family => this.FamilyAndSize switch
        {
            GameFontFamilyAndSize.Undefined => GameFontFamily.Undefined,
            GameFontFamilyAndSize.Axis96 => GameFontFamily.Axis,
            GameFontFamilyAndSize.Axis12 => GameFontFamily.Axis,
            GameFontFamilyAndSize.Axis14 => GameFontFamily.Axis,
            GameFontFamilyAndSize.Axis18 => GameFontFamily.Axis,
            GameFontFamilyAndSize.Axis36 => GameFontFamily.Axis,
            GameFontFamilyAndSize.Jupiter16 => GameFontFamily.Jupiter,
            GameFontFamilyAndSize.Jupiter20 => GameFontFamily.Jupiter,
            GameFontFamilyAndSize.Jupiter23 => GameFontFamily.Jupiter,
            GameFontFamilyAndSize.Jupiter45 => GameFontFamily.JupiterNumeric,
            GameFontFamilyAndSize.Jupiter46 => GameFontFamily.Jupiter,
            GameFontFamilyAndSize.Jupiter90 => GameFontFamily.JupiterNumeric,
            GameFontFamilyAndSize.Meidinger16 => GameFontFamily.Meidinger,
            GameFontFamilyAndSize.Meidinger20 => GameFontFamily.Meidinger,
            GameFontFamilyAndSize.Meidinger40 => GameFontFamily.Meidinger,
            GameFontFamilyAndSize.MiedingerMid10 => GameFontFamily.MiedingerMid,
            GameFontFamilyAndSize.MiedingerMid12 => GameFontFamily.MiedingerMid,
            GameFontFamilyAndSize.MiedingerMid14 => GameFontFamily.MiedingerMid,
            GameFontFamilyAndSize.MiedingerMid18 => GameFontFamily.MiedingerMid,
            GameFontFamilyAndSize.MiedingerMid36 => GameFontFamily.MiedingerMid,
            GameFontFamilyAndSize.TrumpGothic184 => GameFontFamily.TrumpGothic,
            GameFontFamilyAndSize.TrumpGothic23 => GameFontFamily.TrumpGothic,
            GameFontFamilyAndSize.TrumpGothic34 => GameFontFamily.TrumpGothic,
            GameFontFamilyAndSize.TrumpGothic68 => GameFontFamily.TrumpGothic,
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Gets the font size.
        /// </summary>
        public float Size => this.FamilyAndSize switch
        {
            GameFontFamilyAndSize.Undefined => 0,
            GameFontFamilyAndSize.Axis96 => 9.6f,
            GameFontFamilyAndSize.Axis12 => 12,
            GameFontFamilyAndSize.Axis14 => 14,
            GameFontFamilyAndSize.Axis18 => 18,
            GameFontFamilyAndSize.Axis36 => 36,
            GameFontFamilyAndSize.Jupiter16 => 16,
            GameFontFamilyAndSize.Jupiter20 => 20,
            GameFontFamilyAndSize.Jupiter23 => 23,
            GameFontFamilyAndSize.Jupiter45 => 45,
            GameFontFamilyAndSize.Jupiter46 => 46,
            GameFontFamilyAndSize.Jupiter90 => 90,
            GameFontFamilyAndSize.Meidinger16 => 16,
            GameFontFamilyAndSize.Meidinger20 => 20,
            GameFontFamilyAndSize.Meidinger40 => 40,
            GameFontFamilyAndSize.MiedingerMid10 => 10,
            GameFontFamilyAndSize.MiedingerMid12 => 12,
            GameFontFamilyAndSize.MiedingerMid14 => 14,
            GameFontFamilyAndSize.MiedingerMid18 => 18,
            GameFontFamilyAndSize.MiedingerMid36 => 36,
            GameFontFamilyAndSize.TrumpGothic184 => 18.4f,
            GameFontFamilyAndSize.TrumpGothic23 => 23,
            GameFontFamilyAndSize.TrumpGothic34 => 34,
            GameFontFamilyAndSize.TrumpGothic68 => 8,
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Gets or sets a value indicating whether this font is bold.
        /// </summary>
        public bool Bold
        {
            get => this.Weight > 0f;
            set => this.Weight = value ? 1f : 0f;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this font is italic.
        /// </summary>
        public bool Italic
        {
            get => this.SkewStrength != 0;
            set => this.SkewStrength = value ? 4 : 0;
        }

        /// <summary>
        /// Gets the recommend GameFontFamilyAndSize given family and size.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="size">Font size in points.</param>
        /// <returns>Recommended GameFontFamilyAndSize.</returns>
        public static GameFontFamilyAndSize GetRecommendedFamilyAndSize(GameFontFamily family, float size)
        {
            if (size <= 0)
                return GameFontFamilyAndSize.Undefined;

            switch (family)
            {
                case GameFontFamily.Undefined:
                    return GameFontFamilyAndSize.Undefined;

                case GameFontFamily.Axis:
                    if (size <= 9.6)
                        return GameFontFamilyAndSize.Axis96;
                    else if (size <= 12)
                        return GameFontFamilyAndSize.Axis12;
                    else if (size <= 14)
                        return GameFontFamilyAndSize.Axis14;
                    else if (size <= 18)
                        return GameFontFamilyAndSize.Axis18;
                    else
                        return GameFontFamilyAndSize.Axis36;

                case GameFontFamily.Jupiter:
                    if (size <= 16)
                        return GameFontFamilyAndSize.Jupiter16;
                    else if (size <= 20)
                        return GameFontFamilyAndSize.Jupiter20;
                    else if (size <= 23)
                        return GameFontFamilyAndSize.Jupiter23;
                    else
                        return GameFontFamilyAndSize.Jupiter46;

                case GameFontFamily.JupiterNumeric:
                    if (size <= 45)
                        return GameFontFamilyAndSize.Jupiter45;
                    else
                        return GameFontFamilyAndSize.Jupiter90;

                case GameFontFamily.Meidinger:
                    if (size <= 16)
                        return GameFontFamilyAndSize.Meidinger16;
                    else if (size <= 20)
                        return GameFontFamilyAndSize.Meidinger20;
                    else
                        return GameFontFamilyAndSize.Meidinger40;

                case GameFontFamily.MiedingerMid:
                    if (size <= 10)
                        return GameFontFamilyAndSize.MiedingerMid10;
                    else if (size <= 12)
                        return GameFontFamilyAndSize.MiedingerMid12;
                    else if (size <= 14)
                        return GameFontFamilyAndSize.MiedingerMid14;
                    else if (size <= 18)
                        return GameFontFamilyAndSize.MiedingerMid18;
                    else
                        return GameFontFamilyAndSize.MiedingerMid36;

                case GameFontFamily.TrumpGothic:
                    if (size <= 18.4)
                        return GameFontFamilyAndSize.TrumpGothic184;
                    else if (size <= 23)
                        return GameFontFamilyAndSize.TrumpGothic23;
                    else if (size <= 34)
                        return GameFontFamilyAndSize.TrumpGothic34;
                    else
                        return GameFontFamilyAndSize.TrumpGothic68;

                default:
                    return GameFontFamilyAndSize.Undefined;
            }
        }

        /// <summary>
        /// Calculates the adjustment to width resulting fron Weight and SkewStrength.
        /// </summary>
        /// <param name="reader">Font information.</param>
        /// <param name="glyph">Glyph.</param>
        /// <returns>Width adjustment in pixel unit.</returns>
        public int CalculateWidthAdjustment(FdtReader reader, FdtReader.FontTableEntry glyph)
        {
            var widthDelta = this.Weight;
            if (this.SkewStrength > 0)
                widthDelta += 1f * this.SkewStrength * (reader.FontHeader.LineHeight - glyph.CurrentOffsetY) / reader.FontHeader.LineHeight;
            else if (this.SkewStrength < 0)
                widthDelta -= 1f * this.SkewStrength * (glyph.CurrentOffsetY + glyph.BoundingHeight) / reader.FontHeader.LineHeight;

            return (int)Math.Ceiling(widthDelta);
        }
    }
}
