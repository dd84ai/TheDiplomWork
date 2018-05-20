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
            this.table_Menu_main = new System.Windows.Forms.TableLayoutPanel();
            this.button_Return = new System.Windows.Forms.Button();
            this.button_SaveAndExit = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.table_Menu_main.SuspendLayout();
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
            this.openGLControl.Size = new System.Drawing.Size(751, 520);
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
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            this.openGLControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseScroller);
            // 
            // table_Menu_main
            // 
            this.table_Menu_main.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.table_Menu_main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.table_Menu_main.BackColor = System.Drawing.Color.White;
            this.table_Menu_main.ColumnCount = 1;
            this.table_Menu_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_Menu_main.Controls.Add(this.button_About, 0, 1);
            this.table_Menu_main.Controls.Add(this.button_Return, 0, 0);
            this.table_Menu_main.Controls.Add(this.button_SaveAndExit, 0, 3);
            this.table_Menu_main.Controls.Add(this.Exit, 0, 2);
            this.table_Menu_main.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.table_Menu_main.Location = new System.Drawing.Point(184, 103);
            this.table_Menu_main.Name = "table_Menu_main";
            this.table_Menu_main.RowCount = 4;
            this.table_Menu_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.table_Menu_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.table_Menu_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.table_Menu_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.table_Menu_main.Size = new System.Drawing.Size(341, 352);
            this.table_Menu_main.TabIndex = 3;
            this.table_Menu_main.Visible = false;
            // 
            // button_Return
            // 
            this.button_Return.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_Return.FlatAppearance.BorderSize = 0;
            this.button_Return.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Return.Location = new System.Drawing.Point(5, 16);
            this.button_Return.Name = "button_Return";
            this.button_Return.Size = new System.Drawing.Size(331, 56);
            this.button_Return.TabIndex = 1;
            this.button_Return.Text = "Return";
            this.button_Return.UseVisualStyleBackColor = true;
            this.button_Return.Click += new System.EventHandler(this.button_Return_Click);
            // 
            // button_SaveAndExit
            // 
            this.button_SaveAndExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_SaveAndExit.FlatAppearance.BorderSize = 0;
            this.button_SaveAndExit.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SaveAndExit.Location = new System.Drawing.Point(5, 280);
            this.button_SaveAndExit.Name = "button_SaveAndExit";
            this.button_SaveAndExit.Size = new System.Drawing.Size(331, 56);
            this.button_SaveAndExit.TabIndex = 0;
            this.button_SaveAndExit.Text = "Save and Exit";
            this.button_SaveAndExit.UseVisualStyleBackColor = true;
            this.button_SaveAndExit.Click += new System.EventHandler(this.button_SaveAndExit_Click);
            // 
            // Exit
            // 
            this.Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(5, 194);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(331, 51);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // button_About
            // 
            this.button_About.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_About.FlatAppearance.BorderSize = 0;
            this.button_About.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_About.Location = new System.Drawing.Point(5, 106);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(331, 51);
            this.button_About.TabIndex = 3;
            this.button_About.Text = "About";
            this.button_About.UseVisualStyleBackColor = true;
            this.button_About.Click += new System.EventHandler(this.button_About_Click);
            // 
            // FormModernOpenGLSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 520);
            this.Controls.Add(this.table_Menu_main);
            this.Controls.Add(this.openGLControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormModernOpenGLSample";
            this.Text = "The Diplom Work";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModernOpenGLSample_FormClosing);
            this.LocationChanged += new System.EventHandler(this.FormModernOpenGLSample_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.table_Menu_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal SharpGL.OpenGLControl openGLControl;
        internal System.Windows.Forms.TableLayoutPanel table_Menu_main;
        private System.Windows.Forms.Button button_SaveAndExit;
        private System.Windows.Forms.Button button_Return;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button button_About;
    }
}

