namespace PrzetwarzanieObrazow
{
    partial class Main
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMaximumValue = new System.Windows.Forms.Label();
            this.labelDvThreshold = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMaximumValue
            // 
            this.labelMaximumValue.AutoSize = true;
            this.labelMaximumValue.Location = new System.Drawing.Point(13, 13);
            this.labelMaximumValue.Name = "labelMaximumValue";
            this.labelMaximumValue.Size = new System.Drawing.Size(46, 17);
            this.labelMaximumValue.TabIndex = 0;
            this.labelMaximumValue.Text = "label1";
            // 
            // labelDvThreshold
            // 
            this.labelDvThreshold.AutoSize = true;
            this.labelDvThreshold.Location = new System.Drawing.Point(12, 49);
            this.labelDvThreshold.Name = "labelDvThreshold";
            this.labelDvThreshold.Size = new System.Drawing.Size(46, 17);
            this.labelDvThreshold.TabIndex = 1;
            this.labelDvThreshold.Text = "label2";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(13, 70);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(46, 17);
            this.labelPosition.TabIndex = 2;
            this.labelPosition.Text = "label1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 160);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelDvThreshold);
            this.Controls.Add(this.labelMaximumValue);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMaximumValue;
        private System.Windows.Forms.Label labelDvThreshold;
        private System.Windows.Forms.Label labelPosition;
    }
}

