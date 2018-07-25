using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AndroidTranslationEditor
{
    public partial class MainWindow : Form
    {
        List<List<XMLValue>> languageLists = new List<List<XMLValue>>();
        List<String> keyList = new List<string>();
        List<String> openFiles = new List<string>();
        Boolean loadingFile = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyDesign();
        }

        /// <summary>
        /// Function that get executed when the OpenFile button has been pressed
        /// and it loads a user specified XML file and set everythin up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "XML files|*.xml";
            fileDialog.ShowDialog();          

            if (fileDialog.FileName != "")
            {
                loadingFile = true;
                AddFileEntry(fileDialog.FileName, fileDialog.SafeFileName);
                loadingFile = false;
            }           
        }

        /// <summary>
        /// Function that get executed when the CreateFile button has been pressed
        /// and it creates a empty xml file at a user set location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "XML files|*.xml";
            fileDialog.ShowDialog();

            string path = fileDialog.FileName;

            if (path != "")
            {
                FileStream file = System.IO.File.Create(path);
                Byte[] emptyXMLstring = new UTF8Encoding().GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<resources>\n</resources>");

                file.Write(emptyXMLstring, 0, emptyXMLstring.Length);
                file.Close();

                loadingFile = true;
                AddFileEntry(path, System.IO.Path.GetFileName(path));
                loadingFile = false;
            }
        }

        /// <summary>
        /// Function that get executed when a cell has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            if (!loadingFile)
            {
                XMLprocessing.XMLWrite(openFiles[e.ColumnIndex - 1], keyList[e.RowIndex], (string)table.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        /// <summary>
        /// Function that adds: 
        /// - the current XML file to the languageList List
        /// - the XML file path to the openFile List
        /// calls FillTable function
        /// </summary>
        /// <param name="path">Path to XML source file</param>
        /// <param name="name">XML source file name</param>
        public void AddFileEntry(string path, string name)
        {
            try
            {
                languageLists.Add(XMLprocessing.XMLRead(path));
                openFiles.Add(path);
                FillTable(path, name);
            }
            catch (XmlException e)
            {
                MessageBox.Show("Error occured: " + e);
            }      
        }

        /// <summary>
        /// Function, that fills the DataGridView(table) with data
        /// out of the LanguageList List
        /// </summary>
        /// <param name="path">Path to XML source file</param>
        /// <param name="name">XML source file name</param>
        public void FillTable(string path, string name)
        {
            table.Visible = true;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            int activeColumn = table.Columns.Add(path, name);

            foreach(XMLValue current in languageLists[activeColumn-1])
            {
                if(keyList.Contains(current.StringName))
                {
                    int cIndex = keyList.IndexOf(current.StringName);
                    table.Rows[cIndex].Cells[activeColumn].Value = current.StringText;
                }
                else
                {
                    //Add new row
                    keyList.Add(current.StringName);
                    int currentIndex = table.Rows.Add(current.StringName);
                    table.Rows[currentIndex].Cells[activeColumn].Value = current.StringText;

                    //If not translatable
                    if(!current.StringTranslateable)
                    {
                        table.Rows[currentIndex].ReadOnly = true;
                        table.Rows[currentIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            table.Columns[activeColumn].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Function that sets all options for the DataGridView (table)
        /// </summary>
        private void ApplyDesign()
        {
            
            //Permissions
            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.SelectionMode = DataGridViewSelectionMode.CellSelect;
            table.MultiSelect = false;
            table.AllowUserToResizeColumns = true;
            table.AllowUserToOrderColumns = false;
            table.AutoGenerateColumns = false;

            //Styles
            table.BorderStyle = BorderStyle.Fixed3D;
            table.DefaultCellStyle.SelectionBackColor = Color.White;
            table.DefaultCellStyle.SelectionForeColor = Color.Blue;
            Font columnFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            table.ColumnHeadersDefaultCellStyle.Font = columnFont;

            // Wrap
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            table.AllowDrop = false;
            table.AllowUserToOrderColumns = false;
            table.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            table.RowHeadersVisible = false;

            // lag compensation
            DoubleBuffered = true;
        }

    }
}