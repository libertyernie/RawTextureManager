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

			DatFile PlKbNr = JsonConvert.DeserializeObject<DatFile>(File.ReadAllText("C:/Users/Owner/Desktop/kirby.json"));
			byte[] data = File.ReadAllBytes("C:/melee/PlKbNr.dat");

			foreach (Bitmap bmp in PlKbNr.Textures.Select(t => t.ExtractFrom(data))) {
				this.flowLayoutPanel1.Controls.Add(new PictureBox {
					Image = bmp,
					Width = bmp.Width,
					Height = bmp.Height
				});
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutForm().ShowDialog();
		}
	}
}
