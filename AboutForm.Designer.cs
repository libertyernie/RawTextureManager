namespace RawTextureManager {
	partial class AboutForm {
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
			this.software_title = new System.Windows.Forms.Label();
			this.version = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.brawllib = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.copyright = new System.Windows.Forms.Label();
			this.jsonnet = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// software_title
			// 
			this.software_title.AutoSize = true;
			this.software_title.Dock = System.Windows.Forms.DockStyle.Left;
			this.software_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.software_title.Location = new System.Drawing.Point(0, 0);
			this.software_title.Name = "software_title";
			this.software_title.Size = new System.Drawing.Size(118, 20);
			this.software_title.TabIndex = 0;
			this.software_title.Text = "software_title";
			// 
			// version
			// 
			this.version.AutoSize = true;
			this.version.Dock = System.Windows.Forms.DockStyle.Right;
			this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.version.Location = new System.Drawing.Point(326, 0);
			this.version.Name = "version";
			this.version.Size = new System.Drawing.Size(66, 20);
			this.version.TabIndex = 1;
			this.version.Text = "version";
			this.version.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(392, 161);
			this.textBox1.TabIndex = 4;
			// 
			// brawllib
			// 
			this.brawllib.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.brawllib.Location = new System.Drawing.Point(0, 193);
			this.brawllib.Name = "brawllib";
			this.brawllib.Size = new System.Drawing.Size(392, 32);
			this.brawllib.TabIndex = 5;
			this.brawllib.Text = "brawllib";
			this.brawllib.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.version);
			this.panel1.Controls.Add(this.software_title);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 24);
			this.panel1.TabIndex = 5;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Controls.Add(this.jsonnet);
			this.panel2.Controls.Add(this.brawllib);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(392, 225);
			this.panel2.TabIndex = 6;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.copyright);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 24);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(392, 16);
			this.panel3.TabIndex = 6;
			// 
			// copyright
			// 
			this.copyright.AutoSize = true;
			this.copyright.Dock = System.Windows.Forms.DockStyle.Left;
			this.copyright.Location = new System.Drawing.Point(0, 0);
			this.copyright.Name = "copyright";
			this.copyright.Size = new System.Drawing.Size(50, 13);
			this.copyright.TabIndex = 0;
			this.copyright.Text = "copyright";
			// 
			// jsonnet
			// 
			this.jsonnet.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.jsonnet.Location = new System.Drawing.Point(0, 161);
			this.jsonnet.Name = "jsonnet";
			this.jsonnet.Size = new System.Drawing.Size(392, 32);
			this.jsonnet.TabIndex = 6;
			this.jsonnet.Text = "jsonnet";
			this.jsonnet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 265);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Name = "AboutForm";
			this.Text = "About";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label software_title;
		private System.Windows.Forms.Label version;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label brawllib;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label copyright;
		private System.Windows.Forms.Label jsonnet;
	}
}