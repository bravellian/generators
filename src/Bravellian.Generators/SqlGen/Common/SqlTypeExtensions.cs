// Copyright (c) Bravellian
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#nullable enable

using System;
using Bravellian.Generators.SqlGen.Pipeline._1_Ingestion.Model;

namespace Bravellian.Generators.SqlGen.Common;

/// <summary>
/// Extension methods for PwSqlType.
/// </summary>
public static class SqlTypeExtensions
{
    /// <summary>
    /// Creates a PwSqlType from a SQL type string.
    /// </summary>
    /// <param name="sqlType">The SQL type string to parse.</param>
    /// <returns>The corresponding PwSqlType, or Unknown if not recognized.</returns>
    public static PwSqlType FromSqlString(this string sqlType)
    {
        if (string.IsNullOrWhiteSpace(sqlType))
        {
            return PwSqlType.Unknown;
        }

        try
        {
            // Use the existing Parse method
            return PwSqlType.Parse(sqlType);
        }
        catch
        {
            // If parsing fails, return Unknown
            return PwSqlType.Unknown;
        }
    }

    /// <summary>
    /// Safe method to parse SQL type that won't throw exceptions.
    /// </summary>
    /// <param name="sqlType">The SQL type to parse.</param>
    /// <param name="logger">Optional logger for errors.</param>
    /// <returns>The parsed SQL type or Unknown if parsing fails.</returns>
    public static PwSqlType SafeParseSqlType(this string sqlType, IBvLogger? logger = null)
    {
        try
        {
            return PwSqlType.Parse(sqlType);
        }
        catch (Exception ex)
        {
            logger?.LogError($"Failed to parse SQL type '{sqlType}': {ex.Message}");
            return PwSqlType.Unknown;
        }
    }
}
