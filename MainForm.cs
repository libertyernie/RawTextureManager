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
		private OpenFileDialog openFileDialog;
		private List<DatFileDefinition> definitions;
		private Dictionary<TreeNode, PictureBox> pbs;

		public MainForm() {
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
			definitions = new List<DatFileDefinition>();
			pbs = new Dictionary<TreeNode, PictureBox>();

			foreach (string file in Directory.EnumerateFiles("Definitions")) {
				definitions.Add(JsonConvert.DeserializeObject<DatFileDefinition>(File.ReadAllText(file)));
			}

			foreach (string file in Directory.EnumerateFiles("C:/melee")) {
				DatFileDefinition def = definitions.Where(d => d.Name == Path.GetFileName(file)).FirstOrDefault();
				if (def == null) continue;

				byte[] data = File.ReadAllBytes(file);

				TreeNode node = this.treeView1.Nodes.Add(file);
				foreach (var t in def.Textures) {
					Bitmap bmp = t.ExtractFrom(data);
					PictureBox pb = new PictureBox {
						Image = bmp,
						Width = bmp.Width,
						Height = bmp.Height
					};
					pb.Click += (o, e) => {
						if (openFileDialog.ShowDialog() == DialogResult.OK) {
							t.ReplaceIn(data, BrawlLib.Imaging.TGA.FromFile(openFileDialog.FileName));
							pb.Image = t.ExtractFrom(data);
						}
					};
					TreeNode node2 = node.Nodes.Add(t.Name);
					pbs.Add(node2, pb);
				}
			}
			treeView1.AfterSelect += treeView1_AfterSelect;
		}

		void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			PictureBox pb;
			if (pbs.TryGetValue(e.Node, out pb)) {
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add(pb);
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutForm().ShowDialog();
		}
	}
}
