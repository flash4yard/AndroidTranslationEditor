using System;

namespace AndroidTranslationEditor
{
    /// <summary>
    /// Data holder class for XML Values from string resource files.
    /// </summary>
    public class XMLValue
    {
        /// <summary>
        /// String identifier
        /// </summary>
        public String StringName;

        /// <summary>
        /// String Content
        /// </summary>
        public String StringText;

        /// <summary>
        /// whether the string is translateable or not (standard is true)
        /// </summary>
        public Boolean StringTranslateable = true;
    }
}
