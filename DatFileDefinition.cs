using BrawlLib.IO;
using BrawlLib.SSBB.ResourceNodes;
using BrawlLib.SSBBTypes;
using BrawlLib.Wii.Textures;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RawTextureManager {
	public class DatFileDefinition {
		public string Name { get; set; }
		public DatTextureDefinition[] Textures { get; set; }

		public override string ToString() {
			return string.Format("{0} ({1} textures)", Name, Textures.Length);
		}
	}

	public class DatTextureDefinition {
		public string Name { get; set; }
		public WiiPixelFormat Type { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		[JsonConverter(typeof(HexIntConverter))]
		public int Location { get; set; }
		public int MipLevels { get; set; }
		public DatPaletteDefinition Palette { get; set; }

		public DatTextureDefinition() {
			this.MipLevels = 1;
		}

		public int GetTextureSize() {
			return TextureConverter.Get(Type).GetMipOffset(Width, Height, MipLevels + 1);
		}
	}

	public class DatPaletteDefinition {
		public WiiPaletteFormat Type { get; set; }
		public int Colors { get; set; }
		[JsonConverter(typeof(HexIntConverter))]
		public int Location { get; set; }

		public int GetPaletteSize() {
			return Colors * 2;
		}
	}
}
