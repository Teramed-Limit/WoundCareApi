using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TeraLinkaCareApi.Common.Utils;

// Class to get the Display Name attribute of an enum value
public static class EnumHelper
{
    // Method to get the Display Name of an enum value
    public static string GetDisplayName(Enum enumValue)
    {
        // Get the type of the enum
        Type type = enumValue.GetType();
        // Get the member information for the enum value
        MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
        // Check if member information is not null and has elements
        if (memberInfo != null && memberInfo.Length > 0)
        {
            // Get the Display attribute of the enum value if it exists
            object[] attributes = memberInfo[0].GetCustomAttributes(
                typeof(DisplayAttribute),
                false
            );
            // Check if Display attribute is present
            if (attributes != null && attributes.Length > 0)
            {
                // Return the Name property of the Display attribute
                return ((DisplayAttribute)attributes[0]).Name;
            }
        }

        // Return the enum value as string if no Display attribute is found
        return enumValue.ToString();
    }
}
