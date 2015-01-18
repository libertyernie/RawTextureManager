﻿using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RawTextureManager {
	public partial class MainForm : Form {
		public static readonly IReadOnlyCollection<DatFileDefinition> Definitions;

		static MainForm() {
			var definitions = new List<DatFileDefinition>();

			foreach (string file in Directory.EnumerateFiles("Definitions")) {
				try {
					definitions.Add(JsonConvert.DeserializeObject<DatFileDefinition>(File.ReadAllText(file)));
				} catch (JsonSerializationException e) {
					string filename = Path.GetFileName(file);
					Console.WriteLine(filename + ": " + e.GetType() + ": " + e.Message + Environment.NewLine + e.StackTrace);
					MessageBox.Show("Could not parse " + filename + ": " + e.Message, "Error in " + filename);
				}
			}

			Definitions = definitions.AsReadOnly();
		}

		private DatFile currentFile;

		private OpenFileDialog openFileDialog;
		private SaveFileDialog saveFileDialog;

		public MainForm() {
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
			saveFileDialog = new SaveFileDialog();

			treeView1.AfterSelect += treeView1_AfterSelect;
		}

		void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node.Tag is DatTexture) {
				DatTexture t = (DatTexture)e.Node.Tag;
				goodPictureBox1.Picture = t.Extract();
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutForm().ShowDialog();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			if (currentFile != null) {
				openFileDialog.InitialDirectory = Path.GetDirectoryName(currentFile.Name);
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				List<DatFileDefinition> defs = Definitions.Where(d => Operators.LikeString(Path.GetFileName(openFileDialog.FileName), d.Name, Microsoft.VisualBasic.CompareMethod.Text)).ToList();
				if (!defs.Any()) {
					MessageBox.Show("No definition found in the Definitions folder that matches the filename " + Path.GetFileName(openFileDialog.FileName) + ".",
						"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				currentFile = new DatFile(openFileDialog.FileName, defs.SelectMany(d => d.Textures));

				treeView1.Nodes.Clear();
				foreach (DatTexture t in currentFile.Textures) {
					TreeNode node = treeView1.Nodes.Add(t.Definition.Name);
					node.Tag = t;
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			if (currentFile == null) return;
			File.WriteAllBytes(currentFile.Name, currentFile.Data);
			currentFile.Modified = false;
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (currentFile == null) return;
			saveFileDialog.InitialDirectory = Path.GetDirectoryName(currentFile.Name);
			saveFileDialog.FileName = currentFile.Name;
			if (saveFileDialog.ShowDialog() == DialogResult.OK) {
				currentFile.Name = saveFileDialog.FileName;
				File.WriteAllBytes(currentFile.Name, currentFile.Data);
				currentFile.Modified = false;
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
			treeView1.Nodes.Clear();
			currentFile = null;
		}
	}
}
