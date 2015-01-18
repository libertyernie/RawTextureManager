using BrawlLib.Imaging;
using BrawlLib.IO;
using BrawlLib.SSBBTypes;
using BrawlLib.Wii.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RawTextureManager {
	public class DatFile {
		public string Name { get; set; }
		public byte[] Data { get; private set; }
		public IReadOnlyCollection<DatTexture> Textures { get; private set; }

		public bool Modified { get; set; }

		public DatFile(string name, IEnumerable<DatTextureDefinition> textures) {
			Name = name;
			Data = File.ReadAllBytes(Name);
			Textures = textures.Select(t => new DatTexture(this, t)).ToList().AsReadOnly();
			Modified = false;
		}
	}

	public class DatTexture {
		public DatFile Parent { get; private set; }
		public DatTextureDefinition Definition { get; private set; }
		public DatPalette Palette { get; private set; }

		public DatTexture(DatFile parent, DatTextureDefinition definition) {
			this.Parent = parent;
			this.Definition = definition;
			this.Palette = definition.Palette == null
				? null
				: new DatPalette(parent, definition.Palette);
		}

		public unsafe void Replace(Bitmap bmp) {
			DatTextureDefinition D = Definition;
			TextureConverter texconv = TextureConverter.Get(D.Type);

			FileMap paletteMap = null;
			FileMap textureMap = (D.Palette == null)
				? texconv.EncodeTEX0Texture(bmp, D.MipLevels)
				: texconv.EncodeTextureIndexed(bmp, D.MipLevels, D.Palette.Colors, D.Palette.Type, QuantizationAlgorithm.MedianCut, out paletteMap);

			TEX0v1* header = (TEX0v1*)textureMap.Address;

			Marshal.Copy(
				header->PixelData,
				Parent.Data,
				D.Location,
				header->PixelDataLength);

			if (paletteMap != null) {
				Palette.Replace((PLT0v1*)paletteMap.Address);
			}

			textureMap.Dispose();
			if (paletteMap != null) paletteMap.Dispose();
		}

		public unsafe Bitmap Extract() {
			DatTextureDefinition D = Definition;
			TextureConverter texconv = TextureConverter.Get(D.Type);

			int texsize = texconv.GetMipOffset(D.Width, D.Height, D.MipLevels + 1);
			using (UnsafeBuffer texbuf = new UnsafeBuffer(sizeof(TEX0v1) + texsize)) {
				TEX0v1* header = (TEX0v1*)texbuf.Address;

				*header = new TEX0v1(D.Width, D.Height, D.Type, D.MipLevels);
				Marshal.Copy(
					Parent.Data,
					D.Location,
					header->PixelData,
					texsize);

				if (D.Palette == null) {
					return texconv.DecodeTexture(header);
				} else {
					using (UnsafeBuffer pltbuf = Palette.ExtractPLT0()) {
						return texconv.DecodeTextureIndexed(header, (PLT0v1*)pltbuf.Address, D.MipLevels);
					}
				}
			}
		}
	}

	public class DatPalette {
		public DatFile Parent { get; private set; }
		public DatPaletteDefinition Definition { get; private set; }

		public DatPalette(DatFile parent, DatPaletteDefinition definition) {
			this.Parent = parent;
			this.Definition = definition;
		}

		public unsafe void Replace(PLT0v1* plt0) {
			Marshal.Copy(
				plt0->PaletteData,
				Parent.Data,
				Definition.Location,
				plt0->PaletteDataLength);
		}

		public unsafe UnsafeBuffer ExtractPLT0() {
			int pltsize = Definition.Colors * 2;
			UnsafeBuffer pltbuf = new UnsafeBuffer(pltsize + sizeof(PLT0v1));

			PLT0v1* header = (PLT0v1*)pltbuf.Address;

			*header = new PLT0v1(Definition.Colors, Definition.Type);
			Marshal.Copy(
				Parent.Data,
				Definition.Location,
				header->PaletteData,
				pltsize);

			return pltbuf;
		}
	}
}
