using System;
using System.Reflection;
using System.Data;
using System.Linq;

namespace PicklesAutomation.Common
{
    public class EnumsManager
    {
        public static string GetEnumTextValue(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            EnumTextValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumTextValueAttribute), false) as EnumTextValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Text : null;
        }
        public static string GetEnumTextDesc(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            EnumTextDescAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumTextDescAttribute), false) as EnumTextDescAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Text : null;
        }
        public static string GetEnumTextNote(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            EnumTextNoteAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumTextNoteAttribute), false) as EnumTextNoteAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Note : null;
        }

        //NOT COMPLETED!!!!
        public static string GetEnumValue(Enum e, string text)
        {
            //// Get the type
            Type type = e.GetType();
            string enum_value = string.Empty;

            foreach (FieldInfo fi in type.GetFields())
            {
                string value = GetEnumTextValue(e);
                if (value.ToLower() == text.ToLower())
                {
                    //enum_value = type.GetField();
                    break;
                }
            }
            return enum_value;
        }
    }
    public sealed class EnumTextValueAttribute : Attribute
    {
        private readonly string enumTextValue;

        public EnumTextValueAttribute(string text)
        {
            enumTextValue = text;
        }

        public string Text
        {
            get { return enumTextValue; }
        }
    }
    public sealed class EnumTextDescAttribute : Attribute
    {
        private readonly string enumTextDesc;

        public EnumTextDescAttribute(string text)
        {
            enumTextDesc = text;
        }

        public string Text
        {
            get { return enumTextDesc; }
        }
    }
    public sealed class EnumTextNoteAttribute : Attribute
    {
        private readonly string enumTextNote;

        public EnumTextNoteAttribute(string text)
        {
            enumTextNote = text;
        }

        public string Note
        {
            get { return enumTextNote; }
        }
    }
}