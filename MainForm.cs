using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RawTextureManager {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();

			List<DatFileDefinition> definitions = new List<DatFileDefinition>();

			foreach (string file in Directory.EnumerateFiles("Definitions")) {
				definitions.Add(JsonConvert.DeserializeObject<DatFileDefinition>(File.ReadAllText(file)));
			}

			foreach (string file in Directory.EnumerateFiles("C:/melee")) {
				DatFileDefinition def = definitions.Where(d => d.Name == Path.GetFileName(file)).FirstOrDefault();
				if (def == null) continue;

				byte[] data = File.ReadAllBytes(file);

				this.flowLayoutPanel1.Controls.Add(new Label { Text = file });
				foreach (Bitmap bmp in def.Textures.Select(t => t.ExtractFrom(data))) {
					this.flowLayoutPanel1.Controls.Add(new PictureBox {
						Image = bmp,
						Width = bmp.Width,
						Height = bmp.Height
					});
				}
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutForm().ShowDialog();
		}
	}
}
