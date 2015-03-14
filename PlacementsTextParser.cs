using BrawlLib.Imaging;
using BrawlLib.Wii.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RawTextureManager {
	public static class PlacementsTextParser {
		private static bool ContainsAlpha (Bitmap b) {
			for (int y = 0; y < b.Height; y++) {
				for (int x = 0; x < b.Width; x++) {
					if (b.GetPixel(x, y).A != 255) {
						return true;
					}
				}
			}
			return false;
		}

		public static DatFileDefinition ParseFile(string txtPath) {
			Regex rDefline = new Regex(@"^((\d\d)? ?- +)?(\d\d([^ ]*) - )?([^ ]*)( \((\d*) lines\*?\))?( \*)?");
			List<DatTextureDefinition> list = new List<DatTextureDefinition>();
			int lastNumColors = 0;
			using (FileStream stream = new FileStream(txtPath, FileMode.Open, FileAccess.Read)) {
				StreamReader sr = new StreamReader(stream);
				for (string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
					try {
						Console.WriteLine(line);
						if (line.StartsWith("~") || line.Length == 0) continue;

						var match = rDefline.Match(line);

						DatTextureDefinition tex = new DatTextureDefinition();
						string number = match.Groups[2].Value;
						tex.Name = match.Groups[4].Value;
						if (string.IsNullOrWhiteSpace(tex.Name)) {
							IEnumerable<string> matchingDirectories = Directory.EnumerateDirectories(Path.GetDirectoryName(txtPath), number + "*");
							tex.Name = matchingDirectories.Any() ? Path.GetFileName(matchingDirectories.First()).Substring(2) : number;
						}
						tex.Location = int.Parse(match.Groups[5].Value, System.Globalization.NumberStyles.HexNumber);
						if (match.Groups[7].Success) {
							tex.Palette = new DatPaletteDefinition();
							tex.Palette.Colors = int.Parse(match.Groups[7].Value) * 8;
							lastNumColors = tex.Palette.Colors;
						} else if (match.Groups[8].Success) {
							tex.Palette = new DatPaletteDefinition();
							tex.Palette.Colors = lastNumColors;
						}
						list.Add(tex);
					} catch (Exception e) {
						throw new Exception("Could not parse line: \"" + line + "\"", e);
					}
				}
			}

			List<Tuple<int, int>> inUse = new List<Tuple<int, int>>();
			Regex rNamePattern = new Regex(@"txt_\d\d\d\d_(\d*).tga");
			list = list.OrderBy(t => t.Location).ToList();
			for (int i = 0; i < list.Count(); i++) {
				DatTextureDefinition tex = list[i];

				IEnumerable<string> directories = Directory.EnumerateDirectories(Path.GetDirectoryName(txtPath), "*" + tex.Name);
				if (!directories.Any()) {
					tex.Name = Microsoft.VisualBasic.Interaction.InputBox("Could not find texture " + tex.Name + ". Does it have a different name in the folder?");
					directories = Directory.EnumerateDirectories(Path.GetDirectoryName(txtPath), "*" + tex.Name);
				}
				IEnumerable<string> filenames = Directory.EnumerateFiles(directories.First(), "*.tga");
				foreach (string filename in filenames) {
					Match match = rNamePattern.Match(filename);
					if (!match.Success) continue;

					int textureType = int.Parse(match.Groups[1].Value);
					tex.Type = (WiiPixelFormat)textureType;

					Bitmap bmp = TGA.FromFile(filename);
					tex.Width = bmp.Width;
					tex.Height = bmp.Height;

					inUse.Add(new Tuple<int, int>(tex.Location, tex.Location + tex.GetTextureSize()));

					if (tex.Palette != null) {
						tex.Palette.Type = ContainsAlpha(bmp) ? WiiPaletteFormat.RGB5A3 : WiiPaletteFormat.RGB565;
					}
					break;
				}
			}
			for (int i = 0; i < list.Count(); i++) {
				DatTextureDefinition tex = list[i];
				if (tex.Palette != null) {
					int loc = tex.Location + tex.GetTextureSize();

					while (true) {
						Tuple<int, int> spanInside = inUse.FirstOrDefault(t => loc >= t.Item1 && loc < t.Item2);
						Console.WriteLine("Checking if " + tex.Name + " 0x" + loc.ToString("X") + " is inside any spans");
						if (spanInside == null) break;
						Console.WriteLine("Inside 0x" + spanInside.Item1.ToString("X") + " - 0x" + spanInside.Item2.ToString("X"));

						loc = spanInside.Item2;
					}

					tex.Palette.Location = loc;
					inUse.Add(new Tuple<int, int>(loc, loc + tex.Palette.GetPaletteSize() + 32));
				}
			}

			DatFileDefinition def = new DatFileDefinition();
			def.Name = "*" + Path.GetFileName(Directory.EnumerateFiles(Path.GetDirectoryName(txtPath), "*.dat").FirstOrDefault() ?? "unknown");
			def.Textures = list.ToArray();
			return def;
		}
	}
}
