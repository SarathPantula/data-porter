using System.ComponentModel;

namespace data_porter.Models.Enums;

/// <summary>
/// Error Code
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// File name cannot be left blank
    /// </summary>
    [Description("File name cannot be left blank")]
    FileNameCannotBeLeftBlank = 2001,

    /// <summary>
    /// File cannot be null
    /// </summary>
    [Description("File cannot be null")]
    FileCannotBeNull = 2002,

    /// <summary>
    /// Invalid Json
    /// </summary>
    [Description("Invalid Json File")]
    InvalidJson = 2003
}
