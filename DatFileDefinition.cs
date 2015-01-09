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
			IntPtr texdata = (IntPtr)(header + 1);
			int texsize = header->_header._size - sizeof(TEX0v1);

			Marshal.Copy(
				texdata,
				file,
				Location,
				texsize);

			if (paletteMap != null) {
				Palette.ReplaceIn(file, (PLT0v1*)paletteMap.Address);
			}

			textureMap.Dispose();
			if (paletteMap != null) paletteMap.Dispose();
		}

		public unsafe Bitmap ExtractFrom(byte[] file) {
			TextureConverter texconv = TextureConverter.Get(Type);

			int texsize = texconv.GetMipOffset(Width, Height, 1 + 1);
			using (UnsafeBuffer texbuf = new UnsafeBuffer(sizeof(TEX0v1) + texsize)) {
				TEX0v1* header = (TEX0v1*)texbuf.Address;
				IntPtr texdata = (IntPtr)(header + 1);

				*header = new TEX0v1(Width, Height, Type, 1);
				Marshal.Copy(
					file,
					Location,
					texdata,
					texsize);

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
			IntPtr pltdata = (IntPtr)(plt0 + 1);
			int pltsize = plt0->_bresEntry._size - sizeof(PLT0v1);

			Marshal.Copy(
				pltdata,
				file,
				Location,
				pltsize);
		}

		public unsafe UnsafeBuffer ExtractAsPLT0(byte[] file) {
			int pltsize = Colors * 2;
			UnsafeBuffer pltbuf = new UnsafeBuffer(pltsize + sizeof(PLT0v1));

			PLT0v1* header = (PLT0v1*)pltbuf.Address;
			IntPtr pltdata = (IntPtr)(header + 1);

			*header = new PLT0v1(Colors, Type);
			Marshal.Copy(
				file,
				Location,
				pltdata,
				pltsize);

			return pltbuf;
		}
	}
}
