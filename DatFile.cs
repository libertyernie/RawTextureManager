using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawTextureManager {
	public class DatFile {
		public string Name { get; set; }
		public byte[] Data { get; private set; }
		public List<DatTexture> Textures { get; private set; }

		public bool Modified { get; set; }

		public DatFile(string name, IEnumerable<DatTexture> textures) {
			Name = name;
			Data = File.ReadAllBytes(Name);
			Textures = textures.ToList();
			Modified = false;
		}
	}
}
