using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawTextureManager {
	public class DatFile {
		public string Name { get; private set; }
		public byte[] Data { get; private set; }
		public DatFileDefinition Definition { get; private set; }

		public bool Modified { get; set; }

		public DatFile(string name, DatFileDefinition definition) {
			Name = name;
			Data = File.ReadAllBytes(Name);
			Definition = definition;
			Modified = false;
		}
	}
}
