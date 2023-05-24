using System.Runtime.Serialization;

namespace LibraryWebApi.Models;

public enum UserRole
{
    [EnumMember(Value = "Normal")]
    Normal,
    [EnumMember(Value = "Admin")]
    Admin
}