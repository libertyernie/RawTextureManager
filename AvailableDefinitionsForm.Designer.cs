namespace RawTextureManager {
	partial class AvailableDefinitionsForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.json = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.texcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.json,
            this.name,
            this.description,
            this.texcount});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.Size = new System.Drawing.Size(584, 361);
			this.dataGridView1.TabIndex = 0;
			// 
			// json
			// 
			this.json.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.json.HeaderText = "JSON file";
			this.json.Name = "json";
			this.json.ReadOnly = true;
			// 
			// name
			// 
			this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.name.HeaderText = "Filename pattern";
			this.name.Name = "name";
			this.name.ReadOnly = true;
			this.name.Width = 120;
			// 
			// description
			// 
			this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.description.HeaderText = "Description";
			this.description.Name = "description";
			this.description.ReadOnly = true;
			// 
			// texcount
			// 
			this.texcount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.texcount.HeaderText = "Textures";
			this.texcount.Name = "texcount";
			this.texcount.ReadOnly = true;
			this.texcount.Width = 60;
			// 
			// AvailableDefinitionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.dataGridView1);
			this.Name = "AvailableDefinitionsForm";
			this.Text = "Available Definitions";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn json;
		private System.Windows.Forms.DataGridViewTextBoxColumn name;
		private System.Windows.Forms.DataGridViewTextBoxColumn description;
		private System.Windows.Forms.DataGridViewTextBoxColumn texcount;
	}
}