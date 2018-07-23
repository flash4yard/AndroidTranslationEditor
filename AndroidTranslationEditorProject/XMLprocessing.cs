using System;
using System.Collections.Generic;
using System.Xml;

namespace AndroidTranslationEditor
{
    /// <summary>
    /// Util Class that provides functions to write and parse
    /// Android string resource files
    /// </summary>
    public class XMLprocessing
    {
        /// <summary>
        /// Function that reads a XML file and stores each id and string
        /// into a new XMLValue type
        /// </summary>
        /// <param name="path">Path to XML source file</param>
        /// <returns>A list of XML Values</returns>
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

        /// <summary>
        /// Function that writes changed values back to the XML file
        /// and if the String identifier does not exist it creates a new entry
        /// </summary>
        /// <param name="path">Path to XML source file</param>
        /// <param name="id">String identifier</param>
        /// <param name="value">String content</param>
        public static void XMLWrite(string path, string id, string value)
        {
            // Escape characters
            value = value.Replace(@"'", @"\'");
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
}
