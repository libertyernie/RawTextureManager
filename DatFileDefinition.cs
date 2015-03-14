using BrawlLib.IO;
using BrawlLib.SSBB.ResourceNodes;
using BrawlLib.SSBBTypes;
using BrawlLib.Wii.Textures;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RawTextureManager {
	/// <summary>
	/// Represents a certain kind of Melee .dat file, with certain textures at certain offsets.
	/// Each .json file in the Definitions folder is deserialized to an instance of this class.
	/// </summary>
	public class DatFileDefinition {
		/// <summary>
		/// The path to the JSON file this instance was deserialized from. This field is not serialized.
		/// </summary>
		[JsonIgnore]
		public string JsonFile { get; set; }
		/// <summary>
		/// A filename pattern to use this .dat file definition for, e.g. "*PlLk?.dat".
		/// Visual Basic's "Like" operator is used to match the filename against this pattern.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// An optional description that will appear in the "Available Definitions" window.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// The textures that are present in this kind of .dat file.
		/// </summary>
		public DatTextureDefinition[] Textures { get; set; }

		public override string ToString() {
			return string.Format("{0} ({1} textures)", Name, Textures.Length);
		}
	}

	/// <summary>
	/// Represents attributes that a certain texture in a .dat file has, including type/format, offset, and size.
	/// </summary>
	public class DatTextureDefinition {
		public string Name { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public WiiPixelFormat Type { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		[JsonConverter(typeof(HexIntConverter))]
		public int Location { get; set; }
		[JsonIgnore]
		public int MipLevels { get { return 1; } }
		public DatPaletteDefinition Palette { get; set; }

		public bool ShouldSerializePalette() {
			return Palette != null;
		}

		public int GetTextureSize() {
			return TextureConverter.Get(Type).GetMipOffset(Width, Height, MipLevels + 1);
		}
	}

	/// <summary>
	/// Represents attributes that a certain texture's palette in a .dat file has, including type/format, offset, and size.
	/// </summary>
	public class DatPaletteDefinition {
		[JsonConverter(typeof(StringEnumConverter))]
		public WiiPaletteFormat Type { get; set; }
		public int Colors { get; set; }
		[JsonConverter(typeof(HexIntConverter))]
		public int Location { get; set; }

		public int GetPaletteSize() {
			return Colors * 2;
		}
	}
}
