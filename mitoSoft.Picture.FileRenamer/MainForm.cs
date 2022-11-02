using mitoSoft.Picture.FileRenamer.Contracts;
using mitoSoft.Picture.FileRenamer.Exceptions;
using mitoSoft.Picture.FileRenamer.Extensions;
using mitoSoft.Picture.FileRenamer.Handler;
using mitoSoft.Picture.FileRenamer.Models;
using System.Runtime.CompilerServices;

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

        //Search
        private void SearchFiles_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FileListBox.Items.Clear();
                foreach (var file in this.openFileDialog.FileNames)
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
                var file = (FilePath)this.FileListBox.Items[j];

                if (File.Exists(file.FullName))
                {
                    this.toolStripStatusLabel.Text = file.FullName;
                    this.toolStripProgressBar.Value++;
                    Application.DoEvents();

                    try
                    {
                        if (file.Extension.ToLower() != ".jpg" &&
                            file.Extension.ToLower() != ".jpeg" &&
                            file.Extension.ToLower() != ".arw" &&
                            file.Extension.ToLower() != ".mov" &&  //MOV files from I-Phone
                            file.Extension.ToLower() != ".mp4")
                        {
                            throw new InvalidExtensionException("invalid file type.");
                        }

                        //*******Im Dateinamen existiert bereits ein DatumsSchlüssel************************

                        i += this.RenameFile(new FileNameHandler(), file);
                    }
                    catch (FormatException)
                    {
                        //*******Im Dateinamen existiert kein DatumsSchlüssel*******************************


                        if (file.Name.StartsWith("IMG-"))
                        {
                            i += this.RenameFile(new WhatsAppHandler(), file);
                        }
                        else if (file.Extension.ToLower() == ".jpg")
                        {
                            i += this.RenameFile(new JpegHandler(), file);
                        }
                        else if (file.Extension.ToLower() == ".arw")
                        {
                            i += this.RenameFile(new SonyHandler(), file);
                        }
                        else if (file.Extension.ToLower() == ".mp4")
                        {
                            i += this.RenameFile(new Mp4Handler(), file);
                        }
                        else if (file.Extension.ToLower() == ".mov")
                        {
                            i += this.RenameFile(new AppleHandler(), file);
                        }
                    }
                    catch (InvalidExtensionException) { }
                    catch (Exception) { }
                    finally
                    {
                        if (string.IsNullOrEmpty(file.Error))
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

        private int RenameFile(IHandler handler, FilePath path)
        {
            try
            {
                var file = new FileInfo(path.FullName);

                var date = handler.GetShootingDate(file);

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

                return 1;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (Exception ex)
            {
                path.Error = ex.Message;
                return 0;
            }
        }

        private void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormatString = this.FormatTextBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}