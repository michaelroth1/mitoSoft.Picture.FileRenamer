using mitoSoft.Media.Commmon;
using mitoSoft.Picture.FileRenamer.Extensions;
using mitoSoft.Picture.FileRenamer.Models;

namespace mitoSoft.Picture.FileRenamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel.Text = string.Empty;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormatString))
            {
                this.FormatTextBox.Text = Properties.Settings.Default.FormatString;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.FileListBox.Items.Clear();
        }

        //Search
        private void SearchFiles_Click(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in this.OpenFileDialog.FileNames)
                {
                    var filePath = new FilePath()
                    {
                        FullName = file,
                    };
                    this.FileListBox.Items.Add(filePath);
                    this.toolStripStatusLabel.Text = $"{this.FileListBox.Items.Count} file(s) selected";
                }
            }
        }

        //Renaming
        private void Ok_Click(object sender, EventArgs e)
        {
            if (this.FileListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("no files selected");
            }

            int i = 0;
            this.toolStripProgressBar.Minimum = 0;
            this.toolStripProgressBar.Maximum = this.FileListBox.Items.Count;
            this.toolStripProgressBar.Value = 0;
            for (int j = this.FileListBox.Items.Count - 1; j >= 0; j--)
            {
                var model = (FilePath)this.FileListBox.Items[j];

                var file = new FileInfo(model.FullName);

                if (File.Exists(file.FullName))
                {
                    this.toolStripStatusLabel.Text = file.FullName;
                    this.toolStripProgressBar.Value++;
                    Application.DoEvents();

                    try
                    {
                        var date = (new MediaFileHandler()).GetCreationDate(file);

                        var dateString = date.ToString(this.FormatTextBox.Text);

                        var newName = $"{dateString}";

                        if ($"{newName}{file.Extension}" != file.Name)
                        {
                            if (File.Exists(@$"{file.Directory}\{newName}{file.Extension}"))
                            {
                                throw new InvalidOperationException($"'{newName}{file.Extension}' could not renamed - it already exists.");
                            }

                            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(file.FullName, $"{newName}{file.Extension}");
                        }

                        i++;
                    }
                    catch (Exception ex)
                    {
                        model.Error = ex.Message;
                    }
                    finally
                    {
                        if (string.IsNullOrEmpty(model.Error))
                        {
                            this.FileListBox.Items.RemoveAt(j);
                        }
                        else
                        {
                            this.FileListBox.Items.Refresh(j);
                        }
                    }
                }
            }

            this.toolStripStatusLabel.Text = $"{i} file(s) renamed";
            Application.DoEvents();
        }

        private void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormatString = this.FormatTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = (FilePath)this.FileListBox.SelectedItem;

            var dir = new FileInfo(file.FullName).DirectoryName;

            this.OpenFileDialog.InitialDirectory = dir;

            this.OpenFileDialog.ShowDialog();
        }

        private void FileListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                FileListBox.SelectedIndex = FileListBox.IndexFromPoint(e.Location);
                if (FileListBox.SelectedIndex != -1)
                {
                    this.contextMenuStrip.Show(this, new Point(e.X, e.Y));
                }
            }
        }
    }
}