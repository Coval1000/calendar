namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.calendar1 = new WindowsFormsApplication1.Calendar();
            this.SuspendLayout();
            // 
            // calendar1
            // 
            this.calendar1.bottom = 0;
            this.calendar1.bottomInner = 0;
            this.calendar1.gap = new WindowsFormsApplication1.Calendar.LukaSiatki(0, 0, 0, 0, WindowsFormsApplication1.Calendar.Centred.None);
            this.calendar1.grid = new WindowsFormsApplication1.Calendar.GridBg(0, 0, 0, 0, WindowsFormsApplication1.Calendar.Centred.None);
            this.calendar1.imgB_lewy = ((System.Drawing.Image)(resources.GetObject("calendar1.imgB_lewy")));
            this.calendar1.imgB_prawy = ((System.Drawing.Image)(resources.GetObject("calendar1.imgB_prawy")));
            this.calendar1.left = 0;
            this.calendar1.leftInner = 0;
            this.calendar1.Location = new System.Drawing.Point(147, 73);
            this.calendar1.monthName = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.calendar1.Name = "calendar1";
            this.calendar1.panelHigh = 100;
            this.calendar1.right = 0;
            this.calendar1.rightInner = 0;
            this.calendar1.Size = new System.Drawing.Size(454, 323);
            this.calendar1.TabIndex = 0;
            this.calendar1.Text = "calendar1";
            this.calendar1.top = 0;
            this.calendar1.topInner = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 430);
            this.Controls.Add(this.calendar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Calendar calendar1;
    }
}

