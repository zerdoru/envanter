﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Envanter
{
    public partial class MDIParent1 : Form
    {
        private readonly Dictionary<Type, Form> _formsShown = new Dictionary<Type, Form>();
        private int childFormNumber;

        public MDIParent1()
        {
            InitializeComponent();

            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            var childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren) childForm.Close();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateChildIfNotExist(typeof(Registration));
        }

        private void ManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateChildIfNotExist(typeof(Item));
        }

        private void CreateChildIfNotExist(Type type)
        {
            if (_formsShown.TryGetValue(type, out var form))
            {
                form.BringToFront();
                return;
            }

            form = (Form) Activator.CreateInstance(type);

            form.MdiParent = this;
            form.Show();
            form.Closing += (o, args) => _formsShown.Remove(type);

            _formsShown.Add(type, form);
        }
    }
}