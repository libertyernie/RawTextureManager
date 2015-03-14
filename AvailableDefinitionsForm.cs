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
	public partial class AvailableDefinitionsForm : Form {
		public AvailableDefinitionsForm() {
			InitializeComponent();

			this.Shown += AvailableDefinitionsForm_Shown;
		}

		void AvailableDefinitionsForm_Shown(object sender, EventArgs e) {
			foreach (var def in MainForm.Definitions) {
				dataGridView1.Rows.Add(new object[] { Path.GetFileName(def.JsonFile), def.Name, def.Description, def.Textures.Length });
			}
		}
	}
}
