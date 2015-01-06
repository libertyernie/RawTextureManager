using BrawlLib.IO;
using BrawlLib.SSBB.ResourceNodes;
using BrawlLib.SSBBTypes;
using BrawlLib.Wii.Textures;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RawTextureManager {
	public class DatFileDefinition {
		public string Name { get; set; }
		public DatTexture[] Textures { get; set; }
	}

	public class DatTexture {
		public string Name { get; set; }
		public WiiPixelFormat Type { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Location { get; set; }
		public DatPalette Palette { get; set; }

		public unsafe void ReplaceIn(byte[] file, Bitmap bmp) {
			TextureConverter texconv = TextureConverter.Get(Type);
			FileMap paletteMap = null;
			FileMap textureMap = (Palette == null)
				? texconv.EncodeTEX0Texture(bmp, 1)
				: texconv.EncodeTextureIndexed(bmp, 1, Palette.Colors, Palette.Type, QuantizationAlgorithm.MedianCut, out paletteMap);
			TEX0v1* header = (TEX0v1*)textureMap.Address;
			Marshal.Copy((IntPtr)((byte*)header + TEX0v1.Size), file, Location, header->_header._size - TEX0v1.Size);

			if (paletteMap != null) {
				Palette.ReplaceIn(file, (PLT0v1*)paletteMap.Address);
			}

			textureMap.Dispose();
			if (paletteMap != null) paletteMap.Dispose();
		}

		public unsafe Bitmap ExtractFrom(byte[] file) {
			TextureConverter texconv = TextureConverter.Get(Type);

			int texsize = texconv.GetMipOffset(Width, Height, 1 + 1);
			using (UnsafeBuffer texbuf = new UnsafeBuffer(TEX0v1.Size + texsize)) {
				TEX0v1* header = (TEX0v1*)texbuf.Address;
				byte* texdata = (byte*)header + TEX0v1.Size;

				*header = new TEX0v1(Width, Height, Type, 1);
				Marshal.Copy(file, Location, (IntPtr)texdata, texsize);

				if (Palette == null) {
					return texconv.DecodeTexture(header);
				} else {
					using (UnsafeBuffer pltbuf = Palette.ExtractAsPLT0(file)) {
						return texconv.DecodeTextureIndexed(header, (PLT0v1*)pltbuf.Address, 1);
					}
				}
			}
		}
	}

	public class DatPalette {
		public WiiPaletteFormat Type { get; set; }
		public int Colors { get; set; }
		public int Location { get; set; }

		public unsafe void ReplaceIn(byte[] file, PLT0v1* plt0) {
			Marshal.Copy((IntPtr)((byte*)plt0 + PLT0v1.Size), file, Location, plt0->_bresEntry._size - PLT0v1.Size);
		}

		public unsafe UnsafeBuffer ExtractAsPLT0(byte[] file) {
			int pltsize = Colors * 2;
			UnsafeBuffer pltbuf = new UnsafeBuffer(pltsize + PLT0v1.Size);

			PLT0v1* header = (PLT0v1*)pltbuf.Address;
			byte* pltdata = (byte*)header + PLT0v1.Size;

			*header = new PLT0v1(Colors, Type);
			Marshal.Copy(file, Location, (IntPtr)pltdata, pltsize);

			return pltbuf;
		}
	}
}
