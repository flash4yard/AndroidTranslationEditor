using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AndroidTranslationEditor
{
    public partial class Form1 : Form
    {
        List<List<XMLValue>> LanguageLists = new List<List<XMLValue>>();
        List<String> keyList = new List<string>();
        List<String> openFiles = new List<string>();
        Boolean loadingFile = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyDesign();
        }

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

        public void AddFileEntry(string path, string name)
        {
            LanguageLists.Add(XMLprocessing.XMLRead(path));
            openFiles.Add(path);
            FillTable(path, name);
        }

        public void FillTable(string path, string name)
        {
            table.Visible = true;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            int activeColumn = table.Columns.Add(path, name);

            foreach(XMLValue current in LanguageLists[activeColumn-1])
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

        private void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            if(!loadingFile)
            {
                XMLprocessing.XMLWrite(openFiles[e.ColumnIndex - 1], keyList[e.RowIndex], (string)table.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }        
        }

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

    public class XMLprocessing
    {
        public static List<XMLValue> XMLRead(string path)
        {
            XmlDocument XMLRead = new XmlDocument();
            List<XMLValue> values = new List<XMLValue>();

            XMLRead.Load(path);

            foreach (XmlNode node in XMLRead.SelectNodes("/resources/string"))
            {
                XMLValue currentValue = new XMLValue();

                currentValue.StringText = node.InnerText;

                foreach (XmlAttribute attribute in node.Attributes)
                {
                    switch (attribute.Name)
                    {
                        case "name":
                            currentValue.StringName = attribute.Value;
                            break;

                        case "translatable":
                            currentValue.StringTranslateable = Boolean.Parse(attribute.Value);
                            break;
                    }
                }
                values.Add(currentValue);
            }
            return values;
        }

        public static void XMLWrite(string path, string id, string value)
        {
            // Escape characters
            value = value.Replace(@"'",@"\'");
            value = value.Replace(@"\\", @"\");

            value = value.Replace(@"\", @"\\");
            value = value.Replace("\"", "\\\"");

            
            XmlDocument XMLWrite = new XmlDocument();

            XMLWrite.Load(path);

            if (XMLWrite.SelectSingleNode("/resources/string[@name='" + id + "']") != null)
            {
                XMLWrite.SelectSingleNode("/resources/string[@name='" + id + "']").InnerText = value;
            }
            else
            {
                XmlNode newItem = XMLWrite.CreateElement("string");
                newItem.InnerText = value;

                XmlAttribute newAttr = XMLWrite.CreateAttribute("name");
                newAttr.InnerText = id;

                newItem.Attributes.Append(newAttr);

                XMLWrite.SelectSingleNode("/resources").AppendChild(newItem);

            }
            XMLWrite.Save(path);
        }
    }

    public class XMLValue
    {
        public String StringName;
        public String StringText;
        public Boolean StringTranslateable = true;
    }
}