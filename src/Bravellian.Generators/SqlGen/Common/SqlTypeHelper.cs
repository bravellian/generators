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
/// Helper class for SQL type operations.
/// </summary>
public static class SqlTypeHelper
{
    /// <summary>
    /// Safely parses a SQL type string into a PwSqlType.
    /// </summary>
    /// <param name="sqlType">The SQL type string to parse.</param>
    /// <param name="logger">Optional logger for errors.</param>
    /// <returns>Parsed PwSqlType or Unknown if parsing fails.</returns>
    public static PwSqlType SafeParseSqlType(string sqlType, IBvLogger? logger = null)
    {
        if (string.IsNullOrWhiteSpace(sqlType))
        {
            return PwSqlType.Unknown;
        }

        try
        {
            // Call Parse method via reflection to avoid ambiguity
            var parseMethod = typeof(PwSqlType).GetMethod("Parse", new[] { typeof(string) });
            if (parseMethod != null)
            {
                return (PwSqlType)parseMethod.Invoke(null, new object[] { sqlType });
            }
        }
        catch (Exception ex)
        {
            logger?.LogError($"Failed to parse SQL type '{sqlType}': {ex.Message}");
        }

        return PwSqlType.Unknown;
    }
}
