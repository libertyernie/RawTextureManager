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
		private byte[] currentFileData;
		private string currentFilePath;

		private OpenFileDialog openFileDialog;
		private List<DatFileDefinition> definitions;

		public MainForm() {
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
			definitions = new List<DatFileDefinition>();

			foreach (string file in Directory.EnumerateFiles("Definitions")) {
				definitions.Add(JsonConvert.DeserializeObject<DatFileDefinition>(File.ReadAllText(file)));
			}

			treeView1.AfterSelect += treeView1_AfterSelect;
		}

		void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node.Tag is DatTexture) {
				DatTexture t = (DatTexture)e.Node.Tag;
				goodPictureBox1.Picture = t.ExtractFrom(currentFileData);
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutForm().ShowDialog();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				currentFilePath = openFileDialog.FileName;
				currentFileData = File.ReadAllBytes(currentFilePath);

				DatFileDefinition def = definitions.Where(d => d.Name == Path.GetFileName(currentFilePath)).FirstOrDefault();

				treeView1.Nodes.Clear();
				if (def == null) return;

				foreach (var t in def.Textures) {
					TreeNode node = treeView1.Nodes.Add(t.Name);
					node.Tag = t;
				}
			}
		}
	}
}
