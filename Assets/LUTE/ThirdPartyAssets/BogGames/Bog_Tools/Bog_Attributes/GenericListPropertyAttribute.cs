using UnityEngine;

namespace BogGames.Tools
{
    public class GenericListPropertyAttribute : PropertyAttribute
    {
        public GenericListPropertyAttribute(params System.Type[] listedTypes)
        {
            this.ListedTypes = listedTypes;
        }

        public GenericListPropertyAttribute(string defaultText, params System.Type[] variableTypes)
        {
            this.defaultText = defaultText;
            this.ListedTypes = variableTypes;
        }

        public string defaultText = "<None>";
        public string compatibleVariableName = string.Empty;

        public System.Type[] ListedTypes { get; set; }
    }
}
