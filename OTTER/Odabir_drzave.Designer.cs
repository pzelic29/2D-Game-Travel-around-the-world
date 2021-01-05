namespace OTTER
{
    partial class Odabir_drzave
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSpanjolska = new System.Windows.Forms.Button();
            this.buttonItalija = new System.Windows.Forms.Button();
            this.buttonGrcka = new System.Windows.Forms.Button();
            this.btnNatrag = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSpanjolska
            // 
            this.buttonSpanjolska.BackColor = System.Drawing.Color.Teal;
            this.buttonSpanjolska.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSpanjolska.Location = new System.Drawing.Point(362, 276);
            this.buttonSpanjolska.Name = "buttonSpanjolska";
            this.buttonSpanjolska.Size = new System.Drawing.Size(26, 25);
            this.buttonSpanjolska.TabIndex = 0;
            this.buttonSpanjolska.Text = "E";
            this.buttonSpanjolska.UseVisualStyleBackColor = false;
            this.buttonSpanjolska.Click += new System.EventHandler(this.buttonSpanjolska_Click);
            this.buttonSpanjolska.Leave += new System.EventHandler(this.buttonSpanjolska_Leave);
            this.buttonSpanjolska.MouseEnter += new System.EventHandler(this.buttonSpanjolska_MouseEnter);
            // 
            // buttonItalija
            // 
            this.buttonItalija.BackColor = System.Drawing.Color.Teal;
            this.buttonItalija.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonItalija.Location = new System.Drawing.Point(447, 267);
            this.buttonItalija.Name = "buttonItalija";
            this.buttonItalija.Size = new System.Drawing.Size(26, 25);
            this.buttonItalija.TabIndex = 1;
            this.buttonItalija.Text = "I";
            this.buttonItalija.UseVisualStyleBackColor = false;
            this.buttonItalija.Click += new System.EventHandler(this.button2_Click);
            this.buttonItalija.MouseEnter += new System.EventHandler(this.buttonItalija_MouseEnter);
            this.buttonItalija.MouseLeave += new System.EventHandler(this.buttonItalija_MouseLeave);
            // 
            // buttonGrcka
            // 
            this.buttonGrcka.BackColor = System.Drawing.Color.Teal;
            this.buttonGrcka.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonGrcka.Location = new System.Drawing.Point(525, 286);
            this.buttonGrcka.Name = "buttonGrcka";
            this.buttonGrcka.Size = new System.Drawing.Size(26, 25);
            this.buttonGrcka.TabIndex = 2;
            this.buttonGrcka.Text = "G";
            this.buttonGrcka.UseVisualStyleBackColor = false;
            this.buttonGrcka.Click += new System.EventHandler(this.buttonGrcka_Click);
            this.buttonGrcka.MouseEnter += new System.EventHandler(this.buttonGrcka_MouseEnter);
            this.buttonGrcka.MouseLeave += new System.EventHandler(this.buttonGrcka_MouseLeave);
            // 
            // btnNatrag
            // 
            this.btnNatrag.BackgroundImage = global::OTTER.Properties.Resources.natrag;
            this.btnNatrag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNatrag.Location = new System.Drawing.Point(2, 524);
            this.btnNatrag.Name = "btnNatrag";
            this.btnNatrag.Size = new System.Drawing.Size(50, 48);
            this.btnNatrag.TabIndex = 3;
            this.btnNatrag.UseVisualStyleBackColor = true;
            this.btnNatrag.Click += new System.EventHandler(this.btnNatrag_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(301, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 4;
            // 
            // Odabir_drzave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OTTER.Properties.Resources.slika_svijeta;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(934, 571);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNatrag);
            this.Controls.Add(this.buttonGrcka);
            this.Controls.Add(this.buttonItalija);
            this.Controls.Add(this.buttonSpanjolska);
            this.Name = "Odabir_drzave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Around the World";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSpanjolska;
        private System.Windows.Forms.Button buttonItalija;
        private System.Windows.Forms.Button buttonGrcka;
        private System.Windows.Forms.Button btnNatrag;
        private System.Windows.Forms.Label label1;
    }
}