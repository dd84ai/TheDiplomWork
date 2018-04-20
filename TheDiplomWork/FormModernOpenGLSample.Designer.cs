namespace TheDiplomWork
{
    partial class FormModernOpenGLSample
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
        //123
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModernOpenGLSample));
            this.openGLControl = new SharpGL.OpenGLControl();
            this.label1_InfoTable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = true;
            this.openGLControl.FrameRate = 160;
            this.openGLControl.Location = new System.Drawing.Point(0, 0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(624, 391);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Click += new System.EventHandler(this.openGLControl_Click);
            this.openGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl_KeyDown);
            this.openGLControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.openGLControl_KeyPress);
            this.openGLControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.openGLControl_KeyUp);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseEnter += new System.EventHandler(this.openGLControl_MouseEnter);
            this.openGLControl.MouseLeave += new System.EventHandler(this.openGLControl_MouseLeave);
            this.openGLControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseScroller);
            // 
            // label1_InfoTable
            // 
            this.label1_InfoTable.AutoSize = true;
            this.label1_InfoTable.Location = new System.Drawing.Point(-3, 0);
            this.label1_InfoTable.Name = "label1_InfoTable";
            this.label1_InfoTable.Size = new System.Drawing.Size(35, 13);
            this.label1_InfoTable.TabIndex = 2;
            this.label1_InfoTable.Text = "label1";
            // 
            // FormModernOpenGLSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 391);
            this.Controls.Add(this.label1_InfoTable);
            this.Controls.Add(this.openGLControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormModernOpenGLSample";
            this.Text = "The Diplom Work";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModernOpenGLSample_FormClosing);
            this.LocationChanged += new System.EventHandler(this.FormModernOpenGLSample_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Label label1_InfoTable;
    }
}

